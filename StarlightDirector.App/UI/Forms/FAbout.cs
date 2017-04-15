using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StarlightDirector.App.UI.Forms {
    public partial class FAbout : Form {

        public FAbout() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        ~FAbout() {
            UnregisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FAbout_Load;
        }

        private void RegisterEventHandlers() {
            Load += FAbout_Load;
        }

        private void FAbout_Load(object sender, EventArgs e) {
            lblAbout.BackColor = Color.Transparent;
            using (var htmlStream = ApplicationHelper.GetResourceStream("StarlightDirector.App.Resources.WebPages.About.html")) {
                using (var reader = new StreamReader(htmlStream)) {
                    var html = reader.ReadToEnd();
                    var version = ApplicationHelper.GetAssemblyVersionString();
                    html = html.Replace("$VERSION_NUMBER$", version).Replace("$VERSION_SUFFIX$", VersionSuffix).Replace("$VERSION_CODE$", VersionCode);
                    lblAbout.Text = html;
                }
            }
        }

        private static readonly string VersionSuffix = " beta ";
        private static readonly string VersionCode = "Riina";

    }
}
