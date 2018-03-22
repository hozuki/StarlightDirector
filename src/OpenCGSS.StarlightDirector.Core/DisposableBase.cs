using System;

namespace OpenCGSS.StarlightDirector {
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

            if (!KeepFinalizer) {
                GC.SuppressFinalize(this);
            }

            _isDisposed = true;

            Disposed?.Invoke(this, EventArgs.Empty);
        }

        protected bool KeepFinalizer { get; set; }

        protected abstract void Dispose(bool disposing);

        private bool _isDisposed;

    }
}
