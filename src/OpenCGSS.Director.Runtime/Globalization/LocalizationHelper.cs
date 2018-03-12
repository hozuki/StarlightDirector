using System.Collections.Generic;
using JetBrains.Annotations;
using OpenCGSS.Director.Core;

namespace OpenCGSS.Director.Globalization {
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

        public static void Relocalize([NotNull] LanguageManager languageManager) {
            Guard.ArgumentNotNull(languageManager, nameof(languageManager));

            foreach (var l in Handlers) {
                l.Localize(languageManager);
            }
        }

        private static readonly List<ILocalizable> Handlers = new List<ILocalizable>();

    }
}
