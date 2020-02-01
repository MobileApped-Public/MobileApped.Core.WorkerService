using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MobileApped.Core.WorkerService.Configuration;
using WorkerServiceConsole.Services;

namespace WorkerServiceConsole
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder =
                Host.CreateDefaultBuilder(args)
                .ConfigureWithDefaults();

            hostBuilder.ConfigureServices(services => {
                services.AddHostedService<TimedServiceWorker>();
            });
            await hostBuilder.StartAndWaitAsync();
        }
    }
}
