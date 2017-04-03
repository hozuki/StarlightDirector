using System.Drawing;
using StarlightDirector.Core;

namespace StarlightDirector.UI.Controls.Extensions {
    public static class ColorExtensions {

        public static Color Offset(this Color color, int offset) {
            int a = color.A;
            int r = color.R;
            int g = color.G;
            int b = color.B;
            r += offset;
            g += offset;
            b += offset;
            r = r.Clamp(byte.MinValue, byte.MaxValue);
            g = g.Clamp(byte.MinValue, byte.MaxValue);
            b = b.Clamp(byte.MinValue, byte.MaxValue);
            return Color.FromArgb(a, r, g, b);
        }

        public static Color Lerp(this Color baseColor, Color otherColor, float percent) {
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
