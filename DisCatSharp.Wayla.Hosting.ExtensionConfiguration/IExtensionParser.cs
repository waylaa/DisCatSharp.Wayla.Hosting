using System.Collections.Immutable;
using System.Reflection;

namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration;

public interface IExtensionParser
{
    /// <summary>
    /// An immutable set containing extension methods that can be used either from <see cref="DiscordClient"/>
    /// or <see cref="DiscordShardedClient"/> that activate that particular extension.
    /// </summary>
    /// <returns></returns>
    public IImmutableSet<MethodInfo> Parse();
}
