using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;

namespace Capibot.Core.Tools
{
    public static class Config
    {
        public static string GetToken(string tokenName)
        {
            string apiKey = Environment.GetEnvironmentVariable(tokenName, EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(apiKey))
            {
                var section = ConfigurationManager.GetSection("appSettings") as NameValueCollection;
                apiKey = section[tokenName];

                if (string.IsNullOrEmpty(apiKey))
                {
                    // token name: discordToken
                    Console.WriteLine("If you want to use the discord API, you need to put your token in your environment.");
                    throw new Exception();
                }
            }

            return apiKey;
        }

    }
}
