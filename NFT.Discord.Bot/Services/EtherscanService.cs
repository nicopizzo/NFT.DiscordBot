using Microsoft.Extensions.Options;
using NFT.DiscordBot.Interfaces;
using NFT.DiscordBot.Models;
using System.Text.Json;

namespace NFT.DiscordBot.Services
{
    public class EtherscanService : IEtherscanService
    {
        private readonly HttpClient _HttpClient;
        private readonly EtherscanOptions _Options;

        public EtherscanService(HttpClient client, IOptions<EtherscanOptions> options)
        {
            _HttpClient = client;
            _Options = options.Value;
        }

        public async Task<GasPriceResult> GetGasPrice()
        {
            var response = await _HttpClient.GetAsync($"?module=gastracker&action=gasoracle&apikey={_Options.Key}");

            var rawResult = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return new GasPriceResult() { status = "0" };

            var result = JsonSerializer.Deserialize<GasPriceResult>(rawResult);

            return result;
        }
    }
}
