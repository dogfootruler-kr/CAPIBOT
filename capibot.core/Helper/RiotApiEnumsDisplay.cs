using System;
using RiotApi.Net.RestClient.Helpers;

namespace Capibot.Core.Helper
{
    public class RiotApiEnumsDisplay
    {
        public static string GetDisplayForQueueType(Enums.GameQueueType gameQueueType)
        {
            string display;
            switch (gameQueueType)
            {
                case Enums.GameQueueType.RANKED_SOLO_5x5:
                    display = "Ranked Solo 5x5";
                    break;
                case Enums.GameQueueType.RANKED_TEAM_3x3:
                    display = "Ranked Team 3x3";
                    break;
                case Enums.GameQueueType.RANKED_TEAM_5x5:
                    display = "Ranked Team 5x5";
                    break;
                default:
                    display = String.Empty;
                    break;
            }
            return display;
        }
    }
}