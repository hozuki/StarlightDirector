using System;
using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.Models.Beatmap;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    internal sealed partial class ScorePreviewerRenderer {

        public void Render(Graphics context, Score score, TimeSpan now) {
            if (score == null) {
                return;
            }

            var nowSeconds = now.TotalSeconds;
            RenderAvatars(context);
            DrawNotes(context, nowSeconds, score.GetAllNotes());
        }

    }
}
