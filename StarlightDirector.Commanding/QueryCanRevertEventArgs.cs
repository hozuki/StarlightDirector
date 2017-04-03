using System;

namespace StarlightDirector.Commanding {
    public sealed class QueryCanRevertEventArgs : EventArgs {

        public bool CanRevert { get; set; } = true;

    }
}
