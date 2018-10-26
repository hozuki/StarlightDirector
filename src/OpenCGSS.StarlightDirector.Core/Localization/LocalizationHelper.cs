using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Localization {
    public static class LocalizationHelper {

        public static void MonitorLocalizationChange([CanBeNull] this ILocalizable localizable) {
            if (localizable == null) {
                return;
            }

            if (Handlers.Contains(localizable)) {
                return;
            }

            Handlers.Add(localizable);
        }

        public static void UnmonitorLocalizationChange([CanBeNull] this ILocalizable localizable) {
            if (localizable == null) {
                return;
            }

            if (!Handlers.Contains(localizable)) {
                return;
            }

            Handlers.Remove(localizable);
        }

        public static void Relocalize([NotNull] ILanguageManager languageManager) {
            Guard.ArgumentNotNull(languageManager, nameof(languageManager));

            foreach (var l in Handlers) {
                l.Localize(languageManager);
            }
        }

        [NotNull]
        public static string GetUserLanguageName() {
            return CultureInfo.CurrentUICulture.Name;
        }

        [NotNull, ItemNotNull]
        private static readonly List<ILocalizable> Handlers = new List<ILocalizable>();

    }
}
