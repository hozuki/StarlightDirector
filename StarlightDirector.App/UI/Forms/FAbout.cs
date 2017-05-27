using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using StarlightDirector.App.Properties;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    public partial class FAbout : Form {

        public static void ShowDialog(IWin32Window parent, bool easterEggEnabled) {
            using (var f = new FAbout()) {
                f._easterEggEnabled = easterEggEnabled;
                f.Localize(LanguageManager.Current);
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

            string html;
            string streamName;
            if (LanguageManager.Current?.CodeName == null) {
                streamName = DefaultAboutPageStreamName;
            } else {
                streamName = $"StarlightDirector.App.Resources.WebPages.About.{LanguageManager.Current.CodeName}.html";
            }
            Stream htmlStream = null;
            var names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            try {
                htmlStream = ApplicationHelper.GetResourceStream(streamName) ?? ApplicationHelper.GetResourceStream(DefaultAboutPageStreamName);
                using (var reader = new StreamReader(htmlStream)) {
                    html = reader.ReadToEnd();
                }
            } finally {
                htmlStream?.Dispose();
            }

            var version = ApplicationHelper.GetAssemblyVersion();
            html = html.Replace("$VERSION_NUMBER$", version.ToString());
            html = html.Replace("$VERSION_SUFFIX$", string.IsNullOrEmpty(VersionSuffix) ? VersionSuffix : " " + VersionSuffix);
            html = html.Replace("$VERSION_CODE$", CodeName.GetCodeName(version));
            lblAbout.Text = html;

            picAnimation.Visible = false;
            label1.Visible = false;
            var clientSize = ClientSize;
            picAnimation.Left = (clientSize.Width - picAnimation.Width) / 2;
            label1.Left = (clientSize.Width - label1.Width) / 2;
            btnClose.Left = (clientSize.Width - btnClose.Width) / 2;
        }

#if PRERELEASE_ALPHA
        private static readonly string VersionSuffix = "alpha";
#elif PRERELEASE_BETA
        private static readonly string VersionSuffix = "beta";
#else
        private static readonly string VersionSuffix = string.Empty;
#endif

        private bool _easterEggEnabled;

        private static readonly string DefaultAboutPageStreamName = "StarlightDirector.App.Resources.WebPages.About.html";

    }
}
