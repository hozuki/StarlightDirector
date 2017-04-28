using System;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    public partial class FSpecialNote : Form {

        public static (DialogResult DialogResult, double NewBpm) VariantBpmRequestInput(IWin32Window parent, double originalBpm = 0) {
            using (var f = new FSpecialNote()) {
                f._bpm = originalBpm;
                var r = f.ShowDialog(parent);
                var bpm = f._bpm;
                return (r, bpm);
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
        }

        private void RegisterEventHandlers() {
            Load += FSpecialNote_Load;
            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            if (!double.TryParse(txtNewBpm.Text, out var bpm) || bpm <= 0) {
                MessageBox.Show(this, "Please enter a valid BPM value.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _bpm = bpm;
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

        private double _bpm;

    }
}
