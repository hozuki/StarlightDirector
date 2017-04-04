using System.Drawing;

namespace StarlightDirector.UI.Rendering {
    public abstract class RenderContext {

        protected RenderContext(Size clientSize) {
            ClientSize = clientSize;
        }

        public SizeF ClientSize { get; private set; }

        public abstract void BeginDraw();

        public abstract void EndDraw();

        public abstract void Clear();

        public virtual void Resize(float width, float height) {
            ClientSize = new SizeF(width, height);
        }

        public abstract void FillRectangle(Brush brush, float x, float y, float width, float height);

        public void FillRectangle(Brush brush, RectangleF rect) {
            FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void FillRectangle(Brush brush, Rectangle rect) {
            FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public abstract void FillEllipse(Brush brush, float x, float y, float width, float height);

        public void FillEllipse(Brush brush, RectangleF rect) {
            FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void FillEllipse(Brush brush, Rectangle rect) {
            FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public abstract void FillPolygon(Brush brush, FillMode fillMode, params PointF[] points);

        public void FillPolygon(Brush brush, params PointF[] points) {
            FillPolygon(brush, FillMode.Alternate, points);
        }

        public void FillPolygon(Brush brush, FillMode fillMode, params Point[] points) {
            var pts = PointsToPointFs(points);
            FillPolygon(brush, fillMode, pts);
        }

        public void FillPolygon(Brush brush, params Point[] points) {
            FillPolygon(brush, FillMode.Alternate, points);
        }

        public abstract void DrawLine(Pen pen, float x1, float y1, float x2, float y2);

        public void DrawLine(Pen pen, Point pt1, Point pt2) {
            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        public void DrawLine(Pen pen, PointF pt1, PointF pt2) {
            DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        public abstract void DrawBezier(Pen pen, float x1, float y1, float cx1, float cy1, float cx2, float cy2, float x2, float y2);

        public void DrawBezier(Pen pen, Point start, Point control1, Point control2, Point end) {
            DrawBezier(pen, start.X, start.Y, control1.X, control1.Y, control2.X, control2.Y, end.X, end.Y);
        }

        public void DrawBezier(Pen pen, PointF start, PointF control1, PointF control2, PointF end) {
            DrawBezier(pen, start.X, start.Y, control1.X, control1.Y, control2.X, control2.Y, end.X, end.Y);
        }

        public abstract void DrawQuadraticBezier(Pen pen, float x1, float y1, float cx, float cy, float x2, float y2);

        public void DrawQuadraticBezier(Pen pen, Point start, Point control, Point end) {
            DrawQuadraticBezier(pen, start.X, start.Y, control.X, control.Y, end.X, end.Y);
        }

        public void DrawQuadraticBezier(Pen pen, PointF start, PointF control, PointF end) {
            DrawQuadraticBezier(pen, start.X, start.Y, control.X, control.Y, end.X, end.Y);
        }

        public abstract void DrawRectangle(Pen pen, float x, float y, float width, float height);

        public void DrawRectangle(Pen pen, Rectangle rect) {
            DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void DrawRectangle(Pen pen, RectangleF rect) {
            DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public abstract void DrawEllipse(Pen pen, float x, float y, float width, float height);

        public void DrawEllipse(Pen pen, Rectangle rect) {
            DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void DrawEllipse(Pen pen, RectangleF rect) {
            DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public abstract void DrawPolygon(Pen pen, params PointF[] points);

        public void DrawPolygon(Pen pen, params Point[] points) {
            var pts = PointsToPointFs(points);
            DrawPolygon(pen, pts);
        }

        public abstract void DrawImage(Image image, float destX, float destY, float destWidth, float destHeight);

        public void DrawImage(Image image, Rectangle rect) {
            DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void DrawImage(Image image, RectangleF rect) {
            DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
        }

        public void DrawImage(Image image, float x, float y) {
            DrawImage(image, x, y, image.Width, image.Height);
        }

        public void DrawImage(Image image, Point location) {
            DrawImage(image, location.X, location.Y, image.Width, image.Height);
        }

        public void DrawImage(Image image, PointF location) {
            DrawImage(image, location.X, location.Y, image.Width, image.Height);
        }

        public void DrawImage(Image image, Point location, Size size) {
            DrawImage(image, location.X, location.Y, size.Width, size.Height);
        }

        public void DrawImage(Image image, PointF location, SizeF size) {
            DrawImage(image, location.X, location.Y, size.Width, size.Height);
        }

        public abstract void DrawImage(Image image, float destX, float destY, float destWidth, float destHeight, float srcX, float srcY, float srcWidth, float srcHeight);

        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect) {
            DrawImage(image, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height);
        }

        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect) {
            DrawImage(image, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height);
        }

        public void DrawImage(Image image, float destX, float destY, float srcX, float srcY, float srcWidth, float srcHeight) {
            DrawImage(image, destX, destY, image.Width, image.Height, srcX, srcY, srcWidth, srcHeight);
        }

        public void DrawImage(Image image, Point destLocation, Size destSize, Point srcLocation, Size srcSize) {
            DrawImage(image, destLocation.X, destLocation.Y, destSize.Width, destSize.Height, srcLocation.X, srcLocation.Y, srcSize.Width, srcSize.Height);
        }

        public void DrawImage(Image image, PointF destLocation, SizeF destSize, PointF srcLocation, SizeF srcSize) {
            DrawImage(image, destLocation.X, destLocation.Y, destSize.Width, destSize.Height, srcLocation.X, srcLocation.Y, srcSize.Width, srcSize.Height);
        }

        public abstract void DrawText(string text, Brush brush, Font font, float destX, float destY, float destWidth, float destHeight);

        public void DrawText(string text, Brush brush, Font font, Rectangle destRect) {
            DrawText(text, brush, font, destRect.X, destRect.Y, destRect.Width, destRect.Height);
        }

        public void DrawText(string text, Brush brush, Font font, RectangleF destRect) {
            DrawText(text, brush, font, destRect.X, destRect.Y, destRect.Width, destRect.Height);
        }

        public abstract SizeF MeasureText(string text, Font font);

        private static PointF[] PointsToPointFs(Point[] points) {
            if (points.Length == 0) {
                return EmptyPointFArray;
            }
            var pts = new PointF[points.Length];
            for (var i = 0; i < pts.Length; ++i) {
                pts[i] = new PointF(points[i].X, points[i].Y);
            }
            return pts;
        }

        private static readonly PointF[] EmptyPointFArray = new PointF[0];

    }
}
