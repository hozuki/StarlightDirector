using System;

namespace StarlightDirector.Commanding {
    public sealed class QueryCanExecuteEventArgs : EventArgs {

        public bool CanExecute { get; set; } = true;

    }
}
