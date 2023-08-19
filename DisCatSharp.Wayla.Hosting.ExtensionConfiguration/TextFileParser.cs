using System.Collections.Immutable;
using DisCatSharp.Wayla.Hosting.ExtensionConfiguration.Extensions;

namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration;

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

    public IReadOnlySet<BaseExtension> Parse()
    {
        ImmutableHashSet<string> parsedExtensions = File
            .ReadAllLines(_filepath)
            .Where(line => line.StartsWith("DisCatSharp"))
            .ToImmutableHashSet();

        return AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(assembly => assembly.FullName != null && parsedExtensions.Contains(assembly.FullName.GetAssemblyNamespace()))
            .Select(assembly => assembly.GetExportedTypes().First(type => type?.BaseType == typeof(BaseExtension)))
            .Select(type => (BaseExtension)Activator.CreateInstance(type)!)
            .ToImmutableHashSet();
    }
}
