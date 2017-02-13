using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capibot.Discord.Events
{
    public class EventService
    {
        public static void LaunchEventService(DiscordClient client)
        {
            #region MessageEvent
            client.MessageReceived += MessageEvent.Received;

            client.MessageDeleted += MessageEvent.Deleted;

            client.MessageUpdated += MessageEvent.Updated;

            #endregion
        }
    }
}
