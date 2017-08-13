using System;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Rendering {
    internal sealed partial class ScorePreviewerRenderer {

        internal void Render(D2DRenderContext context, Score score, TimeSpan now) {
            if (score == null) {
                return;
            }

            var nowSeconds = now.TotalSeconds;
            RenderAvatars(context);
            DrawNotes(context, nowSeconds, score.GetAllNotes());
        }

    }
}
