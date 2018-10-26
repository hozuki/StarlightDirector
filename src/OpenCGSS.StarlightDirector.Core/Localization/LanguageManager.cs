using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Localization {
    public sealed class LanguageManager : ILanguageManager {

        // ReSharper disable once NotNullMemberIsNotInitialized
        private LanguageManager([NotNull] string language) {
            Guard.NotNullOrEmpty(language, nameof(language));

            Language = language;
        }

        public void SetString(string key, string value) {
            if (key == null) {
                return;
            }

            value = value ?? string.Empty;

            var indexOfBackslash = value.IndexOf('\\');

            if (indexOfBackslash >= 0) {
                value = Unescape(value, indexOfBackslash);
            }

            _translations[key] = value;
        }

        /// <summary>
        /// Attempt to retrieve a localized string. If <see cref="key"/> is not found, return <see langword="null"/>.
        /// </summary>
        /// <param name="key">The key of local string.</param>
        /// <returns>Retrieved locale string, or <see langword="null"/>.</returns>
        [CanBeNull]
        public static string TryGetString([NotNull] string key) {
            Guard.ArgumentNotNull(key, nameof(key));

            return Current?.GetString(key, null);
        }

        /// <summary>
        /// Attempt to retrieve a localized string. If <see cref="key"/> is not found, return <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="key">The key of local string.</param>
        /// <returns>Retrieved locale string, or <see cref="string.Empty"/>.</returns>
        public string GetString(string key) {
            Guard.ArgumentNotNull(key, nameof(key));

            var gotString = GetString(key, string.Empty);

            Debug.Assert(gotString != null, nameof(gotString) + " != null");

            return gotString;
        }

        public string GetString(string key, string defaultValue) {
            if (key == null) {
                return null;
            }

            _translations.TryGetValue(key, out var value);

            return value ?? defaultValue;
        }

        public string this[string key] {
            [NotNull]
            get => GetString(key);
            set => SetString(key, value);
        }

        public string Language { get; }

        public string DisplayName { get; private set; }

        public string CodeName { get; private set; }

        [CanBeNull]
        public static ILanguageManager Current { get; set; }

        [NotNull]
        public static ILanguageManager CreateFromFile([NotNull] string fileName, [NotNull] string language) {
            Guard.ArgumentNotNull(fileName, nameof(fileName));
            Guard.ArgumentNotNull(language, nameof(language));
            Guard.FileExists(fileName);

            using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                return CreateFromFile(fileStream, language);
            }
        }

        [NotNull]
        public static ILanguageManager CreateFromFile([NotNull] FileStream fileStream, [NotNull] string language) {
            Guard.ArgumentNotNull(fileStream, nameof(fileStream));
            Guard.ArgumentNotNull(language, nameof(language));

            using (var reader = new StreamReader(fileStream)) {
                return CreateFromFile(reader, language);
            }
        }

        [NotNull]
        public static ILanguageManager CreateFromFile([NotNull] StreamReader reader, [NotNull] string language) {
            Guard.NotNull(reader, nameof(reader));
            Guard.NotNullOrEmpty(language, nameof(language));

            var manager = CreateNewNotRecorded(language);

            FillDictionary(reader, manager);

            return manager;
        }

        [NotNull]
        public static ILanguageManager LoadOrCreateFromFile([NotNull] string fileName, [NotNull] string language) {
            Guard.ArgumentNotNull(fileName, nameof(fileName));
            Guard.ArgumentNotNull(language, nameof(language));
            Guard.FileExists(fileName);

            using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                return LoadOrCreateFromFile(fileStream, language);
            }
        }

        [NotNull]
        public static ILanguageManager LoadOrCreateFromFile([NotNull] FileStream fileStream, [NotNull] string language) {
            Guard.ArgumentNotNull(fileStream, nameof(fileStream));
            Guard.ArgumentNotNull(language, nameof(language));

            using (var reader = new StreamReader(fileStream)) {
                return LoadOrCreateFromFile(reader, language);
            }
        }

        [NotNull]
        public static ILanguageManager LoadOrCreateFromFile([NotNull] StreamReader reader, [NotNull] string language) {
            Guard.NotNull(reader, nameof(reader));
            Guard.NotNullOrEmpty(language, nameof(language));

            var manager = GetOrCreate(language);

            FillDictionary(reader, manager);

            return manager;
        }

        [CanBeNull]
        [ContractAnnotation("language:null => null; language:notnull => notnull")]
        private static LanguageManager GetOrCreate([CanBeNull] string language) {
            if (language == null) {
                return null;
            }

            if (Managers.ContainsKey(language)) {
                return Managers[language];
            }

            var manager = new LanguageManager(language);

            Managers[language] = manager;

            return manager;
        }

        [CanBeNull]
        [ContractAnnotation("language:null => null; language:notnull => notnull")]
        private static LanguageManager CreateNewNotRecorded([CanBeNull] string language) {
            if (language == null) {
                return null;
            }

            var manager = new LanguageManager(language);

            return manager;
        }

        private static void FillDictionary([NotNull] StreamReader reader, [NotNull] LanguageManager manager) {
            Guard.NotNull(reader, nameof(reader));
            Guard.NotNull(manager, nameof(manager));

            while (!reader.EndOfStream) {
                var line = reader.ReadLine();

                if (string.IsNullOrEmpty(line)) {
                    continue;
                }

                if (line[0] == '#') {
                    continue;
                }

                var eqPos = line.IndexOf('=');

                if (eqPos <= 0) {
                    continue;
                }

                var key = line.Substring(0, eqPos);
                var value = line.Substring(eqPos + 1);

                manager[key] = value;
            }

            manager.DisplayName = manager.GetString("lang.display_name", null) ?? "Neutral";
            manager.CodeName = manager.GetString("lang.code_name", null) ?? "neutral";
        }

        [NotNull]
        private static string Unescape([NotNull] string str, int searchStart) {
            var newLine = Environment.NewLine;
            var newLineLength = newLine.Length;
            var searchIndex = searchStart;

            var sb = new StringBuilder(str);
            var len = sb.Length;

            while (searchIndex < len) {
                if (searchIndex != '\\') {
                    continue;
                }

                if (searchIndex >= len - 1) {
                    break;
                }

                if (sb[searchIndex + 1] == '\\') {
                    sb.Replace(@"\\", @"\", searchIndex, 1);

                    len = sb.Length;

                    searchIndex += (2 - 1);
                } else if (sb[searchIndex + 1] == 'n') {
                    sb.Replace(@"\n", newLine, searchIndex, 1);

                    len = sb.Length;

                    searchIndex += (newLineLength - 1);
                } else {
                    ++searchIndex;
                }
            }

            return sb.ToString();
        }

        private readonly Dictionary<string, string> _translations = new Dictionary<string, string>();

        private static readonly Dictionary<string, LanguageManager> Managers = new Dictionary<string, LanguageManager>();

    }
}
