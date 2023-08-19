namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration.Extensions;

public static class DiscordClientExtensions
{
    public static void UseExtensions(this DiscordClient client, IExtensionParser parser)
    {
        ArgumentNullException.ThrowIfNull(parser, nameof(parser));
        IReadOnlySet<BaseExtension> parsedExtensions = parser.Parse();

        foreach (BaseExtension extension in parsedExtensions)
        {
            client.AddExtension(extension);
        }
    }

    public static void UseExtensions(this DiscordShardedClient shardedClient, IExtensionParser parser)
    {
        ArgumentNullException.ThrowIfNull(parser, nameof(parser));
        IReadOnlySet<BaseExtension> parsedExtensions = parser.Parse();

        foreach (BaseExtension extension in parsedExtensions)
        {
            foreach (DiscordClient client in shardedClient.ShardClients.Values)
            {
                client.AddExtension(extension);
            }
        }
    }
}
