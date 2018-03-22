using System.Drawing;

namespace OpenCGSS.StarlightDirector.UI.Controls {
    public static class ControlGeometryHelper {

        public static bool PointInRect(Point point, Rectangle rectangle) {
            return point.X >= rectangle.X && point.Y >= rectangle.Y && point.X < rectangle.Right && point.Y < rectangle.Bottom;
        }

        public static bool PointInRect(int x, int y, Rectangle rectangle) {
            return x >= rectangle.X && y >= rectangle.Y && x < rectangle.Right && y < rectangle.Bottom;
        }

    }
}
