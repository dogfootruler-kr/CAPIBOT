using System;
using System.Collections.Generic;
using System.Linq;
using Capibot.Core.Tools;
using Capibot.Core.Helper;
using RiotApi.Net.RestClient;
using RiotApi.Net.RestClient.Configuration;
using RiotApi.Net.RestClient.Helpers;
using RiotApi.Net.RestClient.Dto.League;

namespace Capibot.Core.Net
{
    public class APIWrapper
    {
        private IRiotClient riotClient;
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

            riotClient = new RiotClient(apiKey);
        }

        public string getSummonersInfo(string username)
        {
            try
            {
                var summoners = riotClient.Summoner.GetSummonersByName(RiotApiConfig.Regions.EUW, username);

                if (summoners.Count < 0)
                {
                    return String.Format("Désolé nous n'avons rien trouvé pour {0}, vueillez vérifier votre orthographe ou réessayer plus tard.", username);
                }
                long summonerId = summoners[username.ToLower()].Id;
                var summonerLeaguesByIds = riotClient.League.GetSummonerLeaguesByIds(RiotApiConfig.Regions.EUW, summonerId);

                IEnumerable<LeagueDto> leaguesOfSummoner = summonerLeaguesByIds[summonerId.ToString()];

                int nbOfLeague = leaguesOfSummoner.Count<LeagueDto>();
                int i = 0;
                string result = "";

                foreach (var league in leaguesOfSummoner)
                {
                    i++;
                    var moreInfo = league.Entries.Where(sum => sum.PlayerOrTeamName.ToLower() == username.ToLower()).First();
                    result += String.Format("{0} est dans la league {1}, {2} division {3} en {4} ({5} points). {6}W/{7}L", username, league.Name, league.Tier, moreInfo.Division, RiotApiEnumsDisplay.GetDisplayForQueueType(league.Queue), moreInfo.LeaguePoints, moreInfo.Wins, moreInfo.Losses);
                    if (nbOfLeague > i)
                    {
                        result += "\n";
                    }
                }

                return result;
            }
            catch (RiotExceptionRaiser.RiotApiException exception)
            {
                if (exception.RiotErrorCode == RiotExceptionRaiser.RiotErrorCode.DATA_NOT_FOUND)
                {
                    return String.Format("RiotApiException: {0}", exception.RiotErrorCode);
                }

                return "Undefined exception";
            }
        }
    }
}
