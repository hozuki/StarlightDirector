using Microsoft.Xna.Framework;

namespace OpenCGSS.StarlightDirector.UI.Controls.Extensions {
    internal static class XnaGeometryExtensions {

        public static bool ContainsAdjusted(this Rectangle rect, float x, float y) {
            float l = rect.Left, r = rect.Right, t = rect.Top, b = rect.Bottom;
            float tmp;
            if (r < l) {
                tmp = l;
                l = r;
                r = tmp;
            }
            if (b < t) {
                tmp = b;
                b = t;
                t = tmp;
            }
            return l <= x && x < r && t <= y && y < b;
        }

        public static bool ContainsAdjusted(this Rectangle rect, Vector2 pt) {
            return ContainsAdjusted(rect, pt.X, pt.Y);
        }

        public static bool ContainsAdjusted(this Rectangle rect, Point pt) {
            return ContainsAdjusted(rect, pt.X, pt.Y);
        }

    }
}
