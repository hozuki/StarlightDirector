using System;
using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Controls.Previewing;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Rendering {
    internal sealed class SlideRibbonMesh : RibbonMesh {

        public SlideRibbonMesh(D2DRenderContext context, Note startNote, Note endNote, double now)
            : base(context, startNote, endNote, now) {
        }

        protected override void BuildVertices() {
            var startTiming = StartNote.Temporary.HitTiming.TotalSeconds;
            var now = Now;
            var context = RenderContext;

            var xs = new float[JointCount];
            var ys = new float[JointCount];
            var rs = new float[JointCount];

            var x2 = NotesLayerUtils.GetNoteXPosition(context, now, EndNote, true, true);
            var y2 = NotesLayerUtils.GetNoteYPosition(context, now, EndNote, true, true);
            var r2 = NotesLayerUtils.GetNoteRadius(now, EndNote);
            float x1, y1, r1;
            if (now <= startTiming) {
                x1 = NotesLayerUtils.GetNoteXPosition(context, now, StartNote, true, true);
                y1 = NotesLayerUtils.GetNoteYPosition(context, now, StartNote, true, true);
                r1 = NotesLayerUtils.GetNoteRadius(now, StartNote);
            } else {
                var startPosition = StartNote.Basic.FinishPosition;
                var endPosition = EndNote.Basic.FinishPosition;
                var tx1 = NotesLayerUtils.GetAvatarXPosition(context.ClientSize, startPosition);
                var tx2 = NotesLayerUtils.GetAvatarXPosition(context.ClientSize, endPosition);
                var endTiming = EndNote.Temporary.HitTiming.TotalSeconds;
                var tr = (float)(now - startTiming) / (float)(endTiming - startTiming);
                x1 = tx1 * (1 - tr) + tx2 * tr;
                y1 = NotesLayerUtils.GetAvatarYPosition(context.ClientSize);
                r1 = Definitions.AvatarCircleRadius;
            }
            for (var i = 0; i < JointCount; ++i) {
                var t = (float)i / (JointCount - 1);
                xs[i] = D2DHelper.Lerp(x1, x2, t);
                ys[i] = D2DHelper.Lerp(y1, y2, t);
                rs[i] = D2DHelper.Lerp(r1, r2, t);
            }

            var vertices = Vertices;
            for (var i = 0; i < JointCount; ++i) {
                var x = xs[i];
                var y = ys[i];
                var r = rs[i];
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
                var vertex1 = new PointF(x - r * sin, y - r * cos);
                var vertex2 = new PointF(x + r * sin, y + r * cos);
                vertices[i * 2] = vertex1;
                vertices[i * 2 + 1] = vertex2;
            }
        }

    }
}
