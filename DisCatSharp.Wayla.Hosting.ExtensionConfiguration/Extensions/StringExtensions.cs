using System.Runtime.CompilerServices;

namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration.Extensions;

public static class StringExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetAssemblyNamespace(this string assemblyFullName)
        => assemblyFullName[..assemblyFullName.IndexOf(',')];
}
