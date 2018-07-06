using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Fellowship.Common.Settings;
using Fellowship.Server.Models.Auth;
using Fellowship.Server.Models.Entities;
using Fellowship.Server.Models.Services;
using JwtSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fellowship.Server.Controllers
{

    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        IAccountSerivce accountService;
        ExternalLoginProvider externalLoginProvider;
        JwtIssuer jwtIssuer;
        AppSettings appSettings;

        public AuthController(
            ExternalLoginProvider externalLoginProvider,
            IAccountSerivce accountService,
            JwtIssuer jwtIssuer,
            AppSettings appSettings)
        {
            this.externalLoginProvider = externalLoginProvider;
            this.accountService = accountService;
            this.jwtIssuer = jwtIssuer;
            this.appSettings = appSettings;
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
#if DEBUG
                    var profile = JsonConvert.DeserializeObject<FacebookProfile>(
                         appSettings.ServerOnly.DebugFacebookProfile);
#else
                    var profile = await this.externalLoginProvider.GetFacebookIdAsync(code, redirectUrl);
#endif


                    if (profile != null)
                    {
                        account = await this.accountService.GetOrCreateFromFacebookAsync(profile);
                    }

                    break;
                default:
                    return this.BadRequest("Unknown service");
            }

            if (account == null)
            {
                return this.BadRequest("Invalid token");
            }

            var claims = await this.accountService.GetSecurityClaimsAsync(account.Id);

            var token = this.jwtIssuer.IssueToken(claims);

            return this.Ok(new
            {
                Token = token,
            });
        }

    }

}