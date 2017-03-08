using Capibot.Discord.Events;
using Capibot.Core.Tools;
using Discord;
using Discord.Commands;
using Capibot.Core.Net;

namespace Capibot.Discord
{
    class StartDiscord
    {
        static DiscordClient client = new DiscordClient();
        static APIWrapper api = new APIWrapper();

        static void Main(string[] args)
        {
            EventService.LaunchEventService(client);

            client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.HelpMode = HelpMode.Public;
            });

            InitialiseCommands();

            client.ExecuteAndWait(async () => {
                await client.Connect(Config.GetToken("discordToken"), TokenType.Bot);
            });
        }

        private static void InitialiseCommands()
        {
            client.GetService<CommandService>().CreateCommand("lol")
                 .Description("Récupère le classement League Of Legends d'un joueur.")
                 .Parameter("userName", ParameterType.Required)
                 .Do(async e =>
                 {
                     string userName = e.GetArg(0);
                     if (string.IsNullOrEmpty(userName))
                     {
                         await e.Channel.SendMessage("Vueillez spécifier votre nom de joueur. Exemple: !lol Faker");
                         return;
                     }

                     string result = api.getSummonersInfo(userName);
                     await e.Channel.SendMessage(result);
                 });

            client.GetService<CommandService>().CreateCommand("item")
                 .Description("Récupère la description d'un objet de League Of Legends. (!item clearcache pour mettre a jour le cache)")
                 .Parameter("itemName/itemID", ParameterType.Required)
                 .Do(async e =>
                 {
                     string itemName = e.GetArg(0);
                     if (string.IsNullOrEmpty(itemName))
                     {
                         await e.Channel.SendMessage("Vueillez spécifier votre nom de joueur. Exemple: !item warmog");
                         return;
                     }

                     string result = api.getItemInfo(itemName);
                     await e.Channel.SendMessage(result);
                 });
        }
    }
}
