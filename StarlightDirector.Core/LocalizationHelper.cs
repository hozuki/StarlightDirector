using System.Collections.Generic;

namespace StarlightDirector.Core {
    public static class LocalizationHelper {

        public static void MonitorLocalizationChange(this ILocalizable localizable) {
            if (localizable == null) {
                return;
            }
            if (Handlers.Contains(localizable)) {
                return;
            }
            Handlers.Add(localizable);
        }

        public static void UnmonitorLocalizationChange(this ILocalizable localizable) {
            if (localizable == null) {
                return;
            }
            if (!Handlers.Contains(localizable)) {
                return;
            }
            Handlers.Remove(localizable);
        }

        public static void Relocalize(LanguageManager languageManager) {
            foreach (var l in Handlers) {
                l.Localize(languageManager);
            }
        }

        private static readonly List<ILocalizable> Handlers = new List<ILocalizable>();

    }
}
