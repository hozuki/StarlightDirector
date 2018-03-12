using System;

namespace StarlightDirector.Commanding {
    public sealed class RevertedEventArgs : EventArgs {

        public RevertedEventArgs(object parameter) {
            Parameter = parameter;
        }

        public object Parameter { get; }

    }
}
