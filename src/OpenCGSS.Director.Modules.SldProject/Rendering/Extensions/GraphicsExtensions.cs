using JetBrains.Annotations;
using MonoGame.Extended.Overlay;

namespace OpenCGSS.Director.Modules.SldProject.Rendering.Extensions {
    public static class GraphicsExtensions {

        public static void FillCircle([NotNull] this Graphics graphics, [NotNull] Brush brush, float centerX, float centerY, float radius) {
            var x = centerX - radius;
            var y = centerY - radius;
            var s = radius * 2;

            graphics.FillEllipse(brush, x, y, s, s);
        }

        public static void DrawCircle([NotNull] this Graphics graphics, [NotNull] Pen pen, float centerX, float centerY, float radius) {
            var x = centerX - radius;
            var y = centerY - radius;
            var s = radius * 2;

            graphics.DrawEllipse(pen, x, y, s, s);
        }

    }
}
