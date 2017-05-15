using System.Drawing;
using SharpDX.Direct2D1;
using StarlightDirector.UI.Rendering.Extensions;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public struct D2DArcSegment {

        public D2DArcSize ArcSize { get; set; }

        public PointF Point { get; set; }

        public float RotationAngle { get; set; }

        public SizeF Size { get; set; }

        public D2DSweepDirection SweepDirection { get; set; }

        internal ArcSegment ToNative() {
            return new ArcSegment {
                ArcSize = (ArcSize)ArcSize,
                Point = Point.ToD2DVector(),
                RotationAngle = RotationAngle,
                Size = Size.ToD2DSize(),
                SweepDirection = (SweepDirection)SweepDirection
            };
        }

    }
}
