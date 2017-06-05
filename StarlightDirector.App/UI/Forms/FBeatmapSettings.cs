using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Core;
using StarlightDirector.UI.Controls.Editing;

namespace StarlightDirector.App.UI.Forms {
    public partial class FBeatmapSettings : Form {

        public static (DialogResult DialogResult, double BPM, double MusicOffset) RequestInput(IWin32Window parentWindow, ProjectSettings projectSettings, PrimaryBeatMode primaryBeatMode) {
            using (var f = new FBeatmapSettings()) {
                f.Localize(LanguageManager.Current);
                f._bpm = projectSettings.BeatPerMinute;
                f._musicOffset = projectSettings.StartTimeOffset;
                f._primaryBeatMode = primaryBeatMode;
                f.MonitorLocalizationChange();
                var r = f.ShowDialog(parentWindow);
                f.UnmonitorLocalizationChange();
                var bpm = f._bpm;
                var offset = f._musicOffset;
                return (r, bpm, offset);
            }
        }

        private FBeatmapSettings() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        ~FBeatmapSettings() {
            UnregisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FBeatmapSettings_Load;
            btnOK.Click -= BtnOK_Click;
            btnOneMeasureMore.Click -= BtnOneMeasureMore_Click;
            btnOneMeasureLess.Click -= BtnOneMeasureLess_Click;
        }

        private void RegisterEventHandlers() {
            Load += FBeatmapSettings_Load;
            btnOK.Click += BtnOK_Click;
            btnOneMeasureMore.Click += BtnOneMeasureMore_Click;
            btnOneMeasureLess.Click += BtnOneMeasureLess_Click;
        }

        private void BtnOneMeasureLess_Click(object sender, EventArgs e) {
            if (!CheckFields()) {
                return;
            }

            var bpm = double.Parse(txtBPM.Text, NumberStyles.Number, CultureInfo.CurrentCulture);
            var startOffset = double.Parse(txtMusicOffset.Text, NumberStyles.Number, CultureInfo.CurrentCulture);
            var interval = MathHelper.BpmToInterval(bpm);
            int beatCount;
            switch (_primaryBeatMode) {
                case PrimaryBeatMode.EveryFourBeats:
                    beatCount = 4;
                    break;
                case PrimaryBeatMode.EveryThreeBeats:
                    beatCount = 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            startOffset -= beatCount * interval;
            _musicOffset = startOffset;
            txtMusicOffset.Text = _musicOffset.ToString("0.000");
            txtMusicOffset.Focus();
            txtMusicOffset.SelectAll();
        }

        private void BtnOneMeasureMore_Click(object sender, EventArgs e) {
            if (!CheckFields()) {
                return;
            }

            var bpm = double.Parse(txtBPM.Text, NumberStyles.Number, CultureInfo.CurrentCulture);
            var startOffset = double.Parse(txtMusicOffset.Text, NumberStyles.Number, CultureInfo.CurrentCulture);
            var interval = MathHelper.BpmToInterval(bpm);
            int beatCount;
            switch (_primaryBeatMode) {
                case PrimaryBeatMode.EveryFourBeats:
                    beatCount = 4;
                    break;
                case PrimaryBeatMode.EveryThreeBeats:
                    beatCount = 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            startOffset += beatCount * interval;
            _musicOffset = startOffset;
            txtMusicOffset.Text = _musicOffset.ToString("0.000");
            txtMusicOffset.Focus();
            txtMusicOffset.SelectAll();
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            if (!CheckFields()) {
                return;
            }

            var bpm = double.Parse(txtBPM.Text, NumberStyles.Number, CultureInfo.CurrentCulture);
            var startOffset = double.Parse(txtMusicOffset.Text, NumberStyles.Number, CultureInfo.CurrentCulture);

            bpm = Math.Round(bpm, 3);
            _bpm = bpm;
            startOffset = Math.Round(startOffset, 3);
            _musicOffset = startOffset;
            DialogResult = DialogResult.OK;
        }

        private void FBeatmapSettings_Load(object sender, EventArgs e) {
            txtBPM.Text = _bpm.ToString("0.000");
            txtMusicOffset.Text = _musicOffset.ToString("0.000");
            txtBPM.SelectAll();
            txtBPM.Select();
        }

        private bool CheckFields() {
            var hasError = false;
            if (!double.TryParse(txtBPM.Text, NumberStyles.Number, CultureInfo.CurrentUICulture, out var bpm)) {
                var errorMessage = LanguageManager.TryGetString("messages.fbeatmapsettings.bpm_is_not_numeric") ?? "Please enter a number.";
                MessageBox.Show(this, errorMessage, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hasError = true;
            }
            if (!hasError && bpm <= 0) {
                var errorMessage = LanguageManager.TryGetString("messages.fbeatmapsettings.bpm_value_invalid") ?? "BPM should be greater than zero.";
                MessageBox.Show(this, errorMessage, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hasError = true;
            }
            if (!hasError && !double.TryParse(txtMusicOffset.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out var startOffset)) {
                var errorMessage = LanguageManager.TryGetString("messages.fbeatmapsettings.score_offset_is_not_numeric") ?? "Please enter a number.";
                MessageBox.Show(this, errorMessage, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                hasError = true;
            }
            return !hasError;
        }

        private double _bpm;
        private double _musicOffset;
        private PrimaryBeatMode _primaryBeatMode;

    }
}
