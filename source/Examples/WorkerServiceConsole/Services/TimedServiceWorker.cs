using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerServiceConsole.Services
{
    public class TimedServiceWorker : BackgroundService
    {
        private Timer timer;
        private ILogger<TimedServiceWorker> logger;
        private IHostApplicationLifetime app;

        public TimedServiceWorker(
            IHostApplicationLifetime app,
            ILogger<TimedServiceWorker> logger)
        {
            timer = new Timer(RunTimedTask);
            this.app = app;
            this.logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("\tStarting timed task");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            timer.Change(1000, 2000);
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stopping timed task");
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            return base.StopAsync(cancellationToken);
        }

        private void RunTimedTask(object state)
        {
            logger.LogInformation($"{DateTime.UtcNow}\tTimed task fired");
        }
    }
}
