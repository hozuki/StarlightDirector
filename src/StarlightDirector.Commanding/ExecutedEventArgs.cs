using System;

namespace StarlightDirector.Commanding {
    public sealed class ExecutedEventArgs : EventArgs {

        public ExecutedEventArgs(object parameter) {
            Parameter = parameter;
        }

        public object Parameter { get; }

    }
}
