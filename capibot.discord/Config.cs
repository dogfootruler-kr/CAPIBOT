using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Capibot.Discord
{
    public static class Config
    {
        public static string GetToken()
        {
            var section = ConfigurationManager.GetSection("DiscordToken") as NameValueCollection;
            return section["token"];
        }

    }
}
