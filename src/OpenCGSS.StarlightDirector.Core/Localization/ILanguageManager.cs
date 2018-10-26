using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Localization {
    public interface ILanguageManager {

        void SetString([CanBeNull] string key, [CanBeNull] string value);

        [NotNull]
        string GetString([CanBeNull] string key);

        [CanBeNull]
        string GetString([CanBeNull] string key, [CanBeNull] string defaultValue);

        [NotNull]
        string this[[CanBeNull] string key] {
            [NotNull]
            get;
            set;
        }

        [NotNull]
        string Language { get; }

        [NotNull]
        string DisplayName { get; }

        [NotNull]
        string CodeName { get; }

    }
}
