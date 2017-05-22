using System;
using System.Threading;
using System.Threading.Tasks;
using SharpDX.Windows;

namespace StarlightDirector.UI.Controls.Direct2D {
    public abstract class Direct2DScene : Direct2DControl {

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if (DesignMode) {
                return;
            }
            Task.Run(() => RenderLoop.Run(this, RenderWrapper));
        }

        public void StartAnimation() {
            IsAnimationEnabled = true;
        }

        public void StopAnimation() {
            IsAnimationEnabled = false;
        }

        public bool IsAnimationEnabled {
            get => _isAnimationEnabled;
            private set {
                lock (_animationEnabledLock) {
                    _isAnimationEnabled = value;
                }
            }
        }

        protected Direct2DScene() {
            _renderDisposeEvent = new AutoResetEvent(false);
        }

        protected sealed override void Dispose(bool disposing) {
            _renderDisposeEvent.Reset();
            _willDispose = true;
            StopAnimation();
            _renderDisposeEvent.WaitOne();
            _renderDisposeEvent.Dispose();
            base.Dispose(disposing);
        }

        private void RenderWrapper() {
            if (IsAnimationEnabled) {
                Render();
            } else {
                WaitForNextVBlank();
            }
            if (_willDispose) {
                if (!_disposeConfirmed) {
                    _renderDisposeEvent.Set();
                    _disposeConfirmed = true;
                }
            }
        }

        private bool _isAnimationEnabled;
        private readonly object _animationEnabledLock = new object();
        private bool _willDispose;
        private bool _disposeConfirmed;
        private readonly AutoResetEvent _renderDisposeEvent;

    }
}
