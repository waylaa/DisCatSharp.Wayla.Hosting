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
                Token = "Nzc5Mzk4NjA0NTY0OTg3OTc1.Gr20fa.7VbQ0I4O4M11Bo4AwMEwa385qhF738JYDvE1mo",
            })
            .UseConsoleLifetime()
            .Build();

        Host.Run();
    }
}
