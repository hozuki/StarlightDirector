using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using MonoGame.Extended.Overlay;

namespace OpenCGSS.StarlightDirector.UI.Controls.Extensions {
    internal static class GraphicsExtensions {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void FillCircle([NotNull] this Graphics graphics, [NotNull] Brush brush, float centerX, float centerY, float radius) {
            graphics.FillEllipse(brush, centerX - radius, centerY - radius, radius * 2, radius * 2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void DrawCircle([NotNull] this Graphics graphics, [NotNull] Pen pen, float centerX, float centerY, float radius) {
            graphics.DrawEllipse(pen, centerX - radius, centerY - radius, radius * 2, radius * 2);
        }

    }
}
