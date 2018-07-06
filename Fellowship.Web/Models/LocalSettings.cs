using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Fellowship.Common.Settings.AppSettings;

namespace Fellowship.Web.Models
{

    public class LocalSettings
    {

        private HttpClient http;
        public WebSettings WebSettings { get; private set; }

        public LocalSettings(HttpClient http)
        {
            this.http = http;
        }

        public async Task Initialize()
        {
            this.WebSettings = await this.http.GetJsonAsync<WebSettings>("/settings");
            RestBuilder.BaseUrl = this.WebSettings.ApiServer;
        }

    }

}
