using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using StarlightDirector.Core;

namespace StarlightDirector.App {
    internal static class EditorSettingsManager {

        public static void LoadSettings() {
            var fileInfo = new FileInfo(SettingsFileName);
            if (!fileInfo.Exists) {
                _editorSettings = new EditorSettings();
                return;
            }
            try {
                using (var fileStream = File.Open(fileInfo.FullName, FileMode.Open, FileAccess.Read)) {
                    using (var streamReader = new StreamReader(fileStream)) {
                        using (var jsonReader = new JsonTextReader(streamReader)) {
                            var serializer = JsonSerializer.Create();
                            var obj = serializer.Deserialize<EditorSettings>(jsonReader);
                            _editorSettings = obj;
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Failed to load editor settings.{Environment.NewLine}{ex.Message}", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _editorSettings = new EditorSettings();
            }
        }

        public static void SaveSettings() {
            var fileInfo = new FileInfo(SettingsFileName);
            try {
                using (var fileStream = File.Open(fileInfo.FullName, FileMode.Create, FileAccess.Write)) {
                    using (var streamWriter = new StreamWriter(fileStream)) {
                        using (var jsonWriter = new JsonTextWriter(streamWriter)) {
                            var serializer = JsonSerializer.Create();
                            serializer.Serialize(jsonWriter, _editorSettings);
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show($"Failed to save editor settings.{Environment.NewLine}{ex.Message}", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static EditorSettings CurrentSettings => _editorSettings;

        internal static void ApplyLanguageSettings() {
            var s = _editorSettings;
            if (s.Language == null) {
                s.Language = SystemHelper.GetUserLanguageName();
            }
            var path = Path.Combine(LanguagesPath, s.Language) + LanguageFileExtension;
            if (!File.Exists(path)) {
                return;
            }
            var manager = LanguageManager.Create(s.Language);
            using (var fileStream = File.Open(path, FileMode.Open, FileAccess.Read)) {
                using (var reader = new StreamReader(fileStream)) {
                    // The first line is name of the language.
                    reader.ReadLine();
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
                }
            }
            LanguageManager.Current = manager;
        }

        private static EditorSettings _editorSettings = new EditorSettings();

        private static readonly string SettingsFileName = "StarlightDirector.config.json";
        private static readonly string LanguagesPath = "Resources/Languages";
        private static readonly string LanguageFileExtension = ".txt";

    }
}
