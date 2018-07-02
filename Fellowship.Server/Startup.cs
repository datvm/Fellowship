using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fellowship.Common.Settings;
using Fellowship.Server.Models.Entities;
using JwtSharp.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fellowship.Server
{
    public class Startup
    {
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var settings = AppSettings.Load();
            services.AddSingleton(settings);

            var connectionString = settings.ServerOnly.DatabaseConnectionString;
            services.AddDbContext<FellowshipContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddJwtIssuerAndBearer(options =>
            {
                var jwtSettings = settings.ServerOnly.Jwt;

                options.Audience = jwtSettings.Audience;
                options.Issuer = jwtSettings.Issuer;
                options.SecurityKey = jwtSettings.SecurityKey;
                options.ExpireSeconds = jwtSettings.LifeTimeSecond;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
