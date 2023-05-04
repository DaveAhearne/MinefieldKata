using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MinefieldKata
{
    class Program
    {
        private static readonly IConfiguration _configuration = new ConfigurationBuilder()
            .AddJsonFile("config.json", optional: true, reloadOnChange: true)
            .Build();

        public static async Task Main()
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(_configuration);

                    services.AddHostedService<MinefieldGame>();
                });

            await builder.RunConsoleAsync();
        }
    }
}