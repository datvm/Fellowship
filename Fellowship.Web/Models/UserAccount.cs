using Microsoft.AspNetCore.Blazor.Browser.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fellowship.Web.Models
{
    public sealed class UserAccount
    {

        private HttpClient http;

        public bool LoggedIn { get; private set; }

        public UserAccount(HttpClient http)
        {
            this.http = http;

            this.Initialize();
        }

        private void Initialize()
        {

        }

        private string GetToken()
        {
            return RegisteredFunction.Invoke<string>("GetStorage", "Jwt");

        }

    }
}
