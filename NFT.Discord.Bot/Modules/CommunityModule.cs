using Discord.Commands;

namespace NFT.DiscordBot.Modules
{
    public class CommunityModule : ModuleBase<SocketCommandContext>
    {
        [Command("whitelist")]
        [Alias("privatesale")]
        [Summary("Whitelist details")]
        public async Task WhitelistAsync()
        {
            await ReplyAsync($"Whitelist your eth address in the \"whitelist\" channel to be qualify for the private sale");
        }

        [Command("giveaway")]
        [Summary("Givaway details")]
        public async Task GiveawayAsync()
        {
            await ReplyAsync($"Owning a BB Beast will qualify you for givaways in the future");
        }

        [Command("roadmap")]
        [Summary("Roadmap details")]
        public async Task RoadAsync()
        {
            await ReplyAsync($"Our roadmap can be found here at https://bbbeast.io");
        }
    }
}
