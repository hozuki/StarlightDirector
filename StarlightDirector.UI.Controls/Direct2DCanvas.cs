using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls {
    public abstract class Direct2DCanvas : Control {

        public Color ClearColor { get; set; }

        protected Direct2DCanvas() {
            if (!DesignMode) {
                const ControlStyles stylesOn = ControlStyles.UserPaint | ControlStyles.Selectable | ControlStyles.Opaque;
                const ControlStyles stylesOff = ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw;
                SetStyle(stylesOn, true);
                SetStyle(stylesOff, false);
                _exitingEvent = new ManualResetEvent(false);
                _renderThread = new Thread(RenderThreadProc);
                _renderThread.IsBackground = true;
            }
            ClearColor = DefaultBackColor;
        }

        protected sealed override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing) {
                if (!DesignMode) {
                    OnDisposeResources(_renderContext);
                    _renderTarget?.Dispose();
                    _d2d1Factory?.Dispose();
                    _dwriteFactory?.Dispose();
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if (!DesignMode) {
                _d2d1Factory = new Factory();
                _dwriteFactory = new SharpDX.DirectWrite.Factory();
                var clientSize = ClientSize;
                var hwndProps = new HwndRenderTargetProperties {
                    Hwnd = Handle,
                    PixelSize = new Size2(clientSize.Width, clientSize.Height)
                };
                var target = new WindowRenderTarget(_d2d1Factory, DefaultRenderTargetProperties, hwndProps);
                _renderTarget = target;
                _renderContext = new D2DRenderContext(target, _dwriteFactory, ClientSize);
                OnCreateResources(_renderContext);
                _renderThread.Start();
            }
        }

        protected override void OnHandleDestroyed(EventArgs e) {
            if (!DesignMode) {
                ContinueLogic = false;
                _exitingEvent.WaitOne();
                _exitingEvent.Dispose();
            }
            base.OnHandleDestroyed(e);
        }

        protected override void OnClientSizeChanged(EventArgs e) {
            base.OnClientSizeChanged(e);
            if (!DesignMode) {
                if (_renderContext != null) {
                    lock (_sizeLock) {
                        var newSize = ClientSize;
                        _renderTarget.Resize(new Size2(newSize.Width, newSize.Height));
                        _renderContext.Resize(newSize.Width, newSize.Height);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (DesignMode) {
                e.Graphics.FillRectangle(Brushes.Black, ClientRectangle);
            }
        }

        protected abstract void OnRender(D2DRenderContext context);

        protected abstract void OnCreateResources(D2DRenderContext context);

        protected abstract void OnDisposeResources(D2DRenderContext context);

        private void RenderThreadProc() {
            var context = _renderContext;
            while (ContinueLogic) {
                lock (_sizeLock) {
                    context.BeginDraw();
                    context.Clear(ClearColor);
                    OnRender(context);
                    context.EndDraw();
                }
            }
            _exitingEvent?.Set();
        }

        private static readonly RenderTargetProperties DefaultRenderTargetProperties = new RenderTargetProperties {
            Usage = RenderTargetUsage.GdiCompatible,
            Type = RenderTargetType.Hardware
        };

        private WindowRenderTarget _renderTarget;
        private D2DRenderContext _renderContext;
        private Factory _d2d1Factory;
        private SharpDX.DirectWrite.Factory _dwriteFactory;

        private bool ContinueLogic {
            get {
                lock (_logicLock) {
                    return _continueLogic;
                }
            }
            set {
                lock (_logicLock) {
                    _continueLogic = value;
                }
            }
        }

        private volatile bool _continueLogic = true;
        private readonly Thread _renderThread;
        private readonly object _logicLock = new object();
        private readonly object _sizeLock = new object();
        private readonly ManualResetEvent _exitingEvent;

    }
}
