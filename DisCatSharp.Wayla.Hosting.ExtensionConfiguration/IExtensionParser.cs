namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration;

public interface IExtensionParser
{
    public IReadOnlySet<BaseExtension> Parse();
}
