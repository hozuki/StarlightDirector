using System;
using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    public partial class FSpecialNote : Form {

        public static (DialogResult DialogResult, double NewBpm) VariantBpmRequestInput(IWin32Window parent, int barIndex, int rowIndex, double originalBpm = 0) {
            using (var f = new FSpecialNote()) {
                f._bpm = originalBpm;
                f.SetManualSelection(false);
                f.cboMeasures.Items.Add((barIndex + 1).ToString());
                f.cboMeasures.SelectedIndex = 0;
                f.cboRows.Items.Add((rowIndex + 1).ToString());
                f.cboRows.SelectedIndex = 0;
                f.Localize(LanguageManager.Current);
                var r = f.ShowDialog(parent);
                var bpm = f._bpm;
                return (r, bpm);
            }
        }

        public static (DialogResult DialogResult, double NewBpm, int BarIndex, int RowIndex) VariantBpmRequestInput(IWin32Window parent, Score score) {
            using (var f = new FSpecialNote()) {
                f._bpm = 0;
                f._score = score;
                f.FillMeasureComboBox();
                f.CboMeasures_SelectedIndexChanged(f, EventArgs.Empty);
                f.Localize(LanguageManager.Current);
                var r = f.ShowDialog(parent);
                var bpm = f._bpm;
                var barIndex = f._barIndex;
                var row = f._rowIndex;
                return (r, bpm, barIndex, row);
            }
        }

        ~FSpecialNote() {
            UnregisterEventHandlers();
        }

        private FSpecialNote() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FSpecialNote_Load;
            btnOK.Click -= BtnOK_Click;
            cboMeasures.SelectedIndexChanged -= CboMeasures_SelectedIndexChanged;
        }

        private void RegisterEventHandlers() {
            Load += FSpecialNote_Load;
            btnOK.Click += BtnOK_Click;
            cboMeasures.SelectedIndexChanged += CboMeasures_SelectedIndexChanged;
        }

        private void CboMeasures_SelectedIndexChanged(object sender, EventArgs e) {
            var score = _score;
            if (score == null) {
                return;
            }
            var selectedIndex = cboMeasures.SelectedIndex;
            var bar = score.Bars.Find(b => b.Basic.Index == selectedIndex);
            if (bar == null) {
                MessageBox.Show(LanguageManager.TryGetString("messages.fspecialnote.measures_not_exist") ?? "There are no available measures yet.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetManualSelection(false);
                btnOK.Enabled = false;
                return;
            }
            cboRows.Items.Clear();
            cboRows.Items.AddRange(Enumerable.Range(1, bar.GetNumberOfGrids()).Select(n => (object)n.ToString()).ToArray());
            if (cboRows.Items.Count > 0) {
                cboRows.SelectedIndex = 0;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            if (!double.TryParse(txtNewBpm.Text, out var bpm) || bpm <= 0) {
                MessageBox.Show(this, LanguageManager.TryGetString("messages.fspecialnote.bpm_invalid") ?? "Please enter a valid BPM value.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _bpm = bpm;
            if (cboMeasures.Enabled) {
                _barIndex = cboMeasures.SelectedIndex;
                _rowIndex = cboRows.SelectedIndex;
            } else {
                _barIndex = _rowIndex = -1;
            }
            DialogResult = DialogResult.OK;
        }

        private void FSpecialNote_Load(object sender, EventArgs e) {
            var bpm = _bpm;
            if (bpm.Equals(0d)) {
                bpm = ProjectSettings.DefaultBeatPerMinute;
            }
            txtNewBpm.Text = bpm.ToString("0.###");
            txtNewBpm.SelectAll();
            txtNewBpm.Select();
        }

        private void SetManualSelection(bool enabled) {
            label2.Enabled = label3.Enabled = enabled;
            cboMeasures.Enabled = cboRows.Enabled = enabled;
        }

        private void FillMeasureComboBox() {
            var score = _score;
            cboMeasures.Items.Clear();
            foreach (var bar in score.Bars) {
                cboMeasures.Items.Add((bar.Basic.Index + 1).ToString());
            }
            if (cboMeasures.Items.Count > 0) {
                cboMeasures.SelectedIndex = 0;
            }
        }

        private double _bpm;
        private Score _score;
        private int _barIndex = -1;
        private int _rowIndex = -1;

    }
}
