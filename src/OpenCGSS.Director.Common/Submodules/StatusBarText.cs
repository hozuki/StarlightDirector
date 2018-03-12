using System.ComponentModel.Composition;
using System.Windows;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Modules.StatusBar;

namespace OpenCGSS.Director.Common.Submodules {
    [Export(typeof(IModule))]
    [Export(typeof(IStatusBarText))]
    public sealed class StatusBarText : ModuleBase, IStatusBarText {

        public override void PostInitialize() {
            var statusBar = IoC.Get<IStatusBar>();

            statusBar.AddItem(string.Empty, GridLength.Auto);
            _statusBar = statusBar;

            Text = DefaultText;

            base.PostInitialize();
        }

        public string Text {
            get => _statusBar.Items[0].Message;
            set => _statusBar.Items[0].Message = value ?? string.Empty;
        }

        public static readonly string DefaultText = "Ready.";

        private IStatusBar _statusBar;

    }
}
