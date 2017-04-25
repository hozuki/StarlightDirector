using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace StarlightDirector.Core {
    public static class ApplicationHelper {

        [DebuggerStepThrough]
        public static Assembly GetAppAssembly() {
            return _appAssembly ?? (_appAssembly = Assembly.GetEntryAssembly());
        }

        public static string GetTitle() {
            if (_title != null) {
                return _title;
            }
            var assembly = GetAppAssembly();
            var appTitleAttr = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            _title = appTitleAttr != null ? appTitleAttr.Title : assembly.GetName().Name;
            return _title;
        }

        // https://support.microsoft.com/en-us/help/319292/how-to-embed-and-access-resources-by-using-visual-c
        public static Stream GetResourceStream(string resourceName) {
            var assembly = GetAppAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            return stream;
        }

        public static string GetAssemblyVersionString() {
            var assembly = GetAppAssembly();
            var version = assembly.GetName().Version;
            return version.ToString();
        }

        private static string _title;
        private static Assembly _appAssembly;

    }
}
