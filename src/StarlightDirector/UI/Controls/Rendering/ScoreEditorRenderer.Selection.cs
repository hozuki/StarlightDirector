using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    partial class ScoreEditorRenderer {

        private void RenderSelectionRectangle(Graphics context, Rectangle selectionRectangle) {
            if (selectionRectangle.IsEmpty) {
                return;
            }
            context.DrawRectangle(_selectionRectStroke, selectionRectangle);
        }

    }
}
