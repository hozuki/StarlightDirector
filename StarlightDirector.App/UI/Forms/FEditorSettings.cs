using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    public partial class FEditorSettings : Form {

        public static DialogResult ChangeSettings(IWin32Window parent, EditorSettings settings) {
            using (var f = new FEditorSettings()) {
                f._editorSettings = settings;
                f.Localize(LanguageManager.Current);
                return f.ShowDialog(parent);
            }
        }

        ~FEditorSettings() {
            UnregisterEventHandlers();
        }

        private FEditorSettings() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FEditorSettings_Load;
            btnOK.Click -= BtnOK_Click;
        }

        private void RegisterEventHandlers() {
            Load += FEditorSettings_Load;
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            var s = _editorSettings;
            s.InvertedScrolling = radInvertedScrollingOn.Checked;
            s.ShowNoteIndicators = chkShowNoteIndicators.Checked;
            s.ScrollingSpeed = (int)txtScrollingSpeed.Value;

            if (cboLanguage.SelectedIndex == 0) {
                s.Language = null;
            } else {
                s.Language = _languages[cboLanguage.SelectedIndex - 1].CodeName;
            }

            DialogResult = DialogResult.OK;
        }

        private void FEditorSettings_Load(object sender, EventArgs e) {
            var directory = new DirectoryInfo(EditorSettingsManager.LanguagesPath);
            if (directory.Exists) {
                var fileNames = Directory.EnumerateFiles(directory.FullName, "*" + EditorSettingsManager.LanguageFileExtension, SearchOption.TopDirectoryOnly);
                foreach (var fileName in fileNames) {
                    // The language name is not important here.
                    var manager = LanguageManager.Load(fileName, fileName);
                    _languages.Add((manager.DisplayName, manager.CodeName));
                }
            }

            cboLanguage.Items.Clear();
            cboLanguage.Items.Add(LanguageManager.TryGetString("lang.use_auto.text") ?? "Auto");
            cboLanguage.Items.AddRange(_languages.Select(v => v.DisplayName).Cast<object>().ToArray());

            var s = _editorSettings;

            int langIndex;
            if (s.Language == null) {
                langIndex = 0;
            } else {
                if (LanguageManager.Current != null) {
                    langIndex = _languages.FindIndex(v => v.CodeName == LanguageManager.Current.CodeName);
                    if (langIndex < 0) {
                        langIndex = 0;
                    } else {
                        langIndex += 1;
                    }
                } else {
                    langIndex = 0;
                }
            }
            cboLanguage.SelectedIndex = langIndex;

            radInvertedScrollingOn.Checked = s.InvertedScrolling;
            radInvertedScrollingOff.Checked = !s.InvertedScrolling;
            chkShowNoteIndicators.Checked = s.ShowNoteIndicators;
            txtScrollingSpeed.Value = s.ScrollingSpeed;
        }

        private EditorSettings _editorSettings;
        private readonly List<(string DisplayName, string CodeName)> _languages = new List<(string, string)>();

    }
}
