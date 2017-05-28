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
            var language = _editorSettings.Language ?? SystemHelper.GetUserLanguageName().Replace('-', '_');
            var path = Path.Combine(LanguagesPath, language) + LanguageFileExtension;
            if (!File.Exists(path)) {
                return;
            }
            var manager = LanguageManager.FromFile(path, language);
            LanguageManager.Current = manager;
        }

        public static readonly string LanguagesPath = "Resources/Languages";
        public static readonly string LanguageFileExtension = ".txt";

        private static EditorSettings _editorSettings = new EditorSettings();

        private static readonly string SettingsFileName = "StarlightDirector.config.json";

    }
}
