using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Core {
    public static class ApplicationHelper {

        [DebuggerStepThrough]
        [NotNull]
        public static Assembly GetAppAssembly() {
            return _appAssembly ?? (_appAssembly = Assembly.GetEntryAssembly());
        }

        [NotNull]
        public static string GetTitle() {
            if (_title != null) {
                return _title;
            }

            var assembly = GetAppAssembly();
            var appTitleAttr = assembly.GetCustomAttribute<AssemblyTitleAttribute>();

            _title = appTitleAttr != null ? appTitleAttr.Title : assembly.GetName().Name;

            return _title;
        }

        [CanBeNull]
        // https://support.microsoft.com/en-us/help/319292/how-to-embed-and-access-resources-by-using-visual-c
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

        private static string _title;
        private static Assembly _appAssembly;

    }
}
