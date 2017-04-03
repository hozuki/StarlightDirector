using NativeBrush = SharpDX.Direct2D1.Brush;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public class D2DBrush : Brush {

        protected D2DBrush(D2DRenderContext context)
            : base(context) {
        }

        // Should only expose to D2DPen.
        internal D2DBrush(NativeBrush nativeBrush, D2DRenderContext context)
            : base(context) {
            NativeBrush = nativeBrush;
        }

        public virtual NativeBrush NativeBrush { get; }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                NativeBrush?.Dispose();
            }
        }

    }
}
