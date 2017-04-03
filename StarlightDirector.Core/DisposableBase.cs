using System;

namespace StarlightDirector.Core {
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
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void Dispose(bool disposing);

        private bool _isDisposed;

    }
}
