using System;
using System.Collections.Generic;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Direct2D;
using StarlightDirector.UI.Controls.Editing;
using StarlightDirector.UI.Controls.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Previewing {
    public sealed class ScorePreviewer : Direct2DScene {

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

        public PreviewerRenderMode RenderMode {
            get => _renderdeMode;
            set {
                var b = value != _renderdeMode;
                if (!b) {
                    return;
                }
                _renderdeMode = value;
                switch (value) {
                    case PreviewerRenderMode.Standard:
                        EditorRenderer.DisposeResources();
                        if (_lastRenderContext != null) {
                            PreviewerRenderer.CreateResources(_lastRenderContext);
                        }
                        break;
                    case PreviewerRenderMode.EditorLike:
                        PreviewerRenderer.DisposeResources();
                        if (_lastRenderContext != null) {
                            EditorRenderer.CreateResources(_lastRenderContext, Font);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

        public void Prepare() {
            var score = Score;
            if (score == null) {
                return;
            }
            _scoreTimingInfo = score.UpdateNoteHitTimings();
            foreach (var note in score.GetAllNotes()) {
                note.Temporary.EditorVisible = false;
            }
        }

        internal void SimulateEditor(ScoreEditor editor) {
            _editorConfig = editor.Config.Clone();
            var look = _editorLook = editor.Look.Clone();
            look.BarInfoTextVisible = false;
            look.TimeInfoVisible = true;
            _editorFullHeight = editor.GetFullHeight();
        }

        protected override void OnRender(D2DRenderContext context) {
            var score = Score;
            var now = Now;
            switch (RenderMode) {
                case PreviewerRenderMode.Standard:
                    PreviewerRenderer.Render(context, score, now);
                    break;
                case PreviewerRenderMode.EditorLike:
                    var offsetY = BarPositionHelper.GetScrollOffsetY(score, _scoreTimingInfo, Now, _editorLook, ClientSize, _editorFullHeight);
                    EditorRenderer.Render(context, score, _editorConfig, _editorLook, offsetY, now);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            FrameRendered?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnCreateResources(D2DRenderContext context) {
            _lastRenderContext = context;
            switch (RenderMode) {
                case PreviewerRenderMode.Standard:
                    PreviewerRenderer.CreateResources(context);
                    break;
                case PreviewerRenderMode.EditorLike:
                    EditorRenderer.CreateResources(context, Font);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnDisposeResources(D2DRenderContext context) {
            switch (RenderMode) {
                case PreviewerRenderMode.Standard:
                    PreviewerRenderer.DisposeResources();
                    break;
                case PreviewerRenderMode.EditorLike:
                    EditorRenderer.DisposeResources();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ScorePreviewerRenderer PreviewerRenderer => _previewerRenderer ?? (_previewerRenderer = new ScorePreviewerRenderer());

        private ScoreEditorRenderer EditorRenderer => _editorRenderer ?? (_editorRenderer = new ScoreEditorRenderer());

        private Score _score;
        private IReadOnlyList<IReadOnlyList<TimeSpan>> _scoreTimingInfo;
        private PreviewerRenderMode _renderdeMode;
        private ScorePreviewerRenderer _previewerRenderer;
        private ScoreEditorRenderer _editorRenderer;
        private ScoreEditorConfig _editorConfig;
        private ScoreEditorLook _editorLook;
        private float _editorFullHeight;
        // Warning: possible ObjectDisposedException
        private D2DRenderContext _lastRenderContext;

    }
}
