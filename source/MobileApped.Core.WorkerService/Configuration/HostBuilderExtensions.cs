using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MobileApped.Core.WorkerService.Configuration
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureWithDefaults(this IHostBuilder hostBuilder)
        {
            string aiConfigKey = "ApplicationInsights:InstrumentationKey";
            hostBuilder
                .ConfigureAppConfiguration((context, config) => {
                    config.AddEnvironmentVariables();

                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string baseConfigPath = Path.Combine(baseDirectory, "Configuration", "Settings");
                    config.SetBasePath(baseConfigPath);

                    string environment = context.HostingEnvironment.EnvironmentName;
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddJsonFile($"appsettings.{environment}.json", optional: true);
                })
                .ConfigureServices((context, services) => {
                    string aiKey = context.Configuration.GetValue<string>(aiConfigKey);
                    services.AddApplicationInsightsTelemetryWorkerService(aiKey);
                })
                .ConfigureLogging((context, logging) => {
                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddConsole();
                    }
                    string aiKey = context.Configuration.GetValue<string>(aiConfigKey);
                    object p = logging.AddApplicationInsights(aiKey);
                });

            return hostBuilder;
        }

        public async static Task StartAndWaitAsync(this IHostBuilder hostBuilder)
        {
            using IHost host = hostBuilder.Build();
            TelemetryClient telemetry = host.Services.GetService<TelemetryClient>();
            ILogger logger = host.Services.GetService<ILogger<IHost>>();

            await host.StartAsync();
            logger.LogInformation("Started worker host");
            await host.WaitForShutdownAsync();

            logger.LogInformation("Stopped worker host");
            logger.LogInformation("Flushing telemetry data");
            telemetry.Flush();

            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
}
