namespace NFT.DiscordBot.Models
{
    public class GasPrice
    {
        public string SafeGasPrice { get; set; }
        public string ProposeGasPrice { get; set; }
        public string FastGasPrice { get; set; }
    }

    public class GasPriceResult
    {
        public string status { get; set; }
        public GasPrice result { get; set; }
    }
}
