using Capibot.Core.Tools;
using Discord;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Capibot.Discord.Events
{
    public static class MessageEvent
    {
        public static void Received(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == "!admin")
            {
                bool isadmin = false;
                IEnumerable<Role> roles = e.User.Roles;
                foreach (Role role in roles)
                {
                    if (role.Name.Contains("Administrator"))
                    {
                        isadmin = true;
                    }
                }
                if (isadmin)
                {
                    e.Channel.SendMessage("Yes, you are! :D");
                }
                else
                {
                    e.Channel.SendMessage("No, you aren't :c");
                }
            }
            if (e.Message.Text == "!help")
            {
                e.Channel.SendMessage("This is a public message!");
                // Because this is a public message, 
                // the bot should send a message to the channel the message was received.
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
            e.Channel.SendMessage("Removing messages has been disabled on this server!");
            e.Channel.SendMessage("<@" + e.Message.User.Id + "> sent: " + e.Message.Text);
        }

        public static void Updated(object sender, MessageUpdatedEventArgs e)
        {

        }
    }
}
