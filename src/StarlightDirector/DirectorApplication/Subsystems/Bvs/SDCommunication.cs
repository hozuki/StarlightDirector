using System;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs.Models;
using OpenCGSS.StarlightDirector.UI.Forms;

namespace OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs {
    internal sealed class SDCommunication : DisposableBase {

        internal SDCommunication([NotNull] FMain mainForm) {
            _server = new SDEditorServer(this);
            _client = new SDEditorClient(this);
            _mainForm = mainForm;
        }

        internal LifeCycleStage SimulatorLifeCycleStage { get; set; }

        [CanBeNull]
        internal Uri SimulatorServerUri { get; set; }

        [NotNull]
        internal SDEditorServer Server => _server;

        [NotNull]
        internal SDEditorClient Client => _client;

        [NotNull]
        internal FMain MainForm => _mainForm;

        protected override void Dispose(bool disposing) {
            _client.Dispose();
            _server.Dispose();
        }

        private readonly FMain _mainForm;
        private readonly SDEditorServer _server;
        private readonly SDEditorClient _client;

    }
}
