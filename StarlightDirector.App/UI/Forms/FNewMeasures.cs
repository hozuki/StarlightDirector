using System;
using System.Windows.Forms;

namespace StarlightDirector.App.UI.Forms {
    public sealed partial class FNewMeasures : Form {

        public FNewMeasures() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        ~FNewMeasures() {
            UnregisterEventHandlers();
        }

        public static (DialogResult DialogResult, int NumberOfMeasures) RequestInput(IWin32Window parentWindow) {
            using (var f = new FNewMeasures()) {
                var r = f.ShowDialog(parentWindow);
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
