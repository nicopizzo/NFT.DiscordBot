namespace NFT.DiscordBot.Interfaces
{
    public interface IDiscordBot
    {
        event EventHandler BotReady;

        Task StartAsync();
    }
}
