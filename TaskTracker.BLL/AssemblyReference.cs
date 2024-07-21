using System.Reflection;

namespace TaskTracker.BLL;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
