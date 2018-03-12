using JetBrains.Annotations;

namespace OpenCGSS.Director.Globalization {
    public interface ILocalizable {

        void Localize([NotNull] LanguageManager manager);

    }
}
