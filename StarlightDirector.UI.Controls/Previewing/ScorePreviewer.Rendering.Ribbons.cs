using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Previewing {
    partial class ScorePreviewer {

        private void DrawHoldRibbon(D2DRenderContext context, double now, Note startNote, Note endNote) {
            OnStageStatus s1 = NotesLayerUtils.GetNoteOnStageStatus(startNote, now), s2 = NotesLayerUtils.GetNoteOnStageStatus(endNote, now);
            if (s1 == s2 && s1 != OnStageStatus.OnStage) {
                return;
            }

            var mesh = new RibbonMesh(context, startNote, endNote, now, ConnectionType.Hold);
            mesh.Fill(_ribbonBrush);
        }

        private void DrawSlideRibbon(D2DRenderContext context, double now, Note startNote, Note endNote) {
            if (endNote.Helper.IsFlick) {
                DrawFlickRibbon(context, now, startNote, endNote);
                return;
            }
            if (startNote.Helper.IsSlideEnd || NotesLayerUtils.IsNoteOnStage(startNote, now)) {
                DrawHoldRibbon(context, now, startNote, endNote);
                return;
            }
            if (NotesLayerUtils.IsNotePassed(startNote, now)) {
                var nextSlideNote = startNote.Editor.NextSlide;
                if (nextSlideNote == null) {
                    // Actually, here is an example of invalid format. :)
                    DrawHoldRibbon(context, now, startNote, endNote);
                    return;
                }
                if (NotesLayerUtils.IsNotePassed(nextSlideNote, now)) {
                    return;
                }
                var startX = NotesLayerUtils.GetEndXByNotePosition(context.ClientSize, startNote.Basic.FinishPosition);
                var endX = NotesLayerUtils.GetEndXByNotePosition(context.ClientSize, nextSlideNote.Basic.FinishPosition);
                var y1 = NotesLayerUtils.GetAvatarYPosition(context.ClientSize);
                var x1 = (float)((now - startNote.Temporary.HitTiming.TotalSeconds) / (nextSlideNote.Temporary.HitTiming.TotalSeconds - startNote.Temporary.HitTiming.TotalSeconds)) * (endX - startX) + startX;
                float t1 = NotesLayerUtils.GetNoteTransformedTime(now, startNote, clampComing: true, clampPassed: true);
                float t2 = NotesLayerUtils.GetNoteTransformedTime(now, endNote, clampComing: true, clampPassed: true);
                float tmid = (t1 + t2) * 0.5f;
                float x2 = NotesLayerUtils.GetNoteXPosition(context, endNote.Basic.FinishPosition, t2);
                float xmid = NotesLayerUtils.GetNoteXPosition(context, endNote.Basic.FinishPosition, tmid);
                float y2 = NotesLayerUtils.GetNoteYPosition(context, t2);
                float ymid = NotesLayerUtils.GetNoteYPosition(context, tmid);
                NotesLayerUtils.GetBezierFromQuadratic(x1, xmid, x2, out float xcontrol1, out float xcontrol2);
                NotesLayerUtils.GetBezierFromQuadratic(y1, ymid, y2, out float ycontrol1, out float ycontrol2);
                // TODO:
                //context.DrawBezier(ConnectionPen, x1, y1, xcontrol1, ycontrol1, xcontrol2, ycontrol2, x2, y2);
            }
        }

        private void DrawFlickRibbon(D2DRenderContext context, double now, Note startNote, Note endNote) {
            OnStageStatus s1 = NotesLayerUtils.GetNoteOnStageStatus(startNote, now), s2 = NotesLayerUtils.GetNoteOnStageStatus(endNote, now);
            if (s1 != OnStageStatus.OnStage && s2 != OnStageStatus.OnStage && s1 == s2) {
                return;
            }

            var mesh = new RibbonMesh(context, startNote, endNote, now, ConnectionType.Flick);
            mesh.Fill(_ribbonBrush);
        }

    }
}
