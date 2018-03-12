using System.Drawing;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Rendering {
    partial class ScoreEditorRenderer {

        private void RenderSelectionRectangle(D2DRenderContext context, Rectangle selectionRectangle) {
            if (selectionRectangle.IsEmpty) {
                return;
            }
            context.DrawRectangle(_selectionRectStroke, selectionRectangle);
        }

    }
}
