using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace TicketMaster.Modules
{
    public class TicketModule : ModuleBase<SocketCommandContext>
    {
        [Command("TicketOpen")]
        public async Task TicketOpenAsync()
        {
            //you would want to add perms to this too, I leave that to you though
            var channel = await Context.Guild.CreateTextChannelAsync("Ticket 1", c => c.Topic = $"Hey {Context.User.Username}, this is your personal help channel.");
            GlobalInMemoryStorage.TocketIDs.Add(channel.Id);
        }

        [Command("TicketClose")]
        public async Task TicketCloseAsync()
        {
            if (GlobalInMemoryStorage.TocketIDs.Any(i => i == Context.Channel.Id))
            {
                var channel = Context.Channel as SocketTextChannel;
                await channel.DeleteAsync();
                var userDms = await Context.User.GetOrCreateDMChannelAsync();
                await userDms.SendMessageAsync("Your ticket channel has been closed.");
            }

        }
    }
}
