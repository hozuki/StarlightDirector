using System;

namespace OpenCGSS.Director.Input {
    public sealed class QueryCanExecuteEventArgs : EventArgs {

        public bool CanExecute { get; set; } = true;

    }
}
