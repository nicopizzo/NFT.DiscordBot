using Discord.Commands;
using NFT.DiscordBot.Interfaces;

namespace NFT.DiscordBot.Modules
{
    public class GasPriceModule : ModuleBase<SocketCommandContext>
    {
        private readonly IEtherscanService _EtherscanService;

        public GasPriceModule(IEtherscanService etherscanService)
        {
            _EtherscanService = etherscanService;
        }

        [Command("gasprice")]
        [Summary("Gets the current price of ethereum")]
        public async Task GetGasPriceAsync()
        {
            var result = await _EtherscanService.GetGasPrice();
            if (result.status == "0") return;
            await ReplyAsync($"Slow: {result.result.SafeGasPrice}, Average: {result.result.ProposeGasPrice}, Fast: {result.result.FastGasPrice}");
        }
    }
}
