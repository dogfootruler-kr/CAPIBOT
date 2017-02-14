using System;
using System.Collections.Generic;
using System.Linq;
using Capibot.Core.Tools;
using Capibot.Core.Helper;
using RiotSharp;

namespace Capibot.Core.Net
{
    public class APIWrapper
    {
        private RiotApi riotClient;
        public APIWrapper()
        {
            string apiKey = Config.GetToken("lolToken");

            //string apiKey = Config.GetToken("lol_token");
            if (string.IsNullOrEmpty(apiKey))
            {
                // token name: lolToken
                Console.WriteLine("If you want to use the league of legend API, you need to put your token in your environment.");
                throw new Exception();
            }

            riotClient = RiotApi.GetInstance(apiKey);
        }

        public string getSummonersInfo(string username)
        {
            try
            {
                RiotSharp.SummonerEndpoint.Summoner summoner = riotClient.GetSummoner(Region.euw, username);
                List<RiotSharp.LeagueEndpoint.League> rankedStats = summoner.GetLeagues();
                string result = "";
                int i = 0;
                int nbOfLeague = rankedStats.Count();

                foreach (var league in rankedStats)
                {
                    string displayQueue = RiotApiEnumsDisplay.GetDisplayForQueueType(league.Queue);
                    string tier = league.Tier.ToString();
                    string leagueName = league.Name.ToString();
                    string division = league.Entries[0].Division.ToString();
                    string wins = league.Entries[0].Wins.ToString();
                    string losses = league.Entries[0].Losses.ToString();
                    string leaguePoints = league.Entries[0].LeaguePoints.ToString();
                    result += String.Format("{0} est dans la league {1}, {2} division {3} en {4} ({5} points). {6}W/{7}L", username, leagueName,
                        tier, division, displayQueue, leaguePoints, wins, losses);
                    i++;
                    if (nbOfLeague > i)
                    {
                        result += "\n";
                    }
                }

                return result;
            }
            catch (RiotSharpException ex)
            {
                return String.Format("Désolé nous n'avons rien trouvé pour {0}, veuillez vérifier votre orthographe ou réessayer plus tard.", username);
            }
        }
    }
}
