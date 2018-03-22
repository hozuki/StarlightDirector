using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Globalization {
    public interface ILocalizable {

        void Localize([CanBeNull] LanguageManager manager);

    }
}
