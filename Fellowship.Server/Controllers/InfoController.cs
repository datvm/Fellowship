using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Fellowship.Common.Settings;
using static Fellowship.Common.Settings.AppSettings;

namespace Fellowship.Server.Controllers
{

    [Route("api/v1/info")]
    [ApiController]
    public class InfoController : ControllerBase
    {

        private AppSettings appSettings;

        public InfoController(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        [HttpGet, Route("settings")]
        public WebSettings Settings()
        {
            return this.appSettings.Web;
        }

    }

}