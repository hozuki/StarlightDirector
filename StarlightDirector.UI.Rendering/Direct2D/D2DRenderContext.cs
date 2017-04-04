using System;
using System.Drawing;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using StarlightDirector.UI.Rendering.Extensions;
using Factory = SharpDX.DirectWrite.Factory;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public sealed class D2DRenderContext : RenderContext {

        public D2DRenderContext(RenderTarget renderTarget, Factory dwriteFactory, Size clientSize)
            : base(clientSize) {
            RenderTarget = renderTarget;
            DirectWriteFactory = dwriteFactory;
        }

        public RenderTarget RenderTarget { get; }

        public Factory DirectWriteFactory { get; }

        public override void BeginDraw() {
            RenderTarget.BeginDraw();
        }

        public override void EndDraw() {
            RenderTarget.EndDraw();
        }

        public void Clear(Color clearColor) {
            RenderTarget.Clear(clearColor.ToRC4());
        }

        public void Clear(RawColor4 clearColor) {
            RenderTarget.Clear(clearColor);
        }

        public override void Clear() {
            RenderTarget.Clear(null);
        }

        public override void FillRectangle(Brush brush, float x, float y, float width, float height) {
            var rectF = new RawRectangleF(x, y, x + width, y + height);
            var d2dBrush = brush.AsD2DBrush();
            RenderTarget.FillRectangle(rectF, d2dBrush.NativeBrush);
        }

        public override void FillEllipse(Brush brush, float x, float y, float width, float height) {
            var ellipse = new Ellipse(new RawVector2(x + width / 2, y + height / 2), width / 2, height / 2);
            var d2dBrush = brush.AsD2DBrush();
            RenderTarget.FillEllipse(ellipse, d2dBrush.NativeBrush);
        }

        public override void FillPolygon(Brush brush, FillMode fillMode, params PointF[] points) {
            if (points == null) {
                throw new ArgumentNullException(nameof(points));
            }
            if (points.Length == 0) {
                return;
            }
            var target = RenderTarget;
            using (var path = new PathGeometry(target.Factory)) {
                using (var sink = path.Open()) {
                    sink.BeginFigure(new RawVector2(points[0].X, points[0].Y), FigureBegin.Filled);
                    var len = points.Length;
                    for (var i = 1; i < len; ++i) {
                        var pt = points[i];
                        sink.AddLine(new RawVector2(pt.X, pt.Y));
                    }
                    sink.EndFigure(FigureEnd.Closed);
                    sink.Close();
                }
                var d2dBrush = brush.AsD2DBrush();
                target.FillGeometry(path, d2dBrush.NativeBrush);
            }
        }

        public override void DrawLine(Pen pen, float x1, float y1, float x2, float y2) {
            var pt1 = new RawVector2(x1, y1);
            var pt2 = new RawVector2(x2, y2);
            var d2dPen = pen.AsD2DPen();
            var d2dBrush = d2dPen.Brush.AsD2DBrush();
            RenderTarget.DrawLine(pt1, pt2, d2dBrush.NativeBrush, d2dPen.StrokeWidth, d2dPen.StrokeStyle);
        }

        public override void DrawBezier(Pen pen, float x1, float y1, float cx1, float cy1, float cx2, float cy2, float x2, float y2) {
            var target = RenderTarget;
            using (var path = new PathGeometry(target.Factory)) {
                using (var sink = path.Open()) {
                    var bezier = new BezierSegment {
                        Point1 = new RawVector2(cx1, cx2),
                        Point2 = new RawVector2(cx2, cy2),
                        Point3 = new RawVector2(x2, y2)
                    };
                    sink.BeginFigure(new RawVector2(x1, y1), FigureBegin.Filled);
                    sink.AddBezier(bezier);
                    sink.EndFigure(FigureEnd.Closed);
                    sink.Close();
                }
                var d2dPen = pen.AsD2DPen();
                var d2dBrush = d2dPen.Brush.AsD2DBrush();
                target.DrawGeometry(path, d2dBrush.NativeBrush, d2dPen.StrokeWidth, d2dPen.StrokeStyle);
            }
        }

        public override void DrawQuadraticBezier(Pen pen, float x1, float y1, float cx, float cy, float x2, float y2) {
            var target = RenderTarget;
            using (var path = new PathGeometry(target.Factory)) {
                using (var sink = path.Open()) {
                    var bezier = new QuadraticBezierSegment {
                        Point1 = new RawVector2(cx, cy),
                        Point2 = new RawVector2(x2, y2),
                    };
                    sink.BeginFigure(new RawVector2(x1, y1), FigureBegin.Filled);
                    sink.AddQuadraticBezier(bezier);
                    sink.EndFigure(FigureEnd.Closed);
                    sink.Close();
                }
                var d2dPen = pen.AsD2DPen();
                var d2dBrush = d2dPen.Brush.AsD2DBrush();
                target.DrawGeometry(path, d2dBrush.NativeBrush, d2dPen.StrokeWidth, d2dPen.StrokeStyle);
            }
        }

        public override void DrawRectangle(Pen pen, float x, float y, float width, float height) {
            var rect = new RawRectangleF(x, y, x + width, y + height);
            var d2dPen = pen.AsD2DPen();
            var d2dBrush = d2dPen.Brush.AsD2DBrush();
            RenderTarget.DrawRectangle(rect, d2dBrush.NativeBrush, d2dPen.StrokeWidth, d2dPen.StrokeStyle);
        }

        public override void DrawEllipse(Pen pen, float x, float y, float width, float height) {
            var ellipse = new Ellipse(new RawVector2(x + width / 2, y + height / 2), width / 2, height / 2);
            var d2dPen = pen.AsD2DPen();
            var d2dBrush = d2dPen.Brush.AsD2DBrush();
            RenderTarget.DrawEllipse(ellipse, d2dBrush.NativeBrush, d2dPen.StrokeWidth, d2dPen.StrokeStyle);
        }

        public override void DrawPolygon(Pen pen, params PointF[] points) {
            if (points == null) {
                throw new ArgumentNullException(nameof(points));
            }
            if (points.Length == 0) {
                return;
            }
            var target = RenderTarget;
            using (var path = new PathGeometry(target.Factory)) {
                using (var sink = path.Open()) {
                    sink.BeginFigure(new RawVector2(points[0].X, points[0].Y), FigureBegin.Filled);
                    var len = points.Length;
                    for (var i = 1; i < len; ++i) {
                        var pt = points[i];
                        sink.AddLine(new RawVector2(pt.X, pt.Y));
                    }
                    sink.EndFigure(FigureEnd.Closed);
                    sink.Close();
                }
                var d2dPen = pen.AsD2DPen();
                var d2dBrush = d2dPen.Brush.AsD2DBrush();
                target.DrawGeometry(path, d2dBrush.NativeBrush, d2dPen.StrokeWidth, d2dPen.StrokeStyle);
            }
        }

        public override void DrawImage(Image image, float destX, float destY, float destWidth, float destHeight) {
            var d2dImage = image.AsD2DImage();
            var destRect = new RawRectangleF(destX, destY, destX + destWidth, destY + destHeight);
            RenderTarget.DrawBitmap(d2dImage.NativeImage, destRect, 1f, BitmapInterpolationMode.Linear);
        }

        public override void DrawImage(Image image, float destX, float destY, float destWidth, float destHeight, float srcX, float srcY, float srcWidth, float srcHeight) {
            var d2dImage = image.AsD2DImage();
            var destRect = new RawRectangleF(destX, destY, destX + destWidth, destY + destHeight);
            var srcRect = new RawRectangleF(srcX, srcY, srcX + srcWidth, srcY + srcHeight);
            RenderTarget.DrawBitmap(d2dImage.NativeImage, destRect, 1f, BitmapInterpolationMode.Linear, srcRect);
        }

        public override void DrawText(string text, Brush brush, Font font, float destX, float destY, float destWidth, float destHeight) {
            var d2dFont = font.AsD2DFont();
            var d2dBrush = brush.AsD2DBrush();
            var destRect = new RawRectangleF(destX, destY, destX + destWidth, destY + destHeight);
            RenderTarget.DrawText(text, d2dFont.NativeFont, destRect, d2dBrush.NativeBrush);
        }

        public override SizeF MeasureText(string text, Font font) {
            var d2dFont = font.AsD2DFont();
            using (var layout = new TextLayout(DirectWriteFactory, text, d2dFont.NativeFont, float.MaxValue, float.MaxValue)) {
                var width = layout.DetermineMinWidth();
                var hasNewLine = text.IndexOf('\n') >= 0;
                if (hasNewLine) {
                    return new SizeF(width, 0);
                } else {
                    return new SizeF(width, font.Size * 4 / 3);
                }
            }
        }

    }
}
