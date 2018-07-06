using LukeVo.EnvironmentSettings.Json;
using ServiceSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fellowship.Common.Settings
{

    [Ignore]
    public class AppSettings : IService
    {

        public static AppSettings Load()
        {
            return JsonEnvironmentSettings.GetInstance<AppSettings>("FellowshipSettingsFilePath");
        }

        public ServerOnlySettings ServerOnly { get; set; }
        public WebSettings Web { get; set; }
        
        public class ServerOnlySettings
        {
            public FacebookSettings Facebook { get; set; }
            public string DatabaseConnectionString { get; set; }
            public JwtSettings Jwt { get; set; }
            public string[] Cors { get; set; }

            public class FacebookSettings
            {
                public string AppId { get; set; }
                public string AppSecret { get; set; }
            }

            public class JwtSettings
            {
                public string SecurityKey { get; set; }
                public string Audience { get; set; }
                public string Issuer { get; set; }
                public int? LifeTimeSecond { get; set; }
            }
        }

        public class WebSettings
        {

            public FacebookSettings Facebook { get; set; }
            public string ApiServer { get; set; }

            public class FacebookSettings
            {
                public string RedirectUri { get; set; }
            }
        }
    }

}
