using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering;

namespace StarlightDirector.UI.Controls {
    partial class ScoreRenderer {

        private void RenderNotes(RenderContext context, Score score) {
            var config = Config;
            var notes = score.GetAllNotes();
        }

    }
}
