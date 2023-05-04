using Microsoft.Extensions.Hosting;
using MinefieldKata.Models;

namespace MinefieldKata
{
    public class MinefieldGame : IHostedService
    {
        private readonly IGame game;

        public MinefieldGame(IGame game)
        {
            this.game = game;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await game.Run(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}