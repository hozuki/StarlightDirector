using System;
using System.Drawing;
using SharpDX.Direct2D1;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Previewing {
    internal sealed class RibbonMesh {

        public RibbonMesh(D2DRenderContext context, Note startNote, Note endNote, double now, ConnectionType connectionType) {
            RenderContext = context;
            StartNote = startNote;
            EndNote = endNote;
            Now = now;
            ConnectionType = connectionType;

            if ((connectionType & ConnectionType.RawMask) == ConnectionType.Slide) {
                InitializeSlide();
            } else {
                InitializeFlick();
            }
            BuildTriangles();
        }

        public D2DRenderContext RenderContext { get; }

        public Note StartNote { get; }

        public Note EndNote { get; }

        public bool IsSlideRibbon { get; }

        public ConnectionType ConnectionType { get; }

        public void Fill(D2DBrush brush) {
            var context = RenderContext;
            var target = context.RenderTarget;
            using (var mesh = new D2DMesh(context, Triangles)) {
                // https://msdn.microsoft.com/en-us/library/dd371939.aspx
                target.AntialiasMode = AntialiasMode.Aliased;
                context.FillMesh(brush, mesh);
                target.AntialiasMode = AntialiasMode.PerPrimitive;
            }
        }

        private double Now { get; }

        private void InitializeFlick() {
            var startTiming = StartNote.Temporary.HitTiming.TotalSeconds;
            var endTiming = EndNote.Temporary.HitTiming.TotalSeconds;
            var finishPosition = EndNote.Basic.FinishPosition;
            var now = Now;
            var context = RenderContext;

            var xs = new float[JointCount];
            var ys = new float[JointCount];
            var rs = new float[JointCount];

            var rawConnection = ConnectionType & ConnectionType.RawMask;
            switch (rawConnection) {
                case ConnectionType.Hold:
                    for (var i = 0; i < JointCount; ++i) {
                        var timing = (endTiming - startTiming) / (JointCount - 1) * i + startTiming;
                        xs[i] = NotesLayerUtils.GetNoteXPosition(context, now, timing, finishPosition, true, true);
                        ys[i] = NotesLayerUtils.GetNoteYPosition(context, now, timing, true, true);
                        rs[i] = NotesLayerUtils.GetNoteRadius(now, timing);
                    }
                    break;
                case ConnectionType.Flick:
                    var x1 = NotesLayerUtils.GetNoteXPosition(context, now, StartNote, true, true);
                    var x2 = NotesLayerUtils.GetNoteXPosition(context, now, EndNote, true, true);
                    var y1 = NotesLayerUtils.GetNoteYPosition(context, now, StartNote, true, true);
                    var y2 = NotesLayerUtils.GetNoteYPosition(context, now, EndNote, true, true);
                    var r1 = NotesLayerUtils.GetNoteRadius(now, StartNote);
                    var r2 = NotesLayerUtils.GetNoteRadius(now, EndNote);
                    for (var i = 0; i < JointCount; ++i) {
                        var t = (float)i / (JointCount - 1);
                        xs[i] = D2DHelper.Lerp(x1, x2, t);
                        ys[i] = D2DHelper.Lerp(y1, y2, t);
                        rs[i] = D2DHelper.Lerp(r1, r2, t);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rawConnection));
            }

            for (var i = 0; i < JointCount; ++i) {
                var x = xs[i];
                var y = ys[i];
                var r = rs[i];
                PointF vertex1, vertex2;
                switch (rawConnection) {
                    case ConnectionType.Hold:
                        vertex1 = new PointF(x - r, y);
                        vertex2 = new PointF(x + r, y);
                        break;
                    case ConnectionType.Flick:
                        float ydif, xdif;
                        if (i == JointCount - 1) {
                            ydif = y - ys[i - 1];
                            xdif = x - xs[i - 1];
                        } else {
                            ydif = ys[i + 1] - y;
                            xdif = xs[i + 1] - x;
                        }
                        var rad = (float)Math.Atan2(ydif, xdif);
                        var cos = (float)Math.Cos(rad);
                        var sin = (float)Math.Sin(rad);
                        vertex1 = new PointF(x - r * sin, y - r * cos);
                        vertex2 = new PointF(x + r * sin, y + r * cos);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(rawConnection));
                }
                Vertices[i * 2] = vertex1;
                Vertices[i * 2 + 1] = vertex2;
            }
        }

        private void InitializeSlide() {
            throw new NotImplementedException();
        }

        private void BuildTriangles() {
            for (var i = 0; i < JointCount - 1; ++i) {
                var t1 = new D2DTriangle();
                var t2 = new D2DTriangle();
                PointF v1 = Vertices[i * 2], v2 = Vertices[i * 2 + 1], v3 = Vertices[i * 2 + 3], v4 = Vertices[i * 2 + 2];
                t1.Point1 = v1;
                t1.Point2 = v2;
                t1.Point3 = v4;
                t2.Point1 = v2;
                t2.Point2 = v3;
                t2.Point3 = v4;
                Triangles[i * 2] = t1;
                Triangles[i * 2 + 1] = t2;
            }
        }

        private static readonly Color[] RainbowColors = {
            D2DHelper.GetFloatColor(0.8f, 1f, 0.65f, 0.65f),
            D2DHelper.GetFloatColor(0.8f, 0.65f, 1f, 0.65f),
            D2DHelper.GetFloatColor(0.8f, 0.65f, 0.65f, 1f)
        };

        private static readonly Color[] CoolColors = {
            D2DHelper.GetFloatColor(0.8f, 0.211764708f, 0.929411769f, 0.992156863f),
            D2DHelper.GetFloatColor(0.8f, 0.164705887f, 0.478431374f, 1f)
        };

        private static readonly Color[] CuteColors = {
            D2DHelper.GetFloatColor(0.8f, 0.921568632f, 0.6784314f, 0.694117665f),
            D2DHelper.GetFloatColor(0.8f, 1f, 0.3019608f, 0.533333361f)
        };

        private static readonly Color[] PassionColors = {
            D2DHelper.GetFloatColor(0.8f, 1f, 0.827451f, 0.129411772f),
            D2DHelper.GetFloatColor(0.8f, 0.854901969f, 0.5647059f, 0f)
        };

        private static readonly Color[] SimpleWhiteColors = {
            D2DHelper.GetFloatColor(0.8f, 0.784313738f, 0.784313738f, 0.784313738f),
            D2DHelper.GetFloatColor(0.8f, 1f, 1f, 1f)
        };

        private const int JointCount = 48;

        private const float Unk = -160;

        private readonly PointF[] Vertices = new PointF[JointCount * 2];
        private D2DTriangle[] Triangles = new D2DTriangle[2 * (JointCount - 1)];

    }
}