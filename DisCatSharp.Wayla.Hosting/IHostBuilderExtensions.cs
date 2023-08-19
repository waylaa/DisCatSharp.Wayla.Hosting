using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DisCatSharp.Wayla.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder AddDiscordHostedClient<THostedClient>(
        this IHostBuilder hostBuilder,
        Func<DiscordConfiguration> configureClient)
        where THostedClient : class, IHostedService
        => hostBuilder.ConfigureServices(services =>
        {
            services
            .AddSingleton(new DiscordClient(configureClient()))
            .AddHostedService<THostedClient>();
        });

    public static IHostBuilder AddDiscordHostedClient<THostedClient>(
        this IHostBuilder hostBuilder,
        Action<HostBuilderContext, DiscordConfiguration> configureClientWithContext)
        where THostedClient : class, IHostedService
        => hostBuilder.ConfigureServices((hostContext, services) =>
        {
            DiscordConfiguration configuration = new();
            configureClientWithContext(hostContext, configuration);

            services
            .AddSingleton(new DiscordClient(configuration))
            .AddHostedService<THostedClient>();
        });

    public static IHostBuilder AddDiscordHostedClient<THostedClient>(
        this IHostBuilder hostBuilder,
        Action<IConfiguration, DiscordConfiguration> configureClientWithConfiguration)
        where THostedClient : class, IHostedService
        => hostBuilder.ConfigureServices((ctx, services) =>
        {
            DiscordConfiguration configuration = new();
            configureClientWithConfiguration(ctx.Configuration, configuration);

            services
            .AddSingleton(new DiscordClient(configuration))
            .AddHostedService<THostedClient>();
        });

    public static IHostBuilder AddDiscordHostedShardedClient<THostedShardedClient>(
        this IHostBuilder hostBuilder,
        Func<DiscordConfiguration> configureShardedClient)
        where THostedShardedClient : class, IHostedService
        => hostBuilder.ConfigureServices(services =>
        {
            services
            .AddSingleton(new DiscordShardedClient(configureShardedClient()))
            .AddHostedService<THostedShardedClient>();
        });

    public static IHostBuilder AddDiscordHostedShardedClient<THostedClient>(
        this IHostBuilder hostBuilder,
        Action<HostBuilderContext, DiscordConfiguration> configureShardedClientWithContext)
        where THostedClient : class, IHostedService
        => hostBuilder.ConfigureServices((hostContext, services) =>
        {
            DiscordConfiguration configuration = new();
            configureShardedClientWithContext(hostContext, configuration);

            services
            .AddSingleton(new DiscordShardedClient(configuration))
            .AddHostedService<THostedClient>();
        });

    public static IHostBuilder AddDiscordHostedShardedClient<THostedClient>(
        this IHostBuilder hostBuilder,
        Action<IConfiguration, DiscordConfiguration> configureShardedClientWithConfiguration)
        where THostedClient : class, IHostedService
        => hostBuilder.ConfigureServices((ctx, services) =>
        {
            DiscordConfiguration configuration = new();
            configureShardedClientWithConfiguration(ctx.Configuration, configuration);

            services
            .AddSingleton(new DiscordShardedClient(configuration))
            .AddHostedService<THostedClient>();
        });
}
