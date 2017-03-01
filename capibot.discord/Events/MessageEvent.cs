using Capibot.Core.Tools;
using Discord;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Capibot.Core.Net;

namespace Capibot.Discord.Events
{
    public static class MessageEvent
    {
        public static void Received(object sender, MessageEventArgs e)
        {
            //e.Channel.SendMessage("Message recu!");
            //e.Channel.SendMessage("<@" + e.Message.User.Id + "> sent: " + e.Message.Text);
        }

        public static void Deleted(object sender, MessageEventArgs e)
        {
            //e.Channel.SendMessage("Removing messages has been disabled on this server!");
            //e.Channel.SendMessage("<@" + e.Message.User.Id + "> deleted: " + e.Message.Text);
        }

        public static void Updated(object sender, MessageUpdatedEventArgs e)
        {
            //e.Channel.SendMessage("Opopop garçon, il est interdit d'update sa phrase sur ce serveur! \\o");
            //e.Channel.SendMessage($"<@{e.User.Nickname} updated: {e.Before.Text}");
        }
    }
}
