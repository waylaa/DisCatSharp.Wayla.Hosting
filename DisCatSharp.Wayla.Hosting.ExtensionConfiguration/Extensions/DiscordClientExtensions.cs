using System.Collections.Immutable;
using System.Reflection;

namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration.Extensions;

public static class DiscordClientExtensions
{
    public static void UseExtensions(this DiscordClient client, IExtensionParser parser)
    {
        ArgumentNullException.ThrowIfNull(parser, nameof(parser));
        IImmutableSet<MethodInfo> extensionActivationMethods = parser.Parse();

        foreach (MethodInfo method in extensionActivationMethods
            .Where(x => x.GetParameters().Any(o => typeof(DiscordClient).IsAssignableTo(o.ParameterType))))
        {
            method.Invoke(null, new object[] { client });
        }
    }

    public static void UseExtensions(this DiscordShardedClient shardedClient, IExtensionParser parser)
    {
        ArgumentNullException.ThrowIfNull(parser, nameof(parser));
        IImmutableSet<MethodInfo> extensionActivationMethods = parser.Parse();

        foreach (MethodInfo method in extensionActivationMethods
            .Where(x => x.GetParameters().Any(o => typeof(DiscordShardedClient).IsAssignableTo(o.ParameterType))))
        {
            method.Invoke(null, new object[] { shardedClient });
        }
    }
}
