using System;
using System.Windows.Forms;

namespace StarlightDirector.App.UI.Forms {
    public partial class FEditorSettings : Form {

        public static DialogResult ChangeSettings(IWin32Window parent, EditorSettings settings) {
            using (var f = new FEditorSettings()) {
                f._editorSettings = settings;
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

            DialogResult = DialogResult.OK;
        }

        private void FEditorSettings_Load(object sender, EventArgs e) {
            var s = _editorSettings;
            radInvertedScrollingOn.Checked = s.InvertedScrolling;
            radInvertedScrollingOff.Checked = !s.InvertedScrolling;
            chkShowNoteIndicators.Checked = s.ShowNoteIndicators;
            txtScrollingSpeed.Value = s.ScrollingSpeed;
        }

        private EditorSettings _editorSettings;

    }
}
