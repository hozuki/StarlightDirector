using System;
using System.ComponentModel.Composition;
using Gemini.Framework;
using Gemini.Framework.Services;
using JetBrains.Annotations;
using OpenCGSS.Director.Common.ViewModels;
using OpenCGSS.Director.Modules.SldProject.Views;

namespace OpenCGSS.Director.Modules.SldProject.ViewModels {
    [Export(typeof(ICgssBeatmapTools))]
    public sealed class CgssBeatmapToolsViewModel : Tool, ICgssBeatmapTools {

        [ImportingConstructor]
        public CgssBeatmapToolsViewModel([NotNull] IShell shell) {
            _shell = shell;
            DisplayName = "CGSS Beatmap Tools";

            _shell.ActiveDocumentChanged += _shell_ActiveDocumentChanged;
        }

        public override PaneLocation PreferredLocation => PaneLocation.Left;

        public void UpdateViewWindowState() {
            _view?.UpdateWindowState();
        }

        protected override void OnViewLoaded(object view) {
            base.OnViewLoaded(view);

            _view = (CgssBeatmapToolsView)view;

            UpdateViewWindowState();
        }

        private void _shell_ActiveDocumentChanged(object sender, EventArgs e) {
            UpdateViewWindowState();
        }

        private CgssBeatmapToolsView _view;
        private readonly IShell _shell;

    }
}
