using System.Drawing;
using SharpDX.Direct2D1;
using StarlightDirector.UI.Rendering.Extensions;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public sealed class D2DPen : Pen {

        public D2DPen(Color color, D2DRenderContext context)
           : base(null, 1) {
            var nativeBrush = new SolidColorBrush(context.RenderTarget, color.ToRC4());
            Brush = new D2DBrush(nativeBrush, context);
            StrokeStyle = null;
            _isExternalBrush = false;
        }

        public D2DPen(Color color, float strokeWidth, D2DRenderContext context)
            : base(null, strokeWidth) {
            var nativeBrush = new SolidColorBrush(context.RenderTarget, color.ToRC4());
            Brush = new D2DBrush(nativeBrush, context);
            StrokeStyle = null;
            _isExternalBrush = false;
        }

        public D2DPen(D2DBrush brush)
            : this(brush, 1) {
        }

        public D2DPen(D2DBrush brush, float strokeWidth)
            : this(brush, strokeWidth, null) {
        }

        public D2DPen(D2DBrush brush, float strokeWidth, StrokeStyle style)
            : base(brush, strokeWidth) {
            StrokeStyle = style;
            _isExternalBrush = true;
        }

        public StrokeStyle StrokeStyle { get; }

        protected override void Dispose(bool disposing) {
            if (disposing && !_isExternalBrush) {
                Brush?.Dispose();
            }
        }

        private readonly bool _isExternalBrush;

    }
}
