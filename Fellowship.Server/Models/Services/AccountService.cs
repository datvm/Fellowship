using Fellowship.Server.Models.Auth;
using Fellowship.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Fellowship.Server.Models.Services
{

    public interface IAccountSerivce : IService
    {
        Task<Account> GetFromFacebookAsync(string facebookId);
        Task<Account> GetOrCreateFromFacebookAsync(FacebookProfile profile);
        Task<List<AccountSpecialClaim>> GetSpecialClaimsAsync(int accountId);
        Task<List<Claim>> GetSecurityClaimsAsync(int accountId);
    }

    public class AccountService : IAccountSerivce, IService<IAccountSerivce>
    {

        FellowshipContext fellowshipContext;

        public AccountService(FellowshipContext fellowshipContext)
        {
            this.fellowshipContext = fellowshipContext;
        }

        public async Task<Account> GetFromFacebookAsync(string facebookId)
        {
            return await this.fellowshipContext.Account
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.FacebookId == facebookId);
        }

        public async Task<Account> GetOrCreateFromFacebookAsync(FacebookProfile profile)
        {
            if (string.IsNullOrEmpty(profile.Id))
            {
                throw new ArgumentException("Empty Facebook Profile Id");
            }

            var result = await this.GetFromFacebookAsync(profile.Id);

            if (result == null)
            {
                result = new Account()
                {
                    FacebookId = profile.Id,
                    FacebookProfile = JsonConvert.SerializeObject(profile),
                    Name = profile.DisplayName ?? profile.Email ?? profile.Id,
                };

                this.fellowshipContext.Account.Add(result);
                await this.fellowshipContext.SaveChangesAsync();
            }

            return result;
        }

        public async Task<List<AccountSpecialClaim>> GetSpecialClaimsAsync(int accountId)
        {
            var result = (await this.fellowshipContext.AccountSpecialClaim
                .Where(q => q.AccountId == accountId)
                .ToListAsync());

            return result;
        }

        public async Task<List<Claim>> GetSecurityClaimsAsync(int accountId)
        {
            var result = (await this.GetSpecialClaimsAsync(accountId))
                .Select(q => new Claim(q.ClaimType, q.ClaimValue))
                .ToList();

            result.Add(new Claim("sub", accountId.ToString()));

            return result;
        }

    }
}
