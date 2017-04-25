using System.Drawing;

namespace StarlightDirector.UI.Controls.Extensions {
    internal static class RectangleExtensions {

        public static void Clear(this Rectangle rectangle) {
            rectangle.Location = Point.Empty;
            rectangle.Size = Size.Empty;
        }

    }
}
