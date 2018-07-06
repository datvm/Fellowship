using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fellowship.Server.Models.Auth;
using Fellowship.Server.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fellowship.Server.Controllers
{

    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private FellowshipContext fellowshipContext;
        private ExternalLoginProvider externalLoginProvider;

        public AuthController(ExternalLoginProvider externalLoginProvider, FellowshipContext fellowshipContext)
        {
            this.externalLoginProvider = externalLoginProvider;
            this.fellowshipContext = fellowshipContext;
        }

        [HttpGet, Route("request")]
        public string FacebookLoginUrl(string state, string redirectUrl)
        {
            return this.externalLoginProvider
                .GetFacebookLoginUrl(state, redirectUrl)
                .AbsoluteUri;
        }

        [HttpGet, Route("token")]
        public async Task<IActionResult> Token(string code, string service, string redirectUrl)
        {
            Account account = null;

            switch (service.ToLower())
            {
                case "facebook":
                    var profile = await this.externalLoginProvider.GetFacebookIdAsync(code, redirectUrl);
                    
                    if (profile != null)
                    {
                        account = this.fellowshipContext.Account
                            .FirstOrDefault(q => q.FacebookId == profile.Id);
                    }

                    break;
                default:
                    return this.BadRequest("Unknown service");
            }

            if (account == null)
            {
                return this.BadRequest("Invalid token");
            }

            return null;
        }

    }

}