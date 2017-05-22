using System;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Direct2D;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Previewing {
    public sealed partial class ScorePreviewer : Direct2DScene {

        public TimeSpan Now { private get; set; }

        public Score Score {
            get => _score;
            set {
                var b = _score != value;
                _score = value;
                if (b) {
                    Prepare();
                }
            }
        }

        public void Prepare() {
            var score = Score;
            score?.UpdateNoteHitTimings();
        }

        protected override void OnRender(D2DRenderContext context) {
            var score = Score;
            if (score == null) {
                return;
            }

            RenderAvatars(context);
            DrawNotes(context, Now.TotalSeconds, score.GetAllNotes());
        }

        private Score _score;

    }
}
