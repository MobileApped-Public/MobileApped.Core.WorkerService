using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;

namespace MobileApped.Core.WorkerService.Configuration.Telemetry
{
    public static class TelemetryExtensions
    {
        public static IServiceCollection AddWorkerServiceTelemetry(this IServiceCollection services) =>
           services.AddApplicationInsightsTelemetryWorkerService();

        public static IServiceCollection AddWorkerServiceTelemetryProcessor<TProcessor>(this IServiceCollection services)
            where TProcessor : ITelemetryProcessor =>
            services.AddApplicationInsightsTelemetryProcessor<TProcessor>();
    }
}
