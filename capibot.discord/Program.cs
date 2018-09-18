using System.Threading.Tasks;

namespace Capibot.Discord
{
    class Program
    {
        public static Task Main(string[] args)
            => StartDiscord.RunAsync(args);
    }
}