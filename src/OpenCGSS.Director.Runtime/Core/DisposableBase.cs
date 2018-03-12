using System;

namespace OpenCGSS.Director.Core {
    public abstract class DisposableBase : IDisposable {

        ~DisposableBase() {
            if (_isDisposed) {
                return;
            }

            Dispose(false);

            _isDisposed = true;
        }

        public bool IsDisposed => _isDisposed;

        public event EventHandler<EventArgs> Disposed;

        public void Dispose() {
            if (_isDisposed) {
                return;
            }

            Dispose(true);

            _isDisposed = true;

            if (!KeepFinalizer) {
                GC.SuppressFinalize(this);
            }

            Disposed?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void Dispose(bool disposing);

        protected bool KeepFinalizer { get; set; }

        private bool _isDisposed;

    }
}
