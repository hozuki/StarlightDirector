using System;
using System.Windows.Forms;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;
using StarlightDirector.UI.Controls.Editing;

namespace StarlightDirector.App.UI.Forms {
    public partial class FGoTo : Form {

        public static (DialogResult Result, object Target) RequestInput(IWin32Window parent, int enabledItemIndex, ScoreEditor editor) {
            using (var f = new FGoTo()) {
                f._editor = editor;
                f._enabledItemIndex = enabledItemIndex;
                var r = f.ShowDialog(parent);
                var target = f._target;
                return (r, target);
            }
        }

        ~FGoTo() {
            UnregisterEventHandler();
        }

        private FGoTo() {
            InitializeComponent();
            RegisterEventHandler();
        }

        private void UnregisterEventHandler() {
            Load -= FGoTo_Load;
            btnOK.Click -= BtnOK_Click;
            radGoToMeasure.CheckedChanged -= RadGoToMeasure_CheckedChanged;
        }

        private void RegisterEventHandler() {
            Load += FGoTo_Load;
            btnOK.Click += BtnOK_Click;
            radGoToMeasure.CheckedChanged += RadGoToMeasure_CheckedChanged;
        }

        private void RadGoToMeasure_CheckedChanged(object sender, EventArgs e) {
            var gotoMeasure = radGoToMeasure.Checked;
            txtTargetMeasure.Enabled = lblTotalMeasures.Enabled = gotoMeasure;
            txtTargetTime.Enabled = lblTotalSeconds.Enabled = !gotoMeasure;
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            var gotoMeasure = radGoToMeasure.Checked;
            if (gotoMeasure) {
                var measureIndex = (int)txtTargetMeasure.Value;
                _target = measureIndex;
            } else {
                var b = double.TryParse(txtTargetTime.Text, out var time);
                var ts = TimeSpan.Zero;
                if (b) {
                    ts = TimeSpan.FromSeconds(time);
                }
                if (!b || !(_scoreStart <= ts && ts <= _scoreEnd)) {
                    MessageBox.Show(this, "Please enter a time within beatmap start and end.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _target = ts;
            }

            DialogResult = DialogResult.OK;
        }

        private void FGoTo_Load(object sender, EventArgs e) {
            switch (_enabledItemIndex) {
                case 0:
                    radGoToMeasure.Checked = true;
                    break;
                case 1:
                    radGoToMeasure.Checked = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_enabledItemIndex), _enabledItemIndex, null);
            }
            radGoToTime.Checked = !radGoToMeasure.Checked;
            RadGoToMeasure_CheckedChanged(sender, EventArgs.Empty);

            var editor = _editor;
            var score = editor.CurrentScore;
            var measures = score.Bars.Count;
            lblTotalMeasures.Text = $"/{measures}";
            if (measures == 0) {
                radGoToMeasure.Enabled = false;
                radGoToTime.Checked = true;
                RadGoToMeasure_CheckedChanged(sender, EventArgs.Empty);
            } else {
                txtTargetMeasure.Minimum = 1;
                txtTargetMeasure.Maximum = measures;
            }
            var scoreStart = TimeSpan.FromSeconds(score.Project.Settings.StartTimeOffset);
            _scoreStart = scoreStart;
            var scoreEnd = score.CalculateDuration();
            _scoreEnd = scoreEnd;
            lblTotalSeconds.Text = $"{scoreStart.TotalSeconds:0.###} - {scoreEnd.TotalSeconds:0.###}";
            txtTargetTime.Text = 0.ToString();
        }

        private TimeSpan _scoreStart;
        private TimeSpan _scoreEnd;

        private ScoreEditor _editor;
        private int _enabledItemIndex;
        private object _target;

    }
}
