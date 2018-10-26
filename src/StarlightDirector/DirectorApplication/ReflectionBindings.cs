using System.Reflection;

namespace OpenCGSS.StarlightDirector.DirectorApplication {
    internal static class ReflectionBindings {

        public const BindingFlags PrivateInstance = BindingFlags.Instance | BindingFlags.NonPublic;
        public const BindingFlags PublicInstance = BindingFlags.Instance | BindingFlags.Public;

    }
}
