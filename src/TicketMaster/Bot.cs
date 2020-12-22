using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace TicketMaster
{
    internal class Bot
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _services;

        public Bot(DiscordSocketClient client, CommandService commandService, IServiceProvider services)
        {
            _client = client;
            _commandService = commandService;
            _services = services;
        }

        internal async Task StartAsync()
        {
            await _client.LoginAsync(TokenType.Bot, "Nzg4ODc5MjU1ODY5NzE4NTc5.X9p7Dw.FtfOpJsNGTzQvR8RIFpB57i50TM");
            await _client.StartAsync();

            _client.Ready += _client_ReadyAsync;
            _client.MessageReceived += _client_MessageReceivedAsync;

            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(),
                                        _services);

            await Task.Delay(-1);
        }

        private async Task _client_MessageReceivedAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            await _commandService.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }

        private Task _client_ReadyAsync()
        {
            Console.WriteLine("Client is ready");
            return Task.CompletedTask;
        }
    }
}