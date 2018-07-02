using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fellowship.Server.Controllers
{

    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpGet, Route("request")]
        public string FacebookLoginUrl(string state)
        {
            return "";
        }

        public void Token(string code, string service)
        {
            
        }

    }

}