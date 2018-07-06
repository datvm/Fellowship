using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Fellowship.Common.Settings.AppSettings;

namespace Fellowship.Web.Host.Controllers
{

    public class SettingsController : Controller
    {

        private WebSettings settings;

        public SettingsController(WebSettings settings)
        {
            this.settings = settings;
        }

        [Route("settings")]
        public WebSettings Settings()
        {
            return this.settings;
        }

    }

}
