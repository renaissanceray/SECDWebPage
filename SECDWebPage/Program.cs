using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SECDWebPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseUrls("http://localhost:7001")
        .UseConfiguration(new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build()
        )
        .UseStartup<Startup>();
    }
}
