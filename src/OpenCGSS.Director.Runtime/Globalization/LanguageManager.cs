using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using JetBrains.Annotations;
using OpenCGSS.Director.Core;

namespace OpenCGSS.Director.Globalization {
    public sealed class LanguageManager {

        public void SetString([CanBeNull] string key, [CanBeNull] string value) {
            if (key == null) {
                return;
            }

            value = value ?? string.Empty;

            if (value.Contains(@"\n")) {
                value = value.Replace(@"\n", Environment.NewLine);
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
        [NotNull]
        public string GetString([CanBeNull] string key) {
            Guard.ArgumentNotNull(key, nameof(key));

            var gotString = GetString(key, string.Empty);

            Debug.Assert(gotString != null, nameof(gotString) + " != null");

            return gotString;
        }

        [CanBeNull]
        public string GetString([CanBeNull] string key, [CanBeNull] string defaultValue) {
            if (key == null) {
                return null;
            }

            _translations.TryGetValue(key, out var value);

            return value ?? defaultValue;
        }

        public string this[[CanBeNull] string key] {
            [NotNull]
            get => GetString(key);
            set => SetString(key, value);
        }

        [CanBeNull]
        [ContractAnnotation("language:null => null; language:notnull => notnull")]
        public static LanguageManager GetOrCreate([CanBeNull] string language) {
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

        [NotNull]
        public string Language { get; }

        [NotNull]
        public string DisplayName { get; set; }

        [NotNull]
        public string CodeName { get; set; }

        [CanBeNull]
        public static LanguageManager Current { get; set; }

        [NotNull]
        public static LanguageManager Load([NotNull] string fileName, [NotNull] string language) {
            Guard.ArgumentNotNull(fileName, nameof(fileName));
            Guard.ArgumentNotNull(language, nameof(language));
            Guard.FileExists(fileName);

            using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                return Load(fileStream, language);
            }
        }

        [NotNull]
        public static LanguageManager Load([NotNull] FileStream fileStream, [NotNull] string language) {
            Guard.ArgumentNotNull(fileStream, nameof(fileStream));
            Guard.ArgumentNotNull(language, nameof(language));

            using (var reader = new StreamReader(fileStream)) {
                return Load(reader, language);
            }
        }

        [NotNull]
        public static LanguageManager Load([NotNull] StreamReader reader, [NotNull] string language) {
            Guard.NotNull(reader, nameof(reader));
            Guard.NotNullOrEmpty(language, nameof(language));

            var manager = CreateNewNotRecorded(language);

            FillDictionary(reader, manager);

            return manager;
        }

        [NotNull]
        public static LanguageManager FromFile([NotNull] string fileName, [NotNull] string language) {
            Guard.ArgumentNotNull(fileName, nameof(fileName));
            Guard.ArgumentNotNull(language, nameof(language));
            Guard.FileExists(fileName);

            using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                return FromFile(fileStream, language);
            }
        }

        [NotNull]
        public static LanguageManager FromFile([NotNull] FileStream fileStream, [NotNull] string language) {
            Guard.ArgumentNotNull(fileStream, nameof(fileStream));
            Guard.ArgumentNotNull(language, nameof(language));

            using (var reader = new StreamReader(fileStream)) {
                return FromFile(reader, language);
            }
        }

        [NotNull]
        public static LanguageManager FromFile([NotNull] StreamReader reader, [NotNull] string language) {
            Guard.NotNull(reader, nameof(reader));
            Guard.NotNullOrEmpty(language, nameof(language));

            var manager = GetOrCreate(language);

            FillDictionary(reader, manager);

            return manager;
        }

        // ReSharper disable once NotNullMemberIsNotInitialized
        private LanguageManager([NotNull] string language) {
            Guard.NotNullOrEmpty(language, nameof(language));

            Language = language;
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

        private readonly Dictionary<string, string> _translations = new Dictionary<string, string>();

        private static readonly Dictionary<string, LanguageManager> Managers = new Dictionary<string, LanguageManager>();

    }
}
