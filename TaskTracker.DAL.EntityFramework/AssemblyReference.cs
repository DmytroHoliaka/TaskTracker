using System.Reflection;

namespace TaskTracker.DAL.EntityFramework;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
