using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NFT.DiscordBot.Interfaces;
using NFT.DiscordBot.Models;
using NFT.DiscordBot.Services;

namespace NFT.DiscordBot
{
    public static class DiscordConfiguration
    {
        public static IServiceCollection AddDiscordBot(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<DiscordOptions>()
                .Bind(config.GetSection(DiscordOptions.NAME));
            services.AddOptions<EtherscanOptions>()
                .Bind(config.GetSection(EtherscanOptions.NAME));

            services.AddHttpClient<IEtherscanService, EtherscanService>((s,c) =>
            {
                var options = s.GetRequiredService<IOptions<EtherscanOptions>>();
                c.BaseAddress = new Uri(options.Value.Url);
            });
            services.AddSingleton<CommandService>();
            services.AddSingleton<DiscordSocketClient>();
            services.AddSingleton<IDiscordBot, DiscordBot>();

            return services;
        }
    }
}
