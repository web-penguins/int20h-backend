using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Host
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.AddServerHeader = false;
                    options.ListenLocalhost(25007);
                })
                .UseSockets()
                .UseEnvironment(EnvironmentName.Development)
                .ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                    
                    if (context.HostingEnvironment.IsDevelopment())
                        builder.AddDebug();
                })
                .UseStartup<Startup>()
                .UseShutdownTimeout(TimeSpan.FromSeconds(10d))
                .SuppressStatusMessages(false)
                .CaptureStartupErrors(true)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build().RunAsync();
        }
    }
}