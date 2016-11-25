using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiotApi.Net.RestClient;
using RiotApi.Net.RestClient.Configuration;

namespace Capibot.Core.Tools
{
    public static class APIWrapper
    {
        //https://github.com/sdesyllas/RiotApi.NET
        IRiotClient riotClient = null;
        string lol_token = Config.GetToken('lol_token');
        string rl_token = Config.GetToken('rl_token');

        public APIWrapper()
        {
            if (!lol_token.IsNullOrEmpty()) {
                try {
                    riotClient = new RiotClient(lol_token);
                }
                catch (Exception ex) {
                    Console.WriteLine("{0} Exception caught.", ex);
                }
            } else {
                Console.writeLine('Legue of legends token not found.');
            }

            // if (!rl_token.IsNullOrEmpty()) {
            //     try {
            //         riotClient = new RiotClient(rl_token);
            //     }
            //     catch (Exception ex) {
            //         Console.WriteLine("{0} Exception caught.", ex);
            //     }
            // } else {
            //     Console.writeLine('Legue of legends token not found.');
            // }
        }

        public var getSummonersStat(string summonerName)
        {

        }

        public static void Text(string text)
        {
            
        }
        
    }
}
