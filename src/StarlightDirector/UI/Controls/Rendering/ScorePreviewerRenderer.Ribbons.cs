using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.Models;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Controls.Previewing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    partial class ScorePreviewerRenderer {

        private void DrawHoldRibbon(Graphics context, double now, Note startNote, Note endNote) {
            OnStageStatus s1 = NotesLayerUtils.GetNoteOnStageStatus(startNote, now), s2 = NotesLayerUtils.GetNoteOnStageStatus(endNote, now);
            if (s1 == s2 && s1 != OnStageStatus.OnStage) {
                return;
            }

            var mesh = new HoldRibbonMesh(context, startNote, endNote, now);
            mesh.Initialize();
            mesh.Fill(_ribbonBrush);
        }

        private void DrawSlideRibbon(Graphics context, double now, Note startNote, Note endNote) {
            if (endNote.Helper.IsFlick) {
                DrawFlickRibbon(context, now, startNote, endNote);
                return;
            }
            if (!NotesLayerUtils.IsNoteComing(startNote, now) && !NotesLayerUtils.IsNotePassed(endNote, now)) {
                var mesh = new SlideRibbonMesh(context, startNote, endNote, now);
                mesh.Initialize();
                mesh.Fill(_ribbonBrush);
            }
        }

        private void DrawFlickRibbon(Graphics context, double now, Note startNote, Note endNote) {
            OnStageStatus s1 = NotesLayerUtils.GetNoteOnStageStatus(startNote, now), s2 = NotesLayerUtils.GetNoteOnStageStatus(endNote, now);
            if (s1 != OnStageStatus.OnStage && s2 != OnStageStatus.OnStage && s1 == s2) {
                return;
            }

            var mesh = new FlickRibbonMesh(context, startNote, endNote, now);
            mesh.Initialize();
            mesh.Fill(_ribbonBrush);
        }

    }
}
