using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace DisCatSharp.Wayla.Hosting.Testing;

internal sealed class Program
{
    [NotNull]
    internal static IHost? Host { get; private set; }

    private static void Main()
    {
        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(configuration =>
            {

            })
            .ConfigureLogging(loggingBuilder =>
            {

            })
            .ConfigureServices((ctx, services) =>
            {

            })
            .AddDiscordHostedClient<ClientExample>(() => new DiscordConfiguration
            {
                Token = "TOKEN",
            })
            .UseConsoleLifetime()
            .Build();

        Host.Run();
    }
}
