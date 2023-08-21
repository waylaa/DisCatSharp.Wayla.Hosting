using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DisCatSharp.Wayla.Hosting;

public static class IHostBuilderExtensions
{
    /// <summary>
    /// Adds a class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/> as a long-running service
    /// with the intention of using <see cref="DiscordClient"/>.
    /// </summary>
    /// <typeparam name="THostedClient">The type of the class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/>.</typeparam>
    /// <param name="hostBuilder">The current <see cref="IHostBuilder"/> context.</param>
    /// <param name="configureClient">A delegate to configure <see cref="DiscordClient"/>.</param>
    /// <returns>[<see cref="IHostBuilder"/>] The same instance of <see cref="IHostBuilder"/> for chaining.</returns>
    public static IHostBuilder AddDiscordHostedClient<THostedClient>(
        this IHostBuilder hostBuilder,
        Func<DiscordConfiguration> configureClient)
        where THostedClient : class, IHostedService
        => hostBuilder.ConfigureServices((hostContext, services) =>
        {
            services
            .AddSingleton(new DiscordClient(configureClient()))
            .AddHostedService<THostedClient>();
        });

    /// <summary>
    /// Adds a class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/> as a long-running service
    /// with the intention of using <see cref="DiscordClient"/>.
    /// </summary>
    /// <typeparam name="THostedClient">The type of the class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/>.</typeparam>
    /// <param name="hostBuilder">The current <see cref="IHostBuilder"/> context.</param>
    /// <param name="configureClientWithContext">A delegate to configure <see cref="DiscordClient"/>.</param>
    /// <returns>[<see cref="IHostBuilder"/>] The same instance of <see cref="IHostBuilder"/> for chaining.</returns>
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

    /// <summary>
    /// Adds a class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/> as a long-running service
    /// with the intention of using <see cref="DiscordClient"/>.
    /// </summary>
    /// <typeparam name="THostedClient">The type of the class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/>.</typeparam>
    /// <param name="hostBuilder">The current <see cref="IHostBuilder"/> context.</param>
    /// <param name="configureClientWithConfiguration">A delegate to configure <see cref="DiscordClient"/>.</param>
    /// <returns>[<see cref="IHostBuilder"/>] The same instance of <see cref="IHostBuilder"/> for chaining.</returns>
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

    /// <summary>
    /// Adds a class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/> as a long-running service
    /// with the intention of using <see cref="DiscordShardedClient"/>.
    /// </summary>
    /// <typeparam name="THostedShardedClient">The type of the class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/>.</typeparam>
    /// <param name="hostBuilder">The current <see cref="IHostBuilder"/> context.</param>
    /// <param name="configureShardedClient">A delegate to configure <see cref="DiscordShardedClient"/>.</param>
    /// <returns>[<see cref="IHostBuilder"/>] The same instance of <see cref="IHostBuilder"/> for chaining.</returns>
    public static IHostBuilder AddDiscordHostedShardedClient<THostedShardedClient>(
        this IHostBuilder hostBuilder,
        Func<DiscordConfiguration> configureShardedClient)
        where THostedShardedClient : class, IHostedService
        => hostBuilder.ConfigureServices((hostContext, services) =>
        {
            services
            .AddSingleton(new DiscordShardedClient(configureShardedClient()))
            .AddHostedService<THostedShardedClient>();
        });

    /// <summary>
    /// Adds a class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/> as a long-running service
    /// with the intention of using <see cref="DiscordShardedClient"/>.
    /// </summary>
    /// <typeparam name="THostedShardedClient">The type of the class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/>.</typeparam>
    /// <param name="hostBuilder">The current <see cref="IHostBuilder"/> context.</param>
    /// <param name="configureShardedClientWithContext">A delegate to configure <see cref="DiscordShardedClient"/>.</param>
    /// <returns>[<see cref="IHostBuilder"/>] The same instance of <see cref="IHostBuilder"/> for chaining.</returns>
    public static IHostBuilder AddDiscordHostedShardedClient<THostedShardedClient>(
        this IHostBuilder hostBuilder,
        Action<HostBuilderContext, DiscordConfiguration> configureShardedClientWithContext)
        where THostedShardedClient : class, IHostedService
        => hostBuilder.ConfigureServices((hostContext, services) =>
        {
            DiscordConfiguration configuration = new();
            configureShardedClientWithContext(hostContext, configuration);

            services
            .AddSingleton(new DiscordShardedClient(configuration))
            .AddHostedService<THostedShardedClient>();
        });

    /// <summary>
    /// Adds a class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/> as a long-running service
    /// with the intention of using <see cref="DiscordShardedClient"/>.
    /// </summary>
    /// <typeparam name="THostedShardedClient">The type of the class implementing <see cref="IHostedService"/> or <see cref="BackgroundService"/>.</typeparam>
    /// <param name="hostBuilder">The current <see cref="IHostBuilder"/> context.</param>
    /// <param name="configureShardedClientWithConfiguration">A delegate to configure <see cref="DiscordShardedClient"/>.</param>
    /// <returns>[<see cref="IHostBuilder"/>] The same instance of <see cref="IHostBuilder"/> for chaining.</returns>
    public static IHostBuilder AddDiscordHostedShardedClient<THostedShardedClient>(
        this IHostBuilder hostBuilder,
        Action<IConfiguration, DiscordConfiguration> configureShardedClientWithConfiguration)
        where THostedShardedClient : class, IHostedService
        => hostBuilder.ConfigureServices((ctx, services) =>
        {
            DiscordConfiguration configuration = new();
            configureShardedClientWithConfiguration(ctx.Configuration, configuration);

            services
            .AddSingleton(new DiscordShardedClient(configuration))
            .AddHostedService<THostedShardedClient>();
        });
}
