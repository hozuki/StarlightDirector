using System.Diagnostics;
using System.Drawing;
using SharpDX.Mathematics.Interop;

namespace StarlightDirector.UI.Rendering.Extensions {
    public static class Direct2DExtensions {

        public static RawRectangleF ToRectF(this RawRectangleF rect) {
            return new RawRectangleF(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public static RawVector2 ToVector2(this RawPoint point) {
            return new RawVector2(point.X, point.Y);
        }

        [DebuggerStepThrough]
        public static RawColor4 ToRC4(this Color color) {
            var a = (float)color.A / byte.MaxValue;
            var r = (float)color.R / byte.MaxValue;
            var g = (float)color.G / byte.MaxValue;
            var b = (float)color.B / byte.MaxValue;
            return new RawColor4(r, g, b, a);
        }

    }
}
