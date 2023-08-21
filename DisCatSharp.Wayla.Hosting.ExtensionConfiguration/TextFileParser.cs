using System.Collections.Immutable;
using System.Reflection;
using DisCatSharp.Wayla.Hosting.ExtensionConfiguration.Extensions;

namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration;

/// <summary>
/// Parses namespaces from DisCatSharp's extension packages using a text file.
/// </summary>
public sealed class TextFileParser : IExtensionParser
{
    private readonly string _filepath;

    public TextFileParser(string filepath)
    {
        if (string.IsNullOrWhiteSpace(filepath))
        {
            throw new ArgumentException($"The file path of the extensions file cannot be null or whitespace.", nameof(filepath));
        }

        if (!File.Exists(filepath))
        {
            throw new ArgumentException($"Extensions file does not exist.", nameof(filepath));
        }

        _filepath = filepath;
    }

    public IImmutableSet<MethodInfo> Parse()
    {
        ImmutableHashSet<string> parsedExtensions = File
            .ReadAllLines(_filepath)
            .Where(line => line.StartsWith("DisCatSharp"))
            .ToImmutableHashSet();

        return AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(assembly => assembly.FullName is not null && parsedExtensions.Contains(assembly.FullName.GetAssemblyNamespace()))
            .SelectMany(extensionAssembly => extensionAssembly.GetExportedTypes())
            .Where(type => type.IsAbstract && type.IsSealed)
            .SelectMany(type => type.GetMethods()
                .Where(method => method.Name.Contains("Use") && method.GetParameters().Any(parameter =>
                        typeof(DiscordClient).IsAssignableFrom(parameter.ParameterType) ||
                        typeof(DiscordShardedClient).IsAssignableFrom(parameter.ParameterType))))
            .ToImmutableHashSet();
    }
}
