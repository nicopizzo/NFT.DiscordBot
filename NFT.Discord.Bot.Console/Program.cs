using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NFT.DiscordBot;
using NFT.DiscordBot.Interfaces;

var config = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .Build();

ServiceProvider serviceProvider = new ServiceCollection()
    .AddDiscordBot(config)
    .BuildServiceProvider();

var bot = serviceProvider.GetRequiredService<IDiscordBot>();
bot.BotReady += bot_Start;
await bot.StartAsync();

await Task.Delay(-1);


void bot_Start(object? sender, EventArgs e)
{
    Console.WriteLine("bot started");
}