using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fellowship.Server.Controllers
{
    [Route("api/v1/test")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet, Route("ping")]
        public void Ping() { }

        [Authorize]
        [HttpGet, Route("claims")]
        public List<KeyValuePair<string, string>> Claims()
        {
            return this.User.Claims
                .Select(q => new KeyValuePair<string, string>(q.Type, q.Value))
                .ToList();
        }

    }
}
