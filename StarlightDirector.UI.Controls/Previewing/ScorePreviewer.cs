using System;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Direct2D;
using StarlightDirector.UI.Controls.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Previewing {
    public sealed class ScorePreviewer : Direct2DScene {

        public ScorePreviewer() {
            _previewerRenderer = new ScorePreviewerRenderer();
        }

        public event EventHandler<EventArgs> FrameRendered;

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
            if (score == null) {
                return;
            }
            score.UpdateNoteHitTimings();
            foreach (var note in score.GetAllNotes()) {
                note.Temporary.EditorVisible = false;
            }
        }

        protected override void OnRender(D2DRenderContext context) {
            _previewerRenderer.Render(context, Score, Now);
            FrameRendered?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnCreateResources(D2DRenderContext context) {
            _previewerRenderer.CreateResources(context);
        }

        protected override void OnDisposeResources(D2DRenderContext context) {
            _previewerRenderer.DisposeResources(context);
        }

        private Score _score;
        private readonly ScorePreviewerRenderer _previewerRenderer;

    }
}
