using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    internal sealed partial class ScoreEditorRenderer {

        // For ScorePreviewer
        public void Render(Graphics context, Score score, ScoreEditorConfig config, ScoreEditorLook look, float scrollOffsetY, TimeSpan now) {
            var hasAnyBar = score?.HasAnyBar ?? false;
            if (hasAnyBar) {
                RenderBars(context, score, config, look, scrollOffsetY);
            }
            var hasAnyNote = score?.HasAnyNote ?? false;
            if (hasAnyNote) {
                RenderNotes(context, score, config, look, scrollOffsetY);
            }
            if (look.TimeInfoVisible) {
                var controlArea = new Rectangle(Point.Zero, context.Bounds.Size);
                DrawBarTimePreviewInfo(context, controlArea, now);
            }
        }

        // For ScoreEditor
        public void Render(Graphics context, Score score, ScoreEditorConfig config, ScoreEditorLook look, float scrollOffsetY, Rectangle selectionRectangle) {
            var hasAnyBar = score?.HasAnyBar ?? false;
            if (hasAnyBar) {
                RenderBars(context, score, config, look, scrollOffsetY);
            }
            var hasAnyNote = score?.HasAnyNote ?? false;
            if (hasAnyNote) {
                RenderNotes(context, score, config, look, scrollOffsetY);
            }
            RenderSelectionRectangle(context, selectionRectangle);
        }

    }
}
