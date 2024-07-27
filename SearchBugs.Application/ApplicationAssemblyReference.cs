using System.Reflection;

namespace SearchBugs.Application;

public class ApplicationAssemblyReference
{
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
