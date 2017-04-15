using System.Diagnostics;
using System.Drawing;

namespace StarlightDirector.UI.Controls.Extensions {
    public static class GraphicsExtensions {

        [DebuggerStepThrough]
        public static void FillRectangle(this Graphics graphics, Color color, RectangleF rect) {
            using (var b = new SolidBrush(color)) {
                graphics.FillRectangle(b, rect);
            }
        }

        [DebuggerStepThrough]
        public static void FillRectangle(this Graphics graphics, Color color, Rectangle rect) {
            using (var b = new SolidBrush(color)) {
                graphics.FillRectangle(b, rect);
            }
        }

        [DebuggerStepThrough]
        public static void FillRectangle(this Graphics graphics, Color color, float x, float y, float width, float height) {
            using (var b = new SolidBrush(color)) {
                graphics.FillRectangle(b, x, y, width, height);
            }
        }

        [DebuggerStepThrough]
        public static void FillRectangle(this Graphics graphics, Color color, int x, int y, int width, int height) {
            using (var b = new SolidBrush(color)) {
                graphics.FillRectangle(b, x, y, width, height);
            }
        }

        [DebuggerStepThrough]
        public static void DrawLine(this Graphics graphics, Color color, Point pt1, Point pt2) {
            using (var p = new Pen(color)) {
                graphics.DrawLine(p, pt1, pt2);
            }
        }

        [DebuggerStepThrough]
        public static void DrawLine(this Graphics graphics, Color color, PointF pt1, PointF pt2) {
            using (var p = new Pen(color)) {
                graphics.DrawLine(p, pt1, pt2);
            }
        }

        [DebuggerStepThrough]
        public static void DrawLine(this Graphics graphics, Color color, int x1, int y1, int x2, int y2) {
            using (var p = new Pen(color)) {
                graphics.DrawLine(p, x1, y1, x2, y2);
            }
        }

        [DebuggerStepThrough]
        public static void DrawLine(this Graphics graphics, Color color, float x1, float y1, float x2, float y2) {
            using (var p = new Pen(color)) {
                graphics.DrawLine(p, x1, y1, x2, y2);
            }
        }

        [DebuggerStepThrough]
        public static void DrawLine(this Graphics graphics, Color color, float width, Point pt1, Point pt2) {
            using (var p = new Pen(color, width)) {
                graphics.DrawLine(p, pt1, pt2);
            }
        }

        [DebuggerStepThrough]
        public static void DrawLine(this Graphics graphics, Color color, float width, PointF pt1, PointF pt2) {
            using (var p = new Pen(color, width)) {
                graphics.DrawLine(p, pt1, pt2);
            }
        }

        [DebuggerStepThrough]
        public static void DrawLine(this Graphics graphics, Color color, float width, int x1, int y1, int x2, int y2) {
            using (var p = new Pen(color, width)) {
                graphics.DrawLine(p, x1, y1, x2, y2);
            }
        }

        [DebuggerStepThrough]
        public static void DrawLine(this Graphics graphics, Color color, float width, float x1, float y1, float x2, float y2) {
            using (var p = new Pen(color, width)) {
                graphics.DrawLine(p, x1, y1, x2, y2);
            }
        }

        [DebuggerStepThrough]
        public static void DrawRectangle(this Graphics graphics, Color color, Rectangle rect) {
            using (var p = new Pen(color)) {
                graphics.DrawRectangle(p, rect);
            }
        }

        [DebuggerStepThrough]
        public static void DrawRectangle(this Graphics graphics, Color color, float width, Rectangle rect) {
            using (var p = new Pen(color, width)) {
                graphics.DrawRectangle(p, rect);
            }
        }

    }
}
