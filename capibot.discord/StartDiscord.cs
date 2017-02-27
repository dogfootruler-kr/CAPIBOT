using Capibot.Discord.Events;
using Capibot.Core.Tools;
using Discord;

namespace Capibot.Discord
{
    class StartDiscord
    {
        static void Main(string[] args)
        {
            DiscordClient client = new DiscordClient();
            EventService.LaunchEventService(client);
            client.ExecuteAndWait(async () => {
                await client.Connect(Config.GetToken("discordToken"), TokenType.Bot);
            });
        }
    }
}
