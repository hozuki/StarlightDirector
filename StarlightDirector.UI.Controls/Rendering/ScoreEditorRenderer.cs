using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Controls.Editing;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Rendering {
    internal sealed partial class ScoreEditorRenderer {

        internal void Render(D2DRenderContext context, Score score, ScoreEditorConfig config, ScoreEditorLook look, float scrollOffsetY, Rectangle selectionRectangle) {
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
