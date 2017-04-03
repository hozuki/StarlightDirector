using System.Reflection;

namespace StarlightDirector.App {
    public static class ApplicationHelper {

        public static string GetTitle() {
            if (_title != null) {
                return _title;
            }
            var assembly = Assembly.GetEntryAssembly();
            var appTitleAttr = assembly.GetCustomAttribute<AssemblyTitleAttribute>();
            _title = appTitleAttr != null ? appTitleAttr.Title : assembly.GetName().Name;
            return _title;
        }

        private static string _title;

    }
}
