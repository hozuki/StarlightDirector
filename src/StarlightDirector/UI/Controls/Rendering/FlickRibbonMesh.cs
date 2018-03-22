using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Controls.Previewing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    internal sealed class FlickRibbonMesh : RibbonMesh {

        public FlickRibbonMesh(Graphics context, Note startNote, Note endNote, double now)
            : base(context, startNote, endNote, now) {
        }

        protected override void BuildVertices() {
            var now = Now;
            var context = RenderContext;

            var xs = new float[JointCount];
            var ys = new float[JointCount];
            var rs = new float[JointCount];

            var x1 = NotesLayerUtils.GetNoteXPosition(context, now, StartNote, true, true);
            var x2 = NotesLayerUtils.GetNoteXPosition(context, now, EndNote, true, true);
            var y1 = NotesLayerUtils.GetNoteYPosition(context, now, StartNote, true, true);
            var y2 = NotesLayerUtils.GetNoteYPosition(context, now, EndNote, true, true);
            var r1 = NotesLayerUtils.GetNoteRadius(now, StartNote);
            var r2 = NotesLayerUtils.GetNoteRadius(now, EndNote);
            for (var i = 0; i < JointCount; ++i) {
                var t = (float)i / (JointCount - 1);
                xs[i] = MathHelper.Lerp(x1, x2, t);
                ys[i] = MathHelper.Lerp(y1, y2, t);
                rs[i] = MathHelper.Lerp(r1, r2, t);
            }

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
                var vertex1 = new Vector2(x - r * sin, y - r * cos);
                var vertex2 = new Vector2(x + r * sin, y + r * cos);

                Vertices[i * 2] = vertex1;
                Vertices[i * 2 + 1] = vertex2;
            }
        }

    }
}
