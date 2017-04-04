using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using StarlightDirector.UI.Rendering.Extensions;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public sealed class D2DSolidBrush : D2DBrush {

        public D2DSolidBrush(D2DRenderContext context, Color color)
            : base(context) {
            _brush = new SolidColorBrush(context.RenderTarget, color.ToRC4());
        }

        public D2DSolidBrush(D2DRenderContext context, RawColor4 color)
            : base(context) {
            _brush = new SolidColorBrush(context.RenderTarget, color);
        }

        public override SharpDX.Direct2D1.Brush NativeBrush => _brush;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _brush.Dispose();
            }
            base.Dispose(disposing);
        }

        private readonly SolidColorBrush _brush;

    }
}
