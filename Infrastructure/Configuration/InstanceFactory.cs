using System.Reflection;

namespace Infrastructure.Configuration;

/// <summary>
/// Represents the instance factory.
/// </summary>
internal static class InstanceFactory
{
    /// <summary>
    /// Creates the instances of the specified type defined in the specified assemblies.
    /// </summary>
    /// <typeparam name="T">The type to instantiate.</typeparam>
    /// <param name="assemblies">The assemblies to scan for the instance of the specified type.</param>
    /// <returns>The enumerable collection of the instances of the specified type defined in the specified assemblies.</returns>
    internal static IEnumerable<T> CreateFromAssemblies<T>(params Assembly[] assemblies) =>
        assemblies
            .SelectMany(assembly => assembly.DefinedTypes)
            .Where(IsAssignableToType<T>)
            .Select(Activator.CreateInstance)
            .Cast<T>();

    private static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
        typeof(T).IsAssignableFrom(typeInfo) && !typeInfo.IsInterface && !typeInfo.IsAbstract;
}
