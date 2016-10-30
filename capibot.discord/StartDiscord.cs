using Capibot.Discord.Events;
using Discord;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Capibot.Discord
{
    class StartDiscord
    {
        static void Main(string[] args)
        {

            DiscordClient client = new DiscordClient();
            EventService.LaunchEventService(client);
            client.ExecuteAndWait(async () => {
                await client.Connect(Config.GetToken(), TokenType.Bot);
            });
        }
    }
}
