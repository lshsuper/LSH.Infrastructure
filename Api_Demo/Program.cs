using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseKestrel().ConfigureAppConfiguration((ctx, cnf) =>
                {
                    var env = ctx.HostingEnvironment;
                    cnf.AddJsonFile("appsettings.json", true, true)
                       .AddJsonFile($"application_{env.EnvironmentName}.json", true, true);
                });
    }
}
