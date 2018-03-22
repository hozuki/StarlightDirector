using System;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Input {
    public sealed class RevertedEventArgs : EventArgs {

        public RevertedEventArgs([CanBeNull] object parameter) {
            Parameter = parameter;
        }

        [CanBeNull]
        public object Parameter { get; }

    }
}