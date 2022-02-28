using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Winton.Extensions.Configuration.Consul;
using ILogger = Serilog.ILogger;

namespace payment_service
{
    public class Program
    {
        const string ConsulSection = "Consul";
        const string ConsulHost = "Host";
        public static void Main(string[] args)
        {
            var appConfiguration = GetAppConfiguration();
            Log.Logger = CreateLogger(appConfiguration);
            CreateHostBuilder(args,appConfiguration).Build().Run();
        }

        private static IConfiguration GetAppConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }

        private static ILogger CreateLogger(IConfiguration appConfiguration)
        {
            var loggerConfiguration = new LoggerConfiguration();
            return loggerConfiguration.CreateLogger();
        }

        public static IHostBuilder CreateHostBuilder(string[] args,IConfiguration appConfiguration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureAppConfiguration(builder =>
                {
                    builder.AddConsul(
                        "config/payment-service",
                        options =>
                        {
                            options.ConsulConfigurationOptions =
                                cco => { cco.Address = new Uri(appConfiguration.GetSection(ConsulSection)[ConsulHost]); };
                            //options.Optional = true;
                            options.PollWaitTime = TimeSpan.FromSeconds(5);
                            options.ReloadOnChange = true;
                        }).AddEnvironmentVariables();
                });
    }
}
