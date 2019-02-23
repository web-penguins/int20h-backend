using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddCors(options =>
                {
                    options.AddPolicy(EnvironmentName.Development, policy =>
                        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                    options.AddPolicy(EnvironmentName.Production, policy =>
                        policy.WithOrigins("https://int20h-2019.vova-lantsov.com").AllowAnyHeader().AllowAnyMethod());
                })
                .AddJsonFormatters(settings => settings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddDataAnnotations()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}