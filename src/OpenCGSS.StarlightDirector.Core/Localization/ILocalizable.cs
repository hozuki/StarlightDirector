using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Localization {
    public interface ILocalizable {

        void Localize([CanBeNull] ILanguageManager manager);

    }
}
