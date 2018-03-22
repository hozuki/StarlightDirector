using System;

namespace OpenCGSS.StarlightDirector.Input {
    public sealed class QueryCanExecuteEventArgs : EventArgs {

        public bool CanExecute { get; set; } = true;

    }
}
