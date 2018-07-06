using Fellowship.Common.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using ServiceSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fellowship.Server.Models.Auth
{

    public class ExternalLoginProvider : IService
    {

        private AppSettings settings;
        private RestClient RestClient { get; set; } = new RestClient("https://www.example.com/");

        public ExternalLoginProvider(AppSettings settings)
        {
            this.settings = settings;
        }

        public Uri GetFacebookLoginUrl(string state, string redirectUrl)
        {
            var oauthSettings = this.settings.ServerOnly.Facebook;

            var request = new RestRequest("https://www.facebook.com/v3.0/dialog/oauth")
                .AddQueryParameter("client_id", oauthSettings.AppId)
                .AddQueryParameter("redirect_uri", redirectUrl)
                .AddQueryParameter("response_type", "code")
                .AddQueryParameter("state", state);

            return this.RestClient.BuildUri(request);
        }

        public async Task<FacebookProfile> GetFacebookIdAsync(string code, string redirectUrl)
        {
            var oauthSettings = this.settings.ServerOnly.Facebook;

            var tokenRequest = new RestRequest("https://graph.facebook.com/v3.0/oauth/access_token", Method.GET)
                .AddQueryParameter("client_id", oauthSettings.AppId)
                .AddQueryParameter("redirect_uri", redirectUrl)
                .AddQueryParameter("client_secret", oauthSettings.AppSecret)
                .AddQueryParameter("code", code);

            var tokenResponse = await this.RestClient.ExecuteTaskAsync(tokenRequest);
            tokenResponse.EnsureSuccessful();

            var token = JsonConvert.DeserializeObject<JObject>(tokenResponse.Content)
                ["access_token"].Value<string>() as string;

            var inspectRequest = new RestRequest("https://graph.facebook.com/me", Method.GET)
                .AddQueryParameter("access_token", token)
                .AddQueryParameter("fields", "id,name,email");

            var inspectResponse = await this.RestClient.ExecuteTaskAsync(inspectRequest);
            inspectResponse.EnsureSuccessful();

            var userInfo = JsonConvert.DeserializeObject<JObject>(inspectResponse.Content);

            var result = new FacebookProfile()
            {
                Id = userInfo["id"].Value<string>(),
                Email = userInfo["email"].Value<string>(),
                DisplayName = userInfo["name"].Value<string>(),
                Service = "Facebook",
            };

            // Try getting the Profile Pic if possible
            try
            {
                var profilePicRequest = new RestRequest("https://graph.facebook.com/me/picture", Method.GET)
                    .AddQueryParameter("access_token", token)
                    .AddQueryParameter("redirect", "false");

                var profilePicResponse = await this.RestClient.ExecuteTaskAsync(profilePicRequest);
                if (profilePicResponse.IsSuccessful)
                {

                    var data = JsonConvert.DeserializeObject<JObject>(profilePicResponse.Content)
                        ["data"].Value<JObject>();

                    if (!data["is_silhouette"].Value<bool>())
                    {
                        result.ProfilePic = data
                            ["url"].Value<string>();
                    }

                }
            }
            catch (Exception) { }

            return result;
        }

    }

    public class FacebookProfile
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string ProfilePic { get; set; }
        public string Service { get; set; }
    }

}
