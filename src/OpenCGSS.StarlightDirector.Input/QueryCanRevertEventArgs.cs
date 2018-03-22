using System;

namespace OpenCGSS.StarlightDirector.Input {
    public sealed class QueryCanRevertEventArgs : EventArgs {

        public bool CanRevert { get; set; } = true;

    }
}
