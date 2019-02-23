using Host.Database;
using Host.Extensions;
using Host.MvcFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Host
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>((provider, builder) =>
                builder.UseInMemoryDatabase(nameof(Context)));
            
            services.AddMvcCore(options => options.Filters.Add(typeof(BadRequestGlobalFilter)))
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Context context)
        {
            context.AddDefaultData();
            
            app.UseCors(env.IsDevelopment() ? EnvironmentName.Development : EnvironmentName.Production).UseMvc();
        }
    }
}