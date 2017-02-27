using System;
using System.Collections.Generic;
using System.Linq;
using Capibot.Core.Tools;
using Capibot.Core.Helper;
using RiotSharp;
using RiotSharp.StaticDataEndpoint;
using RiotSharp.LeagueEndpoint;
using RiotSharp.SummonerEndpoint;

namespace Capibot.Core.Net
{
    public class APIWrapper
    {
        private RiotApi riotAPI;
        private StaticRiotApi staticRiotAPI;
        private Dictionary<int, ItemStatic> staticListItem = null;

        public APIWrapper()
        {
            string apiKey = Config.GetToken("lolToken");
            
            if (string.IsNullOrEmpty(apiKey))
            {
                // token name: lolToken
                Console.WriteLine("If you want to use the league of legend API, you need to put your token in your environment.");
                throw new Exception();
            }

            riotAPI = RiotApi.GetInstance(apiKey);
            staticRiotAPI = StaticRiotApi.GetInstance(apiKey);
        }
        
        public string getItemInfo(string itemName)
        {
            try
            {
                if (staticListItem == null)
                {
                    staticListItem = staticRiotAPI.GetItems(Region.euw, ItemData.all, Language.fr_FR).Items;
                }

                int itemNbr = 0;

                Int32.TryParse(itemName, out itemNbr);

                List<KeyValuePair<int, ItemStatic>> retrievedItems = staticListItem.Where(x => (x.Value.Name != null && x.Value.Name.ToLower().Contains(itemName.ToLower())) || (x.Key == itemNbr)).ToList();
                int listLength = retrievedItems.Count;
                string result = "";
                int i = 0;

                if (listLength == 1)
                {
                    result = String.Format("Name : {0}\nDescription: {1}\n", retrievedItems[0].Value.Name, retrievedItems[0].Value.SanitizedDescription);
                    return result;
                }

                foreach (KeyValuePair<int, ItemStatic> item in retrievedItems)
                {
                    result += String.Format("- {0} (id: {1})", item.Value.Name, item.Key);
                    i++;
                    if (i < listLength)
                    {
                        result += "\n";
                    }
                }

                if (result.Length > 2000)
                {
                    return String.Format("Désolé nous n'avons rien trouvé trop de résultat. (Limitation 2000 caractères par Discord)", itemName);
                }

                result = !string.IsNullOrEmpty(result) ? result : "Je n'ai trouvé aucun item.";

                return result;

            } catch (RiotSharpException ex)
            {
                return String.Format("Désolé nous n'avons rien trouvé pour {0}, veuillez vérifier votre orthographe ou réessayer plus tard.", itemName);
            }
        }

        public string getSummonersInfo(string username)
        {
            try
            {
                List<League> rankedStats;
                Summoner summoner;
                
                try {
                    summoner = riotAPI.GetSummoner(Region.euw, username);
                }
                catch (RiotSharpException ex) {
                    return String.Format("Désolé nous n'avons rien trouvé pour {0}, veuillez vérifier votre orthographe ou réessayer plus tard.", username);
                }

                try {
                    rankedStats = summoner.GetLeagues();
                }
                catch (RiotSharpException ex) {
                    return String.Format("{0} est unranked.", username);
                }

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
                return String.Format("Exception stacktrace: {0}", ex.StackTrace);
            }
        }
    }
}
