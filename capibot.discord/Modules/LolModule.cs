using Capibot.Core.Net;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Capibot.Discord.Modules
{
    public class LolModule : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _service;
        private readonly IConfigurationRoot _config;
        static APIWrapper api;
        static string lolToken;

        public LolModule(CommandService service, IConfigurationRoot config)
        {
            _service = service;
            _config = config;

            if (string.IsNullOrWhiteSpace(lolToken))
            {
                lolToken = _config["tokens:discord"];
                if (string.IsNullOrWhiteSpace(lolToken))
                    throw new Exception("Please enter your lol API token into the `_configuration.json` file found in the applications root directory.");
                api = new APIWrapper(lolToken);
            }
        }

        [Command("lol")]
        [Summary("Cherche un invocateur (euw), usage: !lol faker")]
        public async Task GetUserAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                await ReplyAsync("Vueillez spécifier votre nom de joueur. Exemple: !lol Faker");
                return;
            }

            string result = api.getSummonersInfo(username);

            await ReplyAsync(result);
        }

        [Command("item")]
        [Summary("Cherche la description d'un objet, usage: !item warmog/clearcache")]
        public async Task GetItemAsync(string itemname)
        {
            if (string.IsNullOrEmpty(itemname))
            {
                await ReplyAsync("Vueillez spécifier votre nom de joueur. Exemple: !item warmog");
                return;
            }

            string result = api.getItemInfo(itemname);

            await ReplyAsync(result);
        }
    }
}
