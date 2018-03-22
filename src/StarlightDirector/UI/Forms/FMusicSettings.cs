using System;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.Globalization;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    public partial class FMusicSettings : Form {

        public static (DialogResult DialogResult, string MusicFileName) RequestInput(IWin32Window parent, string originalFileName) {
            using (var f = new FMusicSettings()) {
                f._musicFileName = originalFileName;
                f.Localize(LanguageManager.Current);
                f.MonitorLocalizationChange();
                var r = f.ShowDialog(parent);
                f.UnmonitorLocalizationChange();
                var musicFileName = f._musicFileName;
                return (r, musicFileName);
            }
        }

        ~FMusicSettings() {
            UnregisterEventHandlers();
        }

        private FMusicSettings() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            btnOK.Click -= BtnOK_Click;
            btnBrowseFile.Click -= BtnBrowseFile_Click;
            btnClearFile.Click -= BtnClearFile_Click;
            Load -= FMusicSettings_Load;
        }

        private void RegisterEventHandlers() {
            btnOK.Click += BtnOK_Click;
            btnBrowseFile.Click += BtnBrowseFile_Click;
            btnClearFile.Click += BtnClearFile_Click;
            Load += FMusicSettings_Load;
        }

        private void FMusicSettings_Load(object sender, EventArgs e) {
            if (_musicFileName != null) {
                txtMusicFilePath.Text = _musicFileName;
            }
        }

        private void BtnClearFile_Click(object sender, EventArgs e) {
            txtMusicFilePath.Text = string.Empty;
        }

        private void BtnBrowseFile_Click(object sender, EventArgs e) {
            openFileDialog.Filter = LanguageManager.TryGetString("misc.filter.wave") ?? "Wave Audio (*.wav)|*.wav";
            openFileDialog.CheckFileExists = true;
            openFileDialog.DereferenceLinks = true;
            openFileDialog.Multiselect = false;
            openFileDialog.ReadOnlyChecked = false;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.ValidateNames = true;
            var r = openFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return;
            }
            txtMusicFilePath.Text = openFileDialog.FileName;
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            _musicFileName = txtMusicFilePath.Text;

            DialogResult = DialogResult.OK;
        }

        private string _musicFileName;

    }
}
