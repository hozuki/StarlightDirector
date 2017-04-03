using System.Drawing;

namespace StarlightDirector.UI.Rendering.Extensions {
    public static class RenderContextExtensions {

        public static void FillCircle(this RenderContext context, Brush brush, float x, float y, float radius) {
            var w = radius + radius;
            context.FillEllipse(brush, x - radius, y - radius, w, w);
        }

        public static void FillCircle(this RenderContext context, Brush brush, Point center, float radius) {
            var w = radius + radius;
            context.FillEllipse(brush, center.X - radius, center.Y - radius, w, w);
        }

        public static void FillCircle(this RenderContext context, Brush brush, PointF center, float radius) {
            var w = radius + radius;
            context.FillEllipse(brush, center.X - radius, center.Y - radius, w, w);
        }

        public static void DrawCircle(this RenderContext context, Pen pen, float x, float y, float radius) {
            var w = radius + radius;
            context.DrawEllipse(pen, x - radius, y - radius, w, w);
        }

        public static void DrawCircle(this RenderContext context, Pen pen, Point center, float radius) {
            var w = radius + radius;
            context.DrawEllipse(pen, center.X - radius, center.Y - radius, w, w);
        }

        public static void DrawCircle(this RenderContext context, Pen pen, PointF center, float radius) {
            var w = radius + radius;
            context.DrawEllipse(pen, center.X - radius, center.Y - radius, w, w);
        }

    }
}
