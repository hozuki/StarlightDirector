using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.Models.Deleste;
using OpenCGSS.StarlightDirector.Models.Editor;
using OpenCGSS.StarlightDirector.Models.Gaming;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    public partial class FExportTxt : Form {

        public static void ShowDialog([CanBeNull] IWin32Window parent, [NotNull] ProjectDocument project, Difficulty difficulty) {
            var score = project.Project.GetScore(difficulty);

            if (score == null) {
                return;
            }

            using (var f = new FExportTxt()) {
                f._score = score;
                f.Localize(LanguageManager.Current);
                f.MonitorLocalizationChange();
                f.ShowDialog(parent);
                f.UnmonitorLocalizationChange();
            }
        }

        ~FExportTxt() {
            UnregisterEventHandlers();
        }

        private FExportTxt() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FExportTxt_Load;
            btnExport.Click -= BtnExport_Click;
        }

        private void RegisterEventHandlers() {
            Load += FExportTxt_Load;
            btnExport.Click += BtnExport_Click;
        }

        private void BtnExport_Click(object sender, EventArgs e) {
            {
                string message;
                int intValue;

                if (!int.TryParse(txtLevel.Text, out intValue) || intValue < 5) {
                    message = LanguageManager.TryGetString("messages.fexporttxt.level_input_requirement") ?? "Level should be an integer, at least 5.";
                    MessageBox.Show(this, message, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtBgmVolume.Text, out intValue) || (intValue < 0 || intValue > 100)) {
                    message = LanguageManager.TryGetString("messages.fexporttxt.bgm_volume_input_requirement") ?? "BGM volume should be an integer between 0 and 100.";
                    MessageBox.Show(this, message, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSeVolume.Text, out intValue) || (intValue < 0 || intValue > 100)) {
                    message = LanguageManager.TryGetString("messages.fexporttxt.se_volume_input_requirement") ?? "SE volume should be an integer between 0 and 100.";
                    MessageBox.Show(this, message, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            ExportTxt();
        }

        private void FExportTxt_Load(object sender, EventArgs e) {
            {
                var scoreDifficulty = _score.Difficulty;

                cboDifficulty.Items.Clear();

                for (var d = Difficulty.Debut; d <= Difficulty.MasterPlus; ++d) {
                    cboDifficulty.Items.Add(DescribedEnumConverter.GetEnumDescription(d));
                }

                cboDifficulty.SelectedIndex = scoreDifficulty - Difficulty.Debut;

                int level;

                switch (scoreDifficulty) {
                    case Difficulty.Debut:
                        level = 7;
                        break;
                    case Difficulty.Regular:
                        level = 13;
                        break;
                    case Difficulty.Pro:
                        level = 17;
                        break;
                    case Difficulty.Master:
                        level = 24;
                        break;
                    case Difficulty.MasterPlus:
                        level = 30;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                txtLevel.Text = level.ToString();
            }

            {
                cboColor.Items.Clear();

                var musicColors = new[] { MusicColor.Cute, MusicColor.Cool, MusicColor.Passion, MusicColor.Multicolor };

                foreach (var c in musicColors) {
                    cboColor.Items.Add(DescribedEnumConverter.GetEnumDescription(c));
                }

                cboColor.SelectedIndex = 3; // multicolor
            }

            {
                cboFormat.Items.Clear();

                var formats = new[] { TxtScoreFormat.Default, TxtScoreFormat.Converted };

                foreach (var f in formats) {
                    cboFormat.Items.Add(DescribedEnumConverter.GetEnumDescription(f));
                }

                cboFormat.SelectedIndex = 1; // converted
            }

            {
                saveFileDialog.OverwritePrompt = true;
                saveFileDialog.ValidateNames = true;
                saveFileDialog.Filter = LanguageManager.TryGetString("misc.filter.deleste.txt") ?? "Deleste TXT Beatmap (*.txt)|*.txt";
            }
        }

        private void ExportTxt() {
            var r = saveFileDialog.ShowDialog(this);

            if (r == DialogResult.Cancel) {
                return;
            }

            _score.UpdateNoteHitTimings();

            var difficulty = (Difficulty)(cboDifficulty.SelectedIndex + 1);
            var title = txtTitle.Text;
            var composer = txtComposer.Text;
            var lyricist = txtLyricist.Text;
            var bg = txtBgFile.Text;
            var song = txtSongFile.Text;
            var level = Convert.ToInt32(txtLevel.Text);
            var color = (MusicColor)(1 << cboColor.SelectedIndex);
            var bgmVolume = Convert.ToInt32(txtBgmVolume.Text);
            var seVolume = Convert.ToInt32(txtSeVolume.Text);

            using (var fileStream = File.Open(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.Write)) {
                using (var writer = new StreamWriter(fileStream, Encoding.UTF8)) { // Use the encoding instance with BOM (this is important)
                    DelesteHelper.WriteDelesteBeatmap(writer, _score, difficulty, title, composer, lyricist, bg, song, level, color, bgmVolume, seVolume);
                }
            }

            var reportMessageTemplate = LanguageManager.TryGetString("messages.fexporttxt.txt_score_exported") ?? "The score is exported to '{0}'.";
            var reportMessage = string.Format(reportMessageTemplate, saveFileDialog.FileName);

            MessageBox.Show(this, reportMessage, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        [NotNull]
        private Score _score;

    }
}
