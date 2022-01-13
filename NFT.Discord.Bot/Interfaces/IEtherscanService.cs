using NFT.DiscordBot.Models;

namespace NFT.DiscordBot.Interfaces
{
    public interface IEtherscanService
    {
        Task<GasPriceResult> GetGasPrice();
    }
}
