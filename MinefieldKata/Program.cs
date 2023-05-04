using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinefieldKata.Models;
using MinefieldKata.Utilities;

namespace MinefieldKata
{
    class Program
    {
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("config.json", optional: true, reloadOnChange: true)
            .Build();

        public static async Task Main()
        {
            var numberOfMines = _configuration.GetValue<int>("mineCount");
            var boardWidth = _configuration.GetValue<int>("width");
            var boardHeight = _configuration.GetValue<int>("height");

            var builder = new HostBuilder()
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddScoped<IGame, Game>();
                  services.AddTransient<IRandomProvider, RandomProvider>();
                  services.AddScoped<IUserInput, UserInput>();
                  services.AddScoped<IMinePositionGenerator, MinePositionGenerator>();
                  services.AddScoped<IConsoleDisplay, ConsoleDisplay>();

                  services.AddScoped<IMap, Map>(x =>
                      new Map(x.GetService<IMinePositionGenerator>(), boardWidth, boardHeight, numberOfMines));

                  services.AddSingleton(_configuration);

                  services.AddHostedService<MinefieldGame>();
              });

            await builder.RunConsoleAsync();
        }
    }
}