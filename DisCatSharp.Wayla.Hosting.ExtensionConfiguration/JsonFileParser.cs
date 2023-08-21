using DisCatSharp.Wayla.Hosting.ExtensionConfiguration.Extensions;
using System.Collections.Immutable;
using System.Reflection;
using System.Text.Json;

namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration;

/// <summary>
/// Parses namespaces from DisCatSharp's extension packages using a JSON file.
/// </summary>
public sealed class JsonFileParser : IExtensionParser
{
    private readonly string _jsonFilepath;

    public JsonFileParser(string jsonFilepath)
    {
        if (!string.IsNullOrWhiteSpace(jsonFilepath))
        {
            throw new ArgumentException($"The file path of the extensions file cannot be null or whitespace.", nameof(jsonFilepath));
        }

        if (!File.Exists(jsonFilepath))
        {
            throw new ArgumentException($"Extensions file does not exist.", nameof(jsonFilepath));
        }

        _jsonFilepath = jsonFilepath;
    }

    public IImmutableSet<MethodInfo> Parse()
    {
        JsonDocument? json = JsonSerializer.Deserialize<JsonDocument>(File.ReadAllText(_jsonFilepath));

        if (json is null)
        {
            return ImmutableHashSet<MethodInfo>.Empty;
        }

        IReadOnlyList<string>? parsedExtensions = json.Deserialize<IReadOnlyList<string>>();

        if (parsedExtensions is null)
        {
            return ImmutableHashSet<MethodInfo>.Empty;
        }

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
