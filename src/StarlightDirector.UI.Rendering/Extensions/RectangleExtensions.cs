using System.Drawing;
using SharpDX.Mathematics.Interop;

namespace StarlightDirector.UI.Rendering.Extensions {
    public static class RectangleExtensions {

        public static RawRectangle ToD2DRect(this Rectangle rect) {
            return new RawRectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static Rectangle ToGdiRect(this RawRectangle rect) {
            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

    }
}
