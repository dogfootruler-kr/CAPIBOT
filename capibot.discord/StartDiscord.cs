using Capibot.Discord.Events;
using Capibot.Core.Tools;
using Discord;
using Discord.Commands;
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
            client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
            });
            EventService.LaunchEventService(client);
            client.ExecuteAndWait(async () => {
                await client.Connect(Config.GetToken("discordToken"), TokenType.Bot);
            });
        }
    }
}
