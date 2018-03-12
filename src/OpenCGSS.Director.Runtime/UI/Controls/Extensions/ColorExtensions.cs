using Microsoft.Xna.Framework;
using Color = System.Drawing.Color;

namespace OpenCGSS.Director.UI.Controls.Extensions {
    internal static class ColorExtensions {

        internal static Color Offset(this Color color, int offset) {
            int a = color.A;
            int r = color.R;
            int g = color.G;
            int b = color.B;

            r += offset;
            g += offset;
            b += offset;

            r = MathHelper.Clamp(r, byte.MinValue, byte.MaxValue);
            g = MathHelper.Clamp(g, byte.MinValue, byte.MaxValue);
            b = MathHelper.Clamp(b, byte.MinValue, byte.MaxValue);

            return Color.FromArgb(a, r, g, b);
        }

        internal static Color Lerp(this Color baseColor, Color otherColor, float percent) {
            int sa = baseColor.A, sr = baseColor.R, sg = baseColor.G, sb = baseColor.B;
            int da = otherColor.A, dr = otherColor.R, dg = otherColor.G, db = otherColor.B;
            var a = (int)(sa * (1 - percent) + da * percent);
            var r = (int)(sr * (1 - percent) + dr * percent);
            var g = (int)(sg * (1 - percent) + dg * percent);
            var b = (int)(sb * (1 - percent) + db * percent);

            return Color.FromArgb(a, r, g, b);
        }

    }
}
