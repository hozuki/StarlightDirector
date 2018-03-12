using System;

namespace OpenCGSS.Director.Input {
    public sealed class QueryCanRevertEventArgs : EventArgs {

        public bool CanRevert { get; set; } = true;

    }
}
