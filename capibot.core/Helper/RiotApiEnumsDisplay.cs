using RiotSharp.Misc;

namespace Capibot.Core.Helper
{
    public class RiotApiEnumsDisplay
    {
        public static string GetDisplayForQueueType(string gameQueueType)
        {
            string display;
            switch (gameQueueType)
            {
                case Queue.RankedSolo5x5:
                    display = "Solo queue 5 vs 5";
                    break;
                case Queue.RankedFlexSR:
                    display = "Flex 5 vs 5";
                    break;
                case Queue.RankedFlexTT:
                    display = "Flex 3 vs 3";
                    break;
                case Queue.RankedTeam3x3:
                    display = "Ranked Team 3 vs 3";
                    break;
                case Queue.RankedTeam5x5:
                    display = "Ranked Team 5 vs 5";
                    break;
                case Queue.TeamBuilderDraftRanked5x5:
                    display = "Team 5 v 5 - Dynamic Queue - Ranked";
                    break;
                case Queue.TeamBuilderDraftUnranked5x5:
                    display = "Team 5 v 5 - Dynamic Queue - Unranked";
                    break;
                default:
                    display = string.Empty;
                    break;
            }
            return display;
        }
    }
}