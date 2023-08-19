using DisCatSharp.Wayla.Hosting.ExtensionConfiguration.Extensions;
using System.Collections.Immutable;
using System.Text.Json;

namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration;

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

    public IReadOnlySet<BaseExtension> Parse()
    {
        ParsableExtensions? preferredExtensions = JsonSerializer.Deserialize<ParsableExtensions>(File.ReadAllText(_jsonFilepath));

        if (preferredExtensions == null)
        {
            return ImmutableHashSet<BaseExtension>.Empty;
        }

        Console.WriteLine(preferredExtensions);

        return AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(assembly => assembly.FullName != null && preferredExtensions.Extensions.Contains(assembly.FullName.GetAssemblyNamespace()))
            .Select(assembly => assembly.GetExportedTypes().First(type => type?.BaseType == typeof(BaseExtension)))
            .Select(type => (BaseExtension)Activator.CreateInstance(type)!)
            .ToImmutableHashSet();
    }

    private sealed record ParsableExtensions(string[] Extensions);
}
