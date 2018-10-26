using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.Localization;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.Models.Editor;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    public partial class FBuildBeatmap : Form {

        ~FBuildBeatmap() {
            UnregisterEventHandlers();
        }

        public static void ShowDialog(IWin32Window parent, ProjectDocument project, Difficulty difficulty) {
            using (var f = new FBuildBeatmap()) {
                f._project = project;
                f._difficulty = difficulty;
                f.Localize(LanguageManager.Current);
                f.MonitorLocalizationChange();
                f.ShowDialog(parent);
                f.UnmonitorLocalizationChange();
            }
        }

        private FBuildBeatmap() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FBuildBeatmap_Load;
            radBuildCsv.CheckedChanged -= RadBuildType_CheckedChanged;
            radEndTimeAuto.CheckedChanged -= RadEndTimeType_CheckedChanged;
            cboDurationDifficulty.SelectedIndexChanged -= CboDurationDifficulty_SelectedIndexChanged;
            btnBuild.Click -= BtnBuild_Click;
            cboCsvDifficulty.SelectedIndexChanged -= CboCsvDifficulty_SelectedIndexChanged;
            btnSelectLiveID.Click -= BtnSelectLiveID_Click;
        }

        private void RegisterEventHandlers() {
            Load += FBuildBeatmap_Load;
            radBuildCsv.CheckedChanged += RadBuildType_CheckedChanged;
            radEndTimeAuto.CheckedChanged += RadEndTimeType_CheckedChanged;
            cboDurationDifficulty.SelectedIndexChanged += CboDurationDifficulty_SelectedIndexChanged;
            btnBuild.Click += BtnBuild_Click;
            cboCsvDifficulty.SelectedIndexChanged += CboCsvDifficulty_SelectedIndexChanged;
            btnSelectLiveID.Click += BtnSelectLiveID_Click;
        }

        private void BtnSelectLiveID_Click(object sender, EventArgs e) {
            var (r, musicID, liveID) = FSelectMusicID.RequestInput(this, _musicID, _liveID);
            if (r == DialogResult.Cancel) {
                return;
            }

            _musicID = musicID;
            _liveID = liveID;
            lblLiveID.Text = liveID.ToString("000");
        }

        private void CboCsvDifficulty_SelectedIndexChanged(object sender, EventArgs e) {
            _difficulty = (Difficulty)(cboCsvDifficulty.SelectedIndex + 1);
        }

        private void BtnBuild_Click(object sender, EventArgs e) {
            var isCustom = radEndTimeCustom.Checked;
            if (isCustom) {
                if (!double.TryParse(txtCustomEndTime.Text, out var customTime) || customTime <= 0) {
                    var errorMessage = LanguageManager.TryGetString("messages.fbuildbeatmap.custom_end_time_invalid") ?? "Please enter a correct number of seconds.";
                    MessageBox.Show(this, errorMessage, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var isBdb = radBuildBdb.Checked;
            var r = isBdb ? ExportBdb() : ExportCsv();
        }

        private void CboDurationDifficulty_SelectedIndexChanged(object sender, EventArgs e) {
            var difficulty = (Difficulty)(cboDurationDifficulty.SelectedIndex + 1);
            var score = _project.Project.GetScore(difficulty);
            Debug.Assert(score != null, nameof(score) + " != null");
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
            label4.Enabled = label6.Enabled = label7.Enabled = label8.Enabled = label9.Enabled = label10.Enabled = isBdb;
            cboDiffculty1.Enabled = cboDiffculty2.Enabled = cboDiffculty3.Enabled = cboDiffculty4.Enabled = cboDiffculty5.Enabled = isBdb;
            label3.Enabled = lblLiveID.Enabled = isBdb;
            btnSelectLiveID.Enabled = isBdb;
            label5.Enabled = !isBdb;
            cboCsvDifficulty.Enabled = !isBdb;
        }

        private void FBuildBeatmap_Load(object sender, EventArgs e) {
            // Initialize difficulty texts.
            var difficultyComboBoxes = new[] { cboDurationDifficulty, cboCsvDifficulty, cboDiffculty1, cboDiffculty2, cboDiffculty3, cboDiffculty4, cboDiffculty5 };
            foreach (var comboBox in difficultyComboBoxes) {
                comboBox.Items.Clear();
                for (var d = Difficulty.Debut; d <= Difficulty.MasterPlus; ++d) {
                    comboBox.Items.Add(DescribedEnumConverter.GetEnumDescription(d));
                }
            }

            cboDiffculty1.SelectedIndex = 0;
            cboDiffculty2.SelectedIndex = 1;
            cboDiffculty3.SelectedIndex = 2;
            cboDiffculty4.SelectedIndex = 3;
            cboDiffculty5.SelectedIndex = 4;

            radEndTimeAuto.Checked = true;
            radEndTimeCustom.Checked = !radEndTimeAuto.Checked;
            radBuildCsv.Checked = _difficulty != Difficulty.Invalid;
            radBuildBdb.Checked = !radBuildCsv.Checked;

            cboCsvDifficulty.SelectedIndex = _difficulty == Difficulty.Invalid ? 0 : (int)_difficulty - 1;
            cboDurationDifficulty.SelectedIndex = _difficulty == Difficulty.Invalid ? 0 : (int)_difficulty - 1;

            RadEndTimeType_CheckedChanged(sender, EventArgs.Empty);
            RadBuildType_CheckedChanged(sender, EventArgs.Empty);

            CboCsvDifficulty_SelectedIndexChanged(sender, EventArgs.Empty);
            CboDurationDifficulty_SelectedIndexChanged(sender, EventArgs.Empty);
        }

        private bool ExportCsv() {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = LanguageManager.TryGetString("misc.filter.csv") ?? "CGSS Single Beatmap (*.csv)|*.csv";
            saveFileDialog.FileName = string.Empty;
            var r = saveFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return false;
            }

            var score = _project.Project.GetScore(_difficulty);
            Debug.Assert(score != null, nameof(score) + " != null");
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
            var reportMessageTemplate = LanguageManager.TryGetString("messages.fbuildbeatmap.csv_score_exported") ?? "The score is exported to '{0}'.";
            var reportMessage = string.Format(reportMessageTemplate, saveFileDialog.FileName);
            MessageBox.Show(this, reportMessage, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        private bool ExportBdb() {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = LanguageManager.TryGetString("misc.filter.bdb") ?? "CGSS Beatmap Bundle (*.bdb)|*.bdb";
            saveFileDialog.FileName = $"musicscores_m{_liveID:000}.bdb";
            var r = saveFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return false;
            }
            var bdbFileName = saveFileDialog.FileName;

            var difficulties = new Difficulty[] {
                GetSelectedDifficulty(cboDiffculty1),
                GetSelectedDifficulty(cboDiffculty2),
                GetSelectedDifficulty(cboDiffculty3),
                GetSelectedDifficulty(cboDiffculty4),
                GetSelectedDifficulty(cboDiffculty5)
            };
            var scores = difficulties.Select(d => _project.Project.GetScore(d)).ToArray();

            var isEndingCustom = radEndTimeCustom.Checked;
            var compiledScores = new CompiledScore[scores.Length];
            for (var i = 0; i < scores.Length; ++i) {
                var score = scores[i];
                CompiledScore compiledScore;
                if (isEndingCustom) {
                    var durationSeconds = double.Parse(txtCustomEndTime.Text);
                    var duration = TimeSpan.FromSeconds(durationSeconds);
                    compiledScore = score.Compile(duration);
                } else {
                    compiledScore = score.Compile();
                }
                compiledScores[i] = compiledScore;
            }

            var connectionString = $"Data Source={bdbFileName}";
            if (File.Exists(bdbFileName)) {
                File.Delete(bdbFileName);
            } else {
                SQLiteConnection.CreateFile(bdbFileName);
            }

            var liveID = _liveID;
            using (var connection = new SQLiteConnection(connectionString)) {
                connection.Open();
                using (var transaction = connection.BeginTransaction()) {
                    SQLiteCommand command;
                    using (command = transaction.Connection.CreateCommand()) {
                        command.CommandText = "CREATE TABLE blobs (name TEXT PRIMARY KEY, data BLOB NOT NULL);";
                        command.ExecuteNonQuery();
                    }
                    using (command = transaction.Connection.CreateCommand()) {
                        command.CommandText = "INSERT INTO blobs (name, data) VALUES (@name, @data);";
                        var nameParam = command.Parameters.Add("name", DbType.AnsiString);
                        var dataParam = command.Parameters.Add("data", DbType.Binary);
                        // update: Create Master+ entry, regardless of its existence in original BDB.
                        for (var i = (int)Difficulty.Debut; i <= (int)Difficulty.MasterPlus; ++i) {
                            var compiledScore = compiledScores[i - 1];
                            var csv = compiledScore.GetCsvString();
                            var csvData = Encoding.ASCII.GetBytes(csv);
                            nameParam.Value = string.Format(ScoreRecordFormat, liveID, i);
                            dataParam.Value = csvData;
                            command.ExecuteNonQuery();
                        }
                        nameParam.Value = string.Format(Score2DCharaFormat, liveID);
                        dataParam.Value = Encoding.ASCII.GetBytes(Score2DCharaText);
                        command.ExecuteNonQuery();
                        nameParam.Value = string.Format(ScoreCyalumeFormat, liveID);
                        dataParam.Value = Encoding.ASCII.GetBytes(ScoreCyalumeText);
                        command.ExecuteNonQuery();
                        nameParam.Value = string.Format(ScoreLyricsFormat, liveID);
                        dataParam.Value = Encoding.ASCII.GetBytes(ScoreLyricsText);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                connection.Close();
            }

            var reportMessageTemplate = LanguageManager.TryGetString("messages.fbuildbeatmap.bdb_scores_exported") ?? "The scores are exported to '{0}'.";
            var reportMessage = string.Format(reportMessageTemplate, saveFileDialog.FileName);
            MessageBox.Show(this, reportMessage, AssemblyHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Information);

            return true;

            Difficulty GetSelectedDifficulty(ComboBox comboBox) {
                return (Difficulty)(comboBox.SelectedIndex + 1);
            }
        }

        private static readonly string ScoreRecordFormat = "musicscores/m{0:000}/{0}_{1}.csv";
        private static readonly string Score2DCharaFormat = "musicscores/m{0:000}/m{0:000}_2dchara.csv";
        private static readonly string ScoreCyalumeFormat = "musicscores/m{0:000}/m{0:000}_cyalume.csv";
        private static readonly string ScoreLyricsFormat = "musicscores/m{0:000}/m{0:000}_lyrics.csv";
        private static readonly string Score2DCharaText = "0.03333,1,w1,,\n0.03333,2,w1,,\n0.03333,3,w1,,\n0.03333,4,w1,,\n0.03333,5,w1,,\n0.03333,MOT_mc_bg_black_00,seat100-0,,";
        private static readonly string ScoreCyalumeText = "time,move_type,BPM,color_pattern,color1,color2,color3,color4,color5,width1,width2,width3,width4,width5,3D_move_type,3D_BPM\n0,Uhoi,5,Random3,Pink01,Blue01,Yellow01,,,,,,,,Uhoi,5";
        private static readonly string ScoreLyricsText = "time,lyrics,size\n0,,";

        private int _musicID = 1001;
        private int _liveID = 1;
        private ProjectDocument _project;
        private Difficulty _difficulty;

    }
}
