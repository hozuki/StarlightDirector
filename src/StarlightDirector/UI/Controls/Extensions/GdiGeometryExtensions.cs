using System.Drawing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Extensions {
    internal static class GdiGeometryExtensions {

        internal static Microsoft.Xna.Framework.Point ToXna(this Point pt) {
            return new Microsoft.Xna.Framework.Point(pt.X, pt.Y);
        }

        internal static Microsoft.Xna.Framework.Rectangle ToXna(this Rectangle rectangle) {
            return new Microsoft.Xna.Framework.Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

    }
}
