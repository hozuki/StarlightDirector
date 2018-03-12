using System;
using System.Collections.Generic;
using System.IO;

namespace StarlightDirector.Core {
    public sealed class LanguageManager {

        public void SetString(string key, string value) {
            if (key == null) {
                return;
            }
            if (value == null) {
                value = string.Empty;
            }
            value = value.Replace(@"\n", Environment.NewLine);
            _translations[key] = value;
        }

        /// <summary>
        /// Attempt to retrieve a localized string. If <see cref="key"/> is not found, return <see langword="null"/>.
        /// </summary>
        /// <param name="key">The key of local string.</param>
        /// <returns>Retrieved locale string, or <see langword="null"/>.</returns>
        public static string TryGetString(string key) {
            return Current?.GetString(key, null);
        }

        /// <summary>
        /// Attempt to retrieve a localized string. If <see cref="key"/> is not found, return <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="key">The key of local string.</param>
        /// <returns>Retrieved locale string, or <see cref="string.Empty"/>.</returns>
        public string GetString(string key) {
            return GetString(key, string.Empty);
        }

        public string GetString(string key, string defaultValue) {
            if (key == null) {
                return null;
            }
            _translations.TryGetValue(key, out var value);
            return value ?? defaultValue;
        }

        public string this[string key] {
            get => GetString(key);
            set => SetString(key, value);
        }

        public static LanguageManager Create(string language) {
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

        public string Language { get; }

        public string DisplayName { get; set; }

        public string CodeName { get; set; }

        public static LanguageManager Current { get; set; }

        public static LanguageManager Load(string fileName, string language) {
            using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)) {
                return Load(fileStream, language);
            }
        }

        public static LanguageManager Load(FileStream fileStream, string language) {
            using (var reader = new StreamReader(fileStream)) {
                return Load(reader, language);
            }
        }

        public static LanguageManager Load(StreamReader reader, string language) {
            if (string.IsNullOrEmpty(language)) {
                throw new ArgumentException("language must not be null.", nameof(language));
            }
            var manager = CreateNewNotRecorded(language);
            FillDictionary(reader, manager);
            return manager;
        }

        public static LanguageManager FromFile(string fileName, string language) {
            using (var fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)) {
                return FromFile(fileStream, language);
            }
        }

        public static LanguageManager FromFile(FileStream fileStream, string language) {
            using (var reader = new StreamReader(fileStream)) {
                return FromFile(reader, language);
            }
        }

        public static LanguageManager FromFile(StreamReader reader, string language) {
            if (string.IsNullOrEmpty(language)) {
                throw new ArgumentException("language must not be null.", nameof(language));
            }
            var manager = Create(language);
            FillDictionary(reader, manager);
            return manager;
        }

        private LanguageManager(string language) {
            Language = language;
        }

        private static LanguageManager CreateNewNotRecorded(string language) {
            if (language == null) {
                return null;
            }
            var manager = new LanguageManager(language);
            return manager;
        }

        private static void FillDictionary(StreamReader reader, LanguageManager manager) {
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
            manager.CodeName = manager.GetString("lang.code_name") ?? "neutral";
        }

        private readonly Dictionary<string, string> _translations = new Dictionary<string, string>();

        private static readonly Dictionary<string, LanguageManager> Managers = new Dictionary<string, LanguageManager>();

    }
}
