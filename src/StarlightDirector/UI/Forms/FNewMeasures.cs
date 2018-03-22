using System;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.Globalization;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    public sealed partial class FNewMeasures : Form {

        public static (DialogResult DialogResult, int NumberOfMeasures) RequestInput(IWin32Window parentWindow) {
            using (var f = new FNewMeasures()) {
                f.Localize(LanguageManager.Current);
                f.MonitorLocalizationChange();
                var r = f.ShowDialog(parentWindow);
                f.UnmonitorLocalizationChange();
                var n = f._numberOfMeasures;
                return (r, n);
            }
        }

        private FNewMeasures() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        ~FNewMeasures() {
            UnregisterEventHandlers();
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
