using Capibot.Discord.Events;
using Capibot.Core.Tools;
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
                await client.Connect(Config.GetToken('discord_token'), TokenType.Bot);
            });
        }
    }
}
