using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pitstop.Infrastructure.Messaging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Luvits.ContentManagementEventHandler
{
    class Program
    {
     public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

     private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddJsonFile($"appsettings.json", optional: false);
                    configHost.AddEnvironmentVariables();
                    configHost.AddEnvironmentVariables("LUVITS_");
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IMessageHandler>((svc) =>
                    {
                        var rabbitMQConfigSection = hostContext.Configuration.GetSection("RabbitMQ");
                        string rabbitMQHost = rabbitMQConfigSection["Host"];
                        string rabbitMQUserName = rabbitMQConfigSection["Username"];
                        string rabbitMQPassword = rabbitMQConfigSection["Password"];
                        return new RabbitMQMessageHandler(rabbitMQHost, rabbitMQUserName, rabbitMQPassword, "Luvits", "ContentManagement", ""); ;
                    });

                    services.AddHostedService<EventHandler>();
                })
               .UseSerilog((hostContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
                })
                .UseConsoleLifetime();
            return hostBuilder;
        }
    }
}
