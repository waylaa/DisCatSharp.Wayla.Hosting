using DisCatSharp.EventArgs;
using DisCatSharp.Lavalink;
using DisCatSharp.Wayla.Hosting.ExtensionConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Immutable;

namespace DisCatSharp.Wayla.Hosting.Testing;

internal sealed class ClientExample : BackgroundService
{
    private readonly DiscordClient _client = Program.Host.Services.GetRequiredService<DiscordClient>();

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _client.Ready += OnReady;
        // Initialize extensions, commands, etc.

        return Task.CompletedTask;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await _client.ConnectAsync();
        await base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _client.Ready -= OnReady;

        await _client.DisconnectAsync();
        _client.Dispose();

        await base.StopAsync(cancellationToken);
    }

    private static Task OnReady(DiscordClient client, ReadyEventArgs args)
    {
        client.UseLavalink();

        string extensionsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "discatsharp_extensions.txt");
        TextFileParser parser = new(extensionsFile);
        ImmutableHashSet<BaseExtension> extensions = (ImmutableHashSet<BaseExtension>)parser.Parse();

        Console.WriteLine($"Loaded {extensions.Count} extensions");
        return Task.CompletedTask;
    }
}
