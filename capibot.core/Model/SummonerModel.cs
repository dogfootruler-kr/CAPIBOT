using System.Collections.Generic;
using RiotApi.Net.RestClient.Dto.Game;
using RiotApi.Net.RestClient.Dto.League;
using RiotApi.Net.RestClient.Dto.Summoner;
using RiotApi.Net.RestClient.Dto.Stats;
using RiotApi.Net.RestClient.Dto.LolStaticData.Champion;
using RiotApi.Net.RestClient.Dto.Stats.Generic;

namespace Capibot.Core.Model
{
    public class SummonerModel
    {
        public string SummonerKey { get; set; }
        public SummonerDto SummonerDto { get; set; }
        public List<RecentGamesDto.GameDto> RecentGames { get; set; }
        public List<LeagueModel> LeagueModels { get; set; }
        public string ProfileImagePath { get; set; }
        public class LeagueModel
        {
            public string LeagueKey { get; set; }
            public LeagueDto LeagueDto { get; set; }
            public string LeagueName { get; set; }
        }
        public class PlayedChampionModel
        {
            public AggregatedStatsDto RankedStats { get; set; }
            public ChampionDto StaticChampion { get; internal set; }
        }
        public PlayerStatsSummaryListDto PlayerStats { get; set; }
        public List<PlayedChampionModel> PlayedChampions { get; set; }
    }
}