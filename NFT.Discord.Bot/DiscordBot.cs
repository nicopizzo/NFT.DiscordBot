using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Options;
using NFT.DiscordBot.Interfaces;
using NFT.DiscordBot.Models;

namespace NFT.DiscordBot
{
    internal class DiscordBot : IDiscordBot, IAsyncDisposable
    {
        private readonly DiscordSocketClient _DiscordClient;
        private readonly CommandService _CommandService;
        private readonly DiscordOptions _Options;
        private readonly IServiceProvider _ServiceProvider;
        private SocketGuild? _Guild = null;

        public event EventHandler BotReady;

        public DiscordBot(DiscordSocketClient client, 
            IOptions<DiscordOptions> options,
            CommandService commandService,
            IServiceProvider serviceProvider)
        {
            _DiscordClient = client;
            _CommandService = commandService;
            _ServiceProvider = serviceProvider;
            _Options = options.Value;
        }

        public async Task StartAsync()
        {
            await _DiscordClient.LoginAsync(TokenType.Bot, _Options.Key);
            await _DiscordClient.StartAsync();

            // setup event handling
            _DiscordClient.Ready += _DiscordClient_Ready;
            _DiscordClient.MessageReceived += _DiscordClient_MessageReceived;
            _DiscordClient.MessageReceived += _DiscordClient_MonitorWhitelist;

            await _CommandService.AddModulesAsync(GetType().Assembly, _ServiceProvider);
        }

        private async Task _DiscordClient_MonitorWhitelist(SocketMessage arg)
        {
            var msg = ParseMessage(arg);
            if (msg == null) return;

            if(msg.Channel.Name == "whitelist" && (!msg.Content.ToLower().StartsWith("0x") || msg.Content.Length != 42))
            {
                await msg.DeleteAsync();
            }
        }

        private async Task _DiscordClient_MessageReceived(SocketMessage arg)
        {
            var msg = ParseMessage(arg);
            if (msg == null) return;
            if (msg.Author.Id == _DiscordClient.CurrentUser.Id) return;

            var context = new SocketCommandContext(_DiscordClient, msg);

            int argPos = 0;
            if(msg.HasCharPrefix('!', ref argPos))
            {
                var result = await _CommandService.ExecuteAsync(context, argPos, _ServiceProvider);
                if (!result.IsSuccess)
                    await context.Channel.SendMessageAsync(result.ToString());
            }
        }

        private async Task _DiscordClient_Ready()
        {
            _Guild = _DiscordClient.Guilds.First();
            SocketTextChannel? whitelist = _Guild.Channels.First(f => f.Name == "whitelist") as SocketTextChannel;
            BotReady(this, EventArgs.Empty);
        }

        private SocketUserMessage? ParseMessage(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg == null) return null;           
            return msg;
        }

        public async ValueTask DisposeAsync()
        {
            if(_DiscordClient.ConnectionState == ConnectionState.Connected)
            {
                await _DiscordClient.StopAsync();
            }
        }
    }
}
