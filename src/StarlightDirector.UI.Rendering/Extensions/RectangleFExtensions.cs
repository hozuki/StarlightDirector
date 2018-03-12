using System.Drawing;
using SharpDX.Mathematics.Interop;

namespace StarlightDirector.UI.Rendering.Extensions {
    public static class RectangleFExtensions {

        public static RawRectangleF ToD2DRectF(this RectangleF rect) {
            return new RawRectangleF(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static RectangleF ToGdiRectF(this RawRectangleF rect) {
            return new RectangleF(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

    }
}
