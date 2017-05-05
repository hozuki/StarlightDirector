using System;
using System.Collections.Generic;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    public partial class FBeatmapSettings : Form {

        public FBeatmapSettings() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        ~FBeatmapSettings() {
            UnregisterEventHandlers();
        }

        public static (DialogResult DialogResult, double BPM, double MusicOffset) RequestInput(IWin32Window parentWindow, ProjectSettings scoreSettings) {
            using (var f = new FBeatmapSettings()) {
                f.InitUI(scoreSettings);
                var r = f.ShowDialog(parentWindow);
                var bpm = f._bpm;
                var offset = f._musicOffset;
                return (r, bpm, offset);
            }
        }

        private void UnregisterEventHandlers() {
            Load -= FBeatmapSettings_Load;
            btnOK.Click -= BtnOK_Click;
            foreach (var control in Controls) {
                if (control is TextBox textBox) {
                    textBox.TextChanged -= TextBox_TextChanged;
                }
            }
        }

        private void RegisterEventHandlers() {
            Load += FBeatmapSettings_Load;
            btnOK.Click += BtnOK_Click;
            foreach (var control in Controls) {
                if (control is TextBox textBox) {
                    textBox.TextChanged += TextBox_TextChanged;
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            var errSet = new HashSet<Control>();
            if (!double.TryParse(txtBPM.Text, out var bpm)) {
                errProvider.SetError(txtBPM, "Please input a number.");
                errSet.Add(txtBPM);
            }
            if (bpm <= 0) {
                if (!errSet.Contains(txtBPM)) {
                    errProvider.SetError(txtBPM, "BPM should be greater than zero.");
                    errSet.Add(txtBPM);
                }
            }
            if (!double.TryParse(txtMusicOffset.Text, out var startOffset)) {
                errProvider.SetError(txtMusicOffset, "Please input a number.");
                errSet.Add(txtMusicOffset);
            }
            if (errSet.Count > 0) {
                return;
            }
            bpm = Math.Round(bpm, 3);
            _bpm = bpm;
            startOffset = Math.Round(startOffset, 3);
            _musicOffset = startOffset;
            DialogResult = DialogResult.OK;
        }

        private void FBeatmapSettings_Load(object sender, EventArgs e) {
            txtBPM.SelectAll();
            txtBPM.Select();
        }

        private void TextBox_TextChanged(object sender, EventArgs e) {
            var control = (Control)sender;
            errProvider.SetError(control, null);
        }

        private void InitUI(ProjectSettings scoreSettings) {
            txtBPM.Text = scoreSettings.BeatPerMinute.ToString("0.000");
            txtMusicOffset.Text = scoreSettings.StartTimeOffset.ToString("0.000");
        }

        private double _bpm;
        private double _musicOffset;

    }
}
