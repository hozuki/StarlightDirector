using System;
using System.Drawing;
using System.Linq;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using StarlightDirector.UI.Rendering.Extensions;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public sealed class D2DLinearGradientBrush : D2DBrush {

        public D2DLinearGradientBrush(D2DRenderContext context, PointF startPoint, PointF endPoint, params Color[] gradientColors)
            : base(context) {
            if (gradientColors.Length < 2) {
                throw new ArgumentException("Linear gradient brush requires at least 2 colors.", nameof(gradientColors));
            }
            var properties = new LinearGradientBrushProperties {
                StartPoint = new RawVector2(startPoint.X, startPoint.Y),
                EndPoint = new RawVector2(endPoint.X, endPoint.Y)
            };
            var colorCount = gradientColors.Length;
            var gradientStops = new GradientStop[colorCount];
            for (var i = 0; i < colorCount; ++i) {
                gradientStops[i] = new GradientStop {
                    Color = gradientColors[i].ToRC4(),
                    Position = (float)i / (colorCount - 1)
                };
            }
            var collection = new GradientStopCollection(context.RenderTarget, gradientStops);
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public D2DLinearGradientBrush(D2DRenderContext context, Point startPoint, Point endPoint, params Color[] gradientColors)
            : base(context) {
            if (gradientColors.Length < 2) {
                throw new ArgumentException("Linear gradient brush requires at least 2 colors.", nameof(gradientColors));
            }
            var properties = new LinearGradientBrushProperties {
                StartPoint = new RawVector2(startPoint.X, startPoint.Y),
                EndPoint = new RawVector2(endPoint.X, endPoint.Y)
            };
            var colorCount = gradientColors.Length;
            var gradientStops = new GradientStop[colorCount];
            for (var i = 0; i < colorCount; ++i) {
                gradientStops[i] = new GradientStop {
                    Color = gradientColors[i].ToRC4(),
                    Position = (float)i / (colorCount - 1)
                };
            }
            var collection = new GradientStopCollection(context.RenderTarget, gradientStops);
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public D2DLinearGradientBrush(D2DRenderContext context, PointF startPoint, PointF endPoint, params (Color Color, float Position)[] gradientStops)
            : base(context) {
            var properties = new LinearGradientBrushProperties {
                StartPoint = new RawVector2(startPoint.X, startPoint.Y),
                EndPoint = new RawVector2(endPoint.X, endPoint.Y)
            };
            var collection = new GradientStopCollection(context.RenderTarget, gradientStops.Select(t => new GradientStop {
                Color = t.Color.ToRC4(),
                Position = t.Position
            }).ToArray());
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public D2DLinearGradientBrush(D2DRenderContext context, Point startPoint, Point endPoint, params (Color Color, float Position)[] gradientStops)
            : base(context) {
            var properties = new LinearGradientBrushProperties {
                StartPoint = new RawVector2(startPoint.X, startPoint.Y),
                EndPoint = new RawVector2(endPoint.X, endPoint.Y)
            };
            var collection = new GradientStopCollection(context.RenderTarget, gradientStops.Select(t => new GradientStop {
                Color = t.Color.ToRC4(),
                Position = t.Position
            }).ToArray());
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public D2DLinearGradientBrush(D2DRenderContext context, PointF startPoint, PointF endPoint, params GradientStop[] gradientStops)
            : base(context) {
            var properties = new LinearGradientBrushProperties {
                StartPoint = new RawVector2(startPoint.X, startPoint.Y),
                EndPoint = new RawVector2(endPoint.X, endPoint.Y)
            };
            var collection = new GradientStopCollection(context.RenderTarget, gradientStops);
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public D2DLinearGradientBrush(D2DRenderContext context, Point startPoint, Point endPoint, params GradientStop[] gradientStops)
            : base(context) {
            var properties = new LinearGradientBrushProperties {
                StartPoint = new RawVector2(startPoint.X, startPoint.Y),
                EndPoint = new RawVector2(endPoint.X, endPoint.Y)
            };
            var collection = new GradientStopCollection(context.RenderTarget, gradientStops);
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public D2DLinearGradientBrush(D2DRenderContext context, PointF startPoint, PointF endPoint, GradientStopCollection collection)
            : base(context) {
            var properties = new LinearGradientBrushProperties {
                StartPoint = new RawVector2(startPoint.X, startPoint.Y),
                EndPoint = new RawVector2(endPoint.X, endPoint.Y)
            };
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public D2DLinearGradientBrush(D2DRenderContext context, Point startPoint, Point endPoint, GradientStopCollection collection)
            : base(context) {
            var properties = new LinearGradientBrushProperties {
                StartPoint = new RawVector2(startPoint.X, startPoint.Y),
                EndPoint = new RawVector2(endPoint.X, endPoint.Y)
            };
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public D2DLinearGradientBrush(D2DRenderContext context, LinearGradientBrushProperties properties, GradientStopCollection collection)
            : base(context) {
            _brush = new LinearGradientBrush(context.RenderTarget, properties, collection);
            _collection = collection;
        }

        public override SharpDX.Direct2D1.Brush NativeBrush => _brush;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _collection.Dispose();
            }
            base.Dispose(disposing);
        }

        private readonly LinearGradientBrush _brush;
        private readonly GradientStopCollection _collection;

    }
}
