using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector {
    public static class AssemblyHelper {

        [NotNull]
        [DebuggerStepThrough]
        public static Assembly GetAppAssembly() {
            return _appAssembly ?? (_appAssembly = Assembly.GetEntryAssembly());
        }

        [NotNull]
        public static string GetTitle() {
            if (_title == null) {
                var assembly = GetAppAssembly();
                var appTitleAttr = assembly.GetCustomAttribute<AssemblyTitleAttribute>();

                var title = appTitleAttr?.Title ?? assembly.GetName().Name;

                if (title == null) {
                    title = "Unknown Assembly";
                }

                _title = title;
            }

            return _title;
        }

        // https://support.microsoft.com/en-us/help/319292/how-to-embed-and-access-resources-by-using-visual-c
        [CanBeNull]
        public static Stream GetResourceStream([NotNull] string resourceName) {
            var assembly = GetAppAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);

            return stream;
        }

        [NotNull]
        public static Version GetAssemblyVersion() {
            var assembly = GetAppAssembly();
            var version = assembly.GetName().Version;

            return version;
        }

        [CanBeNull]
        private static string _title;
        [CanBeNull]
        private static Assembly _appAssembly;

    }
}
