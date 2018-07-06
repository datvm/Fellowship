using Microsoft.AspNetCore.Blazor.Browser.Interop;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static Fellowship.Common.Settings.AppSettings;

namespace Fellowship.Web.Models
{
    public sealed class UserAccount
    {

        private HttpClient http;
        private LocalSettings localSettings;
        
        public bool LoggedIn { get; private set; }

        public UserAccount(HttpClient http, LocalSettings settings)
        {
            this.http = http;
            this.localSettings = settings;

            this.Initialize();
        }

        private void Initialize()
        {
            var token = this.GetToken();

            if (this.ValidateToken(token))
            {
                this.LoggedIn = true;
            }
            else
            {
                this.SetToken(null);
            }
        }

        public async Task<string> GetFacebookLogInUrl()
        {
            var state = Guid.NewGuid().ToString();
            RegisteredFunction.Invoke<string>("SetStorage", "FacebookState", state);

            var redirectUrl = this.localSettings.WebSettings.Facebook.RedirectUri;
         
            var request = this.http
                .Build("api/v1/auth/request")
                .AddQueryParameter("state", state)
                .AddQueryParameter("redirectUrl", redirectUrl);

            var response = await request.ExecuteAsync();
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private string GetToken()
        {
            return RegisteredFunction.Invoke<string>("GetStorage", "Jwt");
        }

        private string SetToken(string token)
        {
            return RegisteredFunction.Invoke<string>("SetStorage", "Jwt", token);
        }

        private bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            return true;
        }

    }
}
