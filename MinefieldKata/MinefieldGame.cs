using Microsoft.Extensions.Hosting;

namespace MinefieldKata
{
    public class MinefieldGame : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Running");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping");
            return Task.CompletedTask;
        }
    }
}