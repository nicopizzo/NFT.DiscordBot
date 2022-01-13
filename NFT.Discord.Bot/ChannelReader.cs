using Discord;
using Discord.WebSocket;
using NFT.DiscordBot.Interfaces;

namespace NFT.DiscordBot
{
    public class ChannelReader : IChannelReader
    {
        private readonly DiscordSocketClient _DiscordClient;
        private SocketGuild _Guild = null;

        public ChannelReader()
        {
            _DiscordClient = new DiscordSocketClient();
            Init().GetAwaiter().GetResult();
        }

        private async Task Init()
        {         
            await _DiscordClient.LoginAsync(TokenType.Bot, "OTMwMjg5MjMyOTI3NDA4MTQ4.YdztXQ.JTVIJ-qQprG1h4DpL4ox9q3ACIk");
            await _DiscordClient.StartAsync();
            _DiscordClient.Ready += _DiscordClient_Ready;
            _DiscordClient.MessageReceived += _DiscordClient_MessageReceived;
        }

        private Task _DiscordClient_Ready()
        {
            _Guild = _DiscordClient.Guilds.First();
            SocketTextChannel? channel = _Guild.Channels.First(f => f.Name == "whitelist") as SocketTextChannel;
            return Task.CompletedTask;
        }

        private Task _DiscordClient_MessageReceived(SocketMessage arg)
        {
            Console.WriteLine(arg.Content);
            return Task.CompletedTask;
        }  
    }
}