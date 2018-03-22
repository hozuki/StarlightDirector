using System;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Input {
    public sealed class ExecutedEventArgs : EventArgs {

        public ExecutedEventArgs([CanBeNull] object parameter) {
            Parameter = parameter;
        }

        [CanBeNull]
        public object Parameter { get; }

    }
}