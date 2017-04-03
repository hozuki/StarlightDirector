using StarlightDirector.UI.Rendering;

namespace StarlightDirector.UI.Controls {
    partial class ScoreRenderer {

        private void RenderNotes(RenderContext context) {
            var config = Config;
            var notes = Score.GetAllNotes();
        }

    }
}
