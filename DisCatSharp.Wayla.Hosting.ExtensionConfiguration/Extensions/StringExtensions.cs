using System.Runtime.CompilerServices;

namespace DisCatSharp.Wayla.Hosting.ExtensionConfiguration.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Gets the namespace from the full name of an assembly.
    /// </summary>
    /// <param name="assemblyFullName">The full name of the assembly.</param>
    /// <returns>[<see cref="string"/>] The namespace of the assembly.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetAssemblyNamespace(this string assemblyFullName)
        => assemblyFullName[..assemblyFullName.IndexOf(',')];
}
