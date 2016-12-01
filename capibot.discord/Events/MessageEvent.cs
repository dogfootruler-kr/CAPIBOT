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
        static string[] func = {
            "!cat",
            "!help",
            "!lol Username"
        };

        public static void Received(object sender, MessageEventArgs e)
        {
            if (e.Message.Text.StartsWith("!lol"))
            {
                APIWrapper api = new APIWrapper();
                if (e.Message.Text.Length < 6)
                {
                    e.Channel.SendMessage("Vueillez spécifier votre nom de joueur. Exemple: !lol Faker");
                    return;
                }

                string summonerName = e.Message.Text.Remove(0, 5).Replace('\n', ' ');
                string result = api.getSummonersInfo(summonerName);
                e.Channel.SendMessage(result);
            }
            if (e.Message.Text == "!help")
            {
                string response = "";
                foreach (string cmd in func)
                {
                    response += String.Format("{0}\n", cmd);
                }
                e.Channel.SendMessage(response);
            }
            if (e.Message.Text == "!cat")
            {
                //Thread t = new Thread(new ParameterizedThreadStart(randomcat));
                //t.Start(e.Channel);
                string s;
                using (WebClient webclient = new WebClient())
                {
                    s = webclient.DownloadString("http://random.cat/meow");
                    int pFrom = s.IndexOf("\\/i\\/") + "\\/i\\/".Length;
                    int pTo = s.LastIndexOf("\"}");
                    string cat = s.Substring(pFrom, pTo - pFrom);
                    webclient.DownloadFile("http://random.cat/i/" + cat, "cat.png");
                    e.Channel.SendMessage("Meow!");
                    e.Channel.SendFile("cat.png");
                }
            }
        }

        public static void Deleted(object sender, MessageEventArgs e)
        {
            //e.Channel.SendMessage("Removing messages has been disabled on this server!");
            //e.Channel.SendMessage("<@" + e.Message.User.Id + "> sent: " + e.Message.Text);
        }

        public static void Updated(object sender, MessageUpdatedEventArgs e)
        {
            //e.Channel.SendMessage("Opopop garçon, il est interdit d'update sa phrase sur ce serveur! \\o");
            //e.Channel.SendMessage($"<@{e.User.Nickname} sent: {e.Before.Text}");
        }
    }
}
