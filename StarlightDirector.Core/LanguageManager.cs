using System;
using System.Collections.Generic;

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

        public static string TryGetString(string key) {
            return Current?.GetString(key, null);
        }

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

        public string Name { get; set; }

        public string CodeName { get; set; }

        public static LanguageManager Current { get; set; }

        private LanguageManager(string language) {
            Language = language;
        }

        private readonly Dictionary<string, string> _translations = new Dictionary<string, string>();

        private static readonly Dictionary<string, LanguageManager> Managers = new Dictionary<string, LanguageManager>();

    }
}
