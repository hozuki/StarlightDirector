using System;
using System.Drawing;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls {
    public abstract class Direct2DCanvas : Control {

        public Color ClearColor { get; set; }

        protected Direct2DCanvas() {
            const ControlStyles stylesOn = ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Selectable | ControlStyles.Opaque | ControlStyles.ResizeRedraw;
            const ControlStyles stylesOff = ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer;
            SetStyle(stylesOn, true);
            SetStyle(stylesOff, false);
            ClearColor = Color.Black;
        }

        protected sealed override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing) {
                OnDisposeResources(_renderContext);
                _d3dDevice?.Dispose();
                _dxgiDevice?.Dispose();
                _dxgiSwapChain?.Dispose();
                _dxgiAdapter?.Dispose();
                _dxgiFactory?.Dispose();
                _dxgiSurface?.Dispose();
                _d2d1Device?.Dispose();
                _d2d1DeviceContext?.Dispose();
                _d2d1Factory?.Dispose();
                _d2d1BackBitmap?.Dispose();
                _dwriteFactory?.Dispose();
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            if (DesignMode) {
                return;
            }

            // https://msdn.microsoft.com/en-us/magazine/dn198239.aspx
            SharpDX.Direct3D11.Device d3dDevice = null;
            try {
                d3dDevice = new SharpDX.Direct3D11.Device(DriverType.Hardware, DeviceCreationFlags.BgraSupport);
            } catch (SharpDXException) {
            }
            if (d3dDevice == null) {
                // GPU doesn't exist.
                d3dDevice = new SharpDX.Direct3D11.Device(DriverType.Warp, DeviceCreationFlags.BgraSupport);
            }
            _d3dDevice = d3dDevice;
            _dxgiDevice = d3dDevice.QueryInterface<SharpDX.DXGI.Device>();

            _dxgiAdapter = _dxgiDevice.Adapter;
            _dxgiFactory = _dxgiAdapter.GetParent<SharpDX.DXGI.Factory2>();

            var swapChainDesc = new SwapChainDescription1 {
                Format = Format.B8G8R8A8_UNorm,
                SampleDescription = new SampleDescription(1, 0),
                Usage = Usage.RenderTargetOutput,
                BufferCount = 2
            };
            _dxgiSwapChain = new SharpDX.DXGI.SwapChain1(_dxgiFactory, d3dDevice, Handle, ref swapChainDesc);

            _d2d1Factory = new SharpDX.Direct2D1.Factory1(FactoryType.SingleThreaded);
            _d2d1Device = new SharpDX.Direct2D1.Device(_d2d1Factory, _dxgiDevice);
            _d2d1DeviceContext = new SharpDX.Direct2D1.DeviceContext(_d2d1Device, DeviceContextOptions.None);

            var clientSize = ClientSize;
            RecreateD2d1BackBitmap(clientSize.Width, clientSize.Height);

            _dwriteFactory = new SharpDX.DirectWrite.Factory();

            _renderContext = new D2DRenderContext(_d2d1DeviceContext, _dwriteFactory, ClientSize);
            OnCreateResources(_renderContext);
        }

        protected override void OnClientSizeChanged(EventArgs e) {
            base.OnClientSizeChanged(e);
            if (_renderContext == null) {
                return;
            }
            lock (_sizeLock) {
                var newSize = ClientSize;
                RecreateD2d1BackBitmap(newSize.Width, newSize.Height);
                _renderContext.Resize(newSize.Width, newSize.Height);
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            if (_renderContext == null) {
                return;
            }
            // Another method, DXGIAdapter.Output.WaitForNextVBlank()+multithreading, does not perform well though.
            lock (_sizeLock) {
                var context = _renderContext;
                context.BeginDraw();
                context.Clear(ClearColor);
                OnRender(context);
                context.EndDraw();
                _dxgiSwapChain.Present(1, PresentFlags.None);
            }
        }

        private void RecreateD2d1BackBitmap(int width, int height) {
            if (_d2d1BackBitmap != null) {
                _dxgiSurface.Dispose();
                _d2d1DeviceContext.Target = null;
                _d2d1BackBitmap.Dispose();
                _dxgiSwapChain.ResizeBuffers(0, width, height, Format.Unknown, SwapChainFlags.None);
            }
            _dxgiSurface = _dxgiSwapChain.GetBackBuffer<SharpDX.DXGI.Surface>(0);
            var dpi = _d2d1Factory.DesktopDpi;
            var bitmapProps = new SharpDX.Direct2D1.BitmapProperties1(new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore), dpi.Width, dpi.Height, BitmapOptions.Target | BitmapOptions.CannotDraw);
            _d2d1BackBitmap = new SharpDX.Direct2D1.Bitmap1(_d2d1DeviceContext, _dxgiSurface, bitmapProps);
            _d2d1DeviceContext.Target = _d2d1BackBitmap;
            _d2d1DeviceContext.DotsPerInch = dpi;
        }

        protected abstract void OnRender(D2DRenderContext context);

        protected abstract void OnCreateResources(D2DRenderContext context);

        protected abstract void OnDisposeResources(D2DRenderContext context);

        private D2DRenderContext _renderContext;
        private SharpDX.Direct3D11.Device _d3dDevice;
        private SharpDX.DXGI.Device _dxgiDevice;
        private SharpDX.DXGI.SwapChain1 _dxgiSwapChain;
        private SharpDX.DXGI.Adapter _dxgiAdapter;
        private SharpDX.DXGI.Factory2 _dxgiFactory;
        private SharpDX.DXGI.Surface _dxgiSurface;
        private SharpDX.Direct2D1.Factory1 _d2d1Factory;
        private SharpDX.Direct2D1.Device _d2d1Device;
        private SharpDX.Direct2D1.DeviceContext _d2d1DeviceContext;
        private SharpDX.Direct2D1.Bitmap1 _d2d1BackBitmap;
        private SharpDX.DirectWrite.Factory _dwriteFactory;

        private readonly object _sizeLock = new object();

    }
}
