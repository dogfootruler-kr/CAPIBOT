using System;
using System.Collections.Generic;
using System.Linq;
using Capibot.Core.Helper;
using RiotSharp;
using RiotSharp.Misc;
using RiotSharp.StaticDataEndpoint;
using RiotSharp.StaticDataEndpoint.Item;
using RiotSharp.LeagueEndpoint;
using RiotSharp.SummonerEndpoint;

namespace Capibot.Core.Net
{
    public class APIWrapper
    {
        private RiotApi riotAPI;
        private StaticRiotApi staticRiotAPI;
        private ItemListStatic staticListItem = null;

        public APIWrapper(string lolApiKey)
        {
            riotAPI = RiotApi.GetDevelopmentInstance(lolApiKey);
            staticRiotAPI = StaticRiotApi.GetInstance(lolApiKey);
        }
        
        public string getItemInfo(string itemName)
        {
            try
            {
                if (itemName.ToLower() == "clearcache")
                {
                    staticListItem = staticRiotAPI.GetItems(Region.euw, ItemData.All, Language.fr_FR);
                    return "Le cache a été vidé.";
                }

                if (staticListItem == null)
                {
                    staticListItem = staticRiotAPI.GetItems(Region.euw, ItemData.All, Language.fr_FR);
                }

                int itemNbr = 0;

                int.TryParse(itemName, out itemNbr);

                List<KeyValuePair<int, ItemStatic>> retrievedItems = staticListItem.Items.Where(x => (x.Value.Name != null && x.Value.Name.ToLower().Contains(itemName.ToLower())) || (x.Key == itemNbr)).ToList();
                int listLength = retrievedItems.Count;
                string result = "";

                if (listLength == 1)
                {
                    result = string.Format("Nom : {0}\nDescription: {1}\n", retrievedItems[0].Value.Name, retrievedItems[0].Value.SanitizedDescription);
                    return result;
                }

                foreach (KeyValuePair<int, ItemStatic> item in retrievedItems)
                {
                    result += string.Format("- {0} (id: {1})\n", item.Value.Name, item.Key);
                }

                if (result.Length > 2000)
                {
                    return string.Format("Désolé nous avons trouvé trop de résultat. (Limitation 2000 caractères par Discord)", itemName);
                }

                result = !string.IsNullOrEmpty(result) ? result : "Je n'ai trouvé aucun item.";

                return result;

            } catch (RiotSharpException)
            {
                return string.Format("Désolé nous n'avons rien trouvé pour {0}, veuillez vérifier votre orthographe ou réessayer plus tard.", itemName);
            }
        }

        public string getSummonersInfo(string username)
        {
            try
            {
                List<LeaguePosition> rankedStats;
                Summoner summoner;
                
                try {
                    summoner = riotAPI.GetSummonerByName(Region.euw, username);
                }
                catch (RiotSharpException ex)
                {
                    return string.Format("Désolé nous n'avons rien trouvé pour {0}, veuillez vérifier votre orthographe ou réessayer plus tard.", username);
                }

                try {
                    rankedStats = riotAPI.GetLeaguePositions(Region.euw, summoner.Id);
                }
                catch (RiotSharpException)
                {
                    return string.Format("{0} est unranked.", username);
                }

                string result = "";

                foreach (var league in rankedStats)
                {
                    string displayQueue = RiotApiEnumsDisplay.GetDisplayForQueueType(league.QueueType);
                    string tier = league.Tier.ToString();
                    string leagueName = league.LeagueName.ToString();
                    string division = league.Rank.ToString();
                    string wins = league.Wins.ToString();
                    string losses = league.Losses.ToString();
                    string leaguePoints = league.LeaguePoints.ToString();
                    result += string.Format("{0} est dans la league {1}, {2} division {3} en {4} ({5} points). {6}W/{7}L\n", username, leagueName,
                        tier, division, displayQueue, leaguePoints, wins, losses);
                }

                return result;
            }
            catch (RiotSharpException ex)
            {
                return string.Format("Exception stacktrace: {0}", ex.StackTrace);
            }
        }
    }
}
