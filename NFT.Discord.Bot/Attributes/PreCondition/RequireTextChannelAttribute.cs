using Discord.Commands;

namespace NFT.DiscordBot.Attributes.PreCondition
{
    public class RequireTextChannelAttribute : PreconditionAttribute
    {
        private readonly string _ChannelName;

        public RequireTextChannelAttribute(string channelName)
        {
            _ChannelName = channelName;
        }

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            if(context.Channel.Name == _ChannelName)
            {
                return Task.FromResult(PreconditionResult.FromSuccess());
            }
            return Task.FromResult(PreconditionResult.FromError("Can not run this command in this text channel"));
        }
    }
}
