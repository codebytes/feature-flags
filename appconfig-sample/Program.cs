using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace appconfig_sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.ConfigureAppConfiguration(config =>
                {
                    var settings = config.Build();
                    var connection = settings.GetConnectionString("AppConfig");
                    config.AddAzureAppConfiguration(options =>
                        options.Connect(connection)
                         .ConfigureRefresh(refreshOpt =>
                                {
                                    refreshOpt.Register(key: "Beta", refreshAll: true)
                                    .SetCacheExpiration(new TimeSpan(0, 0, 5));
                                })
                          .UseFeatureFlags(featureFlagOptions =>
                            {
                                featureFlagOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5);
                            }));
                }).UseStartup<Startup>());
    }
}
