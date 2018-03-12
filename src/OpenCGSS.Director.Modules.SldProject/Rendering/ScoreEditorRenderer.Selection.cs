using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;

namespace OpenCGSS.Director.Modules.SldProject.Rendering {
    partial class ScoreEditorRenderer {

        private void RenderSelectionRectangle([NotNull] Graphics graphics, [CanBeNull] Rectangle? selectionRectangle) {
            if (selectionRectangle == null) {
                return;
            }

            if (selectionRectangle.Value.IsEmpty) {
                return;
            }

            graphics.DrawRectangle(_selectionRectStroke, selectionRectangle.Value);
        }

    }
}
