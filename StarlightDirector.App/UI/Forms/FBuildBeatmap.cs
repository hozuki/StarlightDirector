using System;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    public partial class FBuildBeatmap : Form {

        public FBuildBeatmap() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        ~FBuildBeatmap() {
            UnregisterEventHandlers();
        }

        public static void ShowDialog(IWin32Window parent, Project project, Difficulty difficulty) {
            using (var f = new FBuildBeatmap()) {
                f._project = project;
                f._difficulty = difficulty;
                f.ShowDialog(parent);
            }
        }

        private void UnregisterEventHandlers() {
            Load -= FBuildBeatmap_Load;
            radBuildCsv.CheckedChanged -= RadBuildType_CheckedChanged;
            radEndTimeAuto.CheckedChanged -= RadEndTimeType_CheckedChanged;
            cboDurationDifficulty.SelectedIndexChanged -= CboDurationDifficulty_SelectedIndexChanged;
            btnBuild.Click -= BtnBuild_Click;
            cboCsvDifficulty.SelectedIndexChanged -= CboCsvDifficulty_SelectedIndexChanged;
        }

        private void RegisterEventHandlers() {
            Load += FBuildBeatmap_Load;
            radBuildCsv.CheckedChanged += RadBuildType_CheckedChanged;
            radEndTimeAuto.CheckedChanged += RadEndTimeType_CheckedChanged;
            cboDurationDifficulty.SelectedIndexChanged += CboDurationDifficulty_SelectedIndexChanged;
            btnBuild.Click += BtnBuild_Click;
            cboCsvDifficulty.SelectedIndexChanged += CboCsvDifficulty_SelectedIndexChanged;
        }

        private void CboCsvDifficulty_SelectedIndexChanged(object sender, EventArgs e) {
            _difficulty = (Difficulty)(cboCsvDifficulty.SelectedIndex + 1);
        }

        private void BtnBuild_Click(object sender, EventArgs e) {
            var isCustom = radEndTimeCustom.Checked;
            if (isCustom) {
                if (!double.TryParse(txtCustomEndTime.Text, out var customTime) || customTime <= 0) {
                    MessageBox.Show(this, "Please enter a correct number of seconds.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var isBdb = radBuildBdb.Checked;
            var r = isBdb ? ExportBdb() : ExportCsv();
            if (r) {
                DialogResult = DialogResult.OK;
            }
        }

        private void CboDurationDifficulty_SelectedIndexChanged(object sender, EventArgs e) {
            var difficulty = (Difficulty)(cboDurationDifficulty.SelectedIndex + 1);
            var score = _project.GetScore(difficulty);
            var estimatedDuration = score.CalculateDuration();
            estimatedDuration += TimeSpan.FromSeconds(1);
            var durationText = estimatedDuration.TotalSeconds.ToString("0.###");
            txtCustomEndTime.Text = durationText;
            lblEstimatedDuration.Text = durationText;
        }

        private void RadEndTimeType_CheckedChanged(object sender, EventArgs e) {
            var isAuto = radEndTimeAuto.Checked;
            txtCustomEndTime.Enabled = !isAuto;
        }

        private void RadBuildType_CheckedChanged(object sender, EventArgs e) {
            var isBdb = radBuildBdb.Checked;
            label4.Enabled = label5.Enabled = label6.Enabled = label7.Enabled = label8.Enabled = label9.Enabled = label10.Enabled = isBdb;
            cboDiffculty1.Enabled = cboDiffculty2.Enabled = cboDiffculty3.Enabled = cboDiffculty4.Enabled = cboDiffculty5.Enabled = isBdb;
            cboCsvDifficulty.Enabled = !isBdb;
        }

        private void FBuildBeatmap_Load(object sender, EventArgs e) {
            cboDiffculty1.SelectedIndex = 0;
            cboDiffculty2.SelectedIndex = 1;
            cboDiffculty3.SelectedIndex = 2;
            cboDiffculty4.SelectedIndex = 3;
            cboDiffculty5.SelectedIndex = 4;

            radEndTimeAuto.Checked = true;
            radBuildCsv.Checked = _difficulty != Difficulty.Invalid;

            cboCsvDifficulty.SelectedIndex = _difficulty == Difficulty.Invalid ? 0 : (int)_difficulty - 1;
            cboDurationDifficulty.SelectedIndex = _difficulty == Difficulty.Invalid ? 0 : (int)_difficulty - 1;

            RadEndTimeType_CheckedChanged(sender, EventArgs.Empty);
            RadBuildType_CheckedChanged(sender, EventArgs.Empty);

            CboCsvDifficulty_SelectedIndexChanged(sender, EventArgs.Empty);
            CboDurationDifficulty_SelectedIndexChanged(sender, EventArgs.Empty);
        }

        private bool ExportCsv() {
            var score = _project.GetScore(_difficulty);
            if (score == null) {
                MessageBox.Show(this, "There is no score to compile and export.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "CGSS Single Beatmap (*.csv)|*.csv";
            var r = saveFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return false;
            }

            CompiledScore compiledScore;
            var isEndingCustom = radEndTimeCustom.Checked;
            if (isEndingCustom) {
                var durationSeconds = double.Parse(txtCustomEndTime.Text);
                var duration = TimeSpan.FromSeconds(durationSeconds);
                compiledScore = score.Compile(duration);
            } else {
                compiledScore = score.Compile();
            }

            var csv = compiledScore.GetCsvString();
            using (var fileStream = File.Open(saveFileDialog.FileName, FileMode.Create, FileAccess.Write)) {
                using (var writer = new StreamWriter(fileStream)) {
                    writer.Write(csv);
                }
            }
            MessageBox.Show(this, $"The score is exported to '{saveFileDialog.FileName}'.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        private bool ExportBdb() {
            throw new NotImplementedException();
        }

        private Project _project;
        private Difficulty _difficulty;

    }
}
