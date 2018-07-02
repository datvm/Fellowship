using Fellowship.Web.Models;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fellowship.Web
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(services =>
            {
                services.AddSingleton<UserAccount>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}
