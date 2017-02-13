using System;
using RiotSharp;

namespace Capibot.Core.Helper
{
    public class RiotApiEnumsDisplay
    {
        public static string GetDisplayForQueueType(RiotSharp.Queue gameQueueType)
        {
            string display;
            switch (gameQueueType)
            {
                case Queue.RankedSolo5x5:
                    display = "Solo queue 5 vs 5";
                    break;
                case Queue.RankedFlexSR:
                    display = "Ranked Summoner's rift";
                    break;
                case Queue.RankedFlexTT:
                    display = "Ranked Flex Twisted Treeline";
                    break;
                case Queue.RankedTeam3x3:
                    display = "Ranked Team 3x3";
                    break;
                case Queue.RankedTeam5x5:
                    display = "Ranked Team 5x5";
                    break;
                case Queue.TeamBuilderDraftRanked5x5:
                    display = "Team 5 v 5 - Dynamic Queue - Ranked";
                    break;
                case Queue.TeamBuilderDraftUnranked5x5:
                    display = "Team 5 v 5 - Dynamic Queue - Unranked";
                    break;
                default:
                    display = String.Empty;
                    break;
            }
            return display;
        }
    }
}