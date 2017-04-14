using System;
using System.Windows.Forms;

namespace StarlightDirector.App.UI.Forms {
    public sealed partial class FAppendMeasures : Form {

        public FAppendMeasures() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        ~FAppendMeasures() {
            UnregisterEventHandlers();
        }

        public static (DialogResult DialogResult, int NumberOfMeasures) RequestInput() {
            using (var f = new FAppendMeasures()) {
                var r = f.ShowDialog();
                var n = f._numberOfMeasures;
                return (r, n);
            }
        }

        private void UnregisterEventHandlers() {
            btnOK.Click -= BtnOK_Click;
            Load -= FAppendMeasures_Load;
        }

        private void RegisterEventHandlers() {
            btnOK.Click += BtnOK_Click;
            Load += FAppendMeasures_Load;
        }

        private void FAppendMeasures_Load(object sender, EventArgs e) {
            txtNumberOfMeasures.Select(0, txtNumberOfMeasures.Text.Length);
            txtNumberOfMeasures.Select();
        }

        private void BtnOK_Click(object sender, EventArgs e) {
            _numberOfMeasures = (int)txtNumberOfMeasures.Value;
            DialogResult = DialogResult.OK;
        }

        private int _numberOfMeasures;

    }
}
