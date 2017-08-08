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
            if (!NotesLayerUtils.IsNoteComing(startNote, now) && !NotesLayerUtils.IsNotePassed(endNote, now)) {
                var mesh = new RibbonMesh(context, startNote, endNote, now, ConnectionType.Slide);
                mesh.Fill(_ribbonBrush);
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
