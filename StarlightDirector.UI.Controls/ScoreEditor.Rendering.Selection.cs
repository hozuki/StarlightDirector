using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        private void RenderSelectionRectangle(D2DRenderContext context) {
            var rect = SelectionRectangle;
            if (rect.IsEmpty) {
                return;
            }
            context.DrawRectangle(_selectionRectStroke, rect);
        }

    }
}
