using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.App.Properties;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    public partial class FAbout : Form {

        public static void ShowDialog(IWin32Window parent, bool easterEggEnabled) {
            using (var f = new FAbout()) {
                f._easterEggEnabled = easterEggEnabled;
                f.ShowDialog(parent);
            }
        }

        ~FAbout() {
            UnregisterEventHandlers();
        }

        private FAbout() {
            InitializeComponent();
            RegisterEventHandlers();
        }

        private void UnregisterEventHandlers() {
            Load -= FAbout_Load;
            lblAbout.DoubleClick -= LblAbout_DoubleClick;
            picAnimation.DoubleClick -= PicAnimation_DoubleClick;
        }

        private void RegisterEventHandlers() {
            Load += FAbout_Load;
            lblAbout.DoubleClick += LblAbout_DoubleClick;
            picAnimation.DoubleClick += PicAnimation_DoubleClick;
        }

        private void PicAnimation_DoubleClick(object sender, EventArgs e) {
            if (!_easterEggEnabled) {
                return;
            }
            picAnimation.Visible = false;
            label1.Visible = false;
            picAnimation.Image = null;
            lblAbout.Visible = true;
        }

        private void LblAbout_DoubleClick(object sender, EventArgs e) {
            if (!_easterEggEnabled) {
                return;
            }
            picAnimation.Image = Resources.HiddenMayu;
            lblAbout.Visible = false;
            picAnimation.Visible = true;
            label1.Visible = true;
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

            picAnimation.Visible = false;
            label1.Visible = false;
            var clientSize = ClientSize;
            picAnimation.Left = (clientSize.Width - picAnimation.Width) / 2;
            label1.Left = (clientSize.Width - label1.Width) / 2;
        }

        private static readonly string VersionSuffix = " beta ";
        private static readonly string VersionCode = "Riina";

        private bool _easterEggEnabled;

    }
}
