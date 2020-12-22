using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace TicketMaster
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .BuildServiceProvider();

            var bot = ActivatorUtilities.CreateInstance<Bot>(services);
            await bot.StartAsync();
        }
    }
}
