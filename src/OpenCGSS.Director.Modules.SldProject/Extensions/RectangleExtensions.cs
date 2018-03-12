using Microsoft.Xna.Framework;

namespace OpenCGSS.Director.Modules.SldProject.Extensions {
    internal static class RectangleExtensions {

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

    }
}
