using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Overlay;
using MonoGame.Extended.WinForms.WindowsDX;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;
using OpenCGSS.StarlightDirector.UI.Controls.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls.Rendering;

namespace OpenCGSS.StarlightDirector.UI.Controls.Previewing {
    public sealed class ScorePreviewer : D3DGameControl {

        public ScorePreviewer() {
            _graphicsBatch = new GraphicsBatch();
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
                        PreviewerRenderer.CreateResources(ClientRectangle.ToXna());
                        break;
                    case PreviewerRenderMode.EditorLike:
                        PreviewerRenderer.DisposeResources();
                        EditorRenderer.CreateResources(_baseFont);
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

        protected override void OnDraw(GameTime gameTime) {
            var score = Score;
            var now = Now;
            var graphicsDevice = GraphicsDevice;

            if (score != null && graphicsDevice != null) {
                graphicsDevice.Clear(Color.Transparent);

                _graphicsBatch.Update(graphicsDevice);

                var spriteBatch = _graphicsBatch.SpriteBatch;
                var graphics = _graphicsBatch.Graphics;

                if (spriteBatch != null && graphics != null) {
                    spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default);

                    graphics.Clear(Color.Transparent);

                    switch (RenderMode) {
                        case PreviewerRenderMode.Standard:
                            PreviewerRenderer.Render(graphics, score, now);
                            break;
                        case PreviewerRenderMode.EditorLike:
                            var offsetY = BarPositionHelper.GetScrollOffsetY(score, _scoreTimingInfo, Now, _editorLook, ClientSize, _editorFullHeight);
                            EditorRenderer.Render(graphics, score, _editorConfig, _editorLook, offsetY, now);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (graphics.UpdateBackBuffer()) {
                        spriteBatch.Draw(graphics.BackBuffer, Vector2.Zero, Color.White);
                    }

                    spriteBatch.End();
                }

                FrameRendered?.Invoke(this, EventArgs.Empty);
            }

            base.OnDraw(gameTime);
        }

        protected override void OnLoadContents() {
            base.OnLoadContents();

            _fontManager = new FontManager();

            var controlFont = Font;
            var baseFont = _fontManager.CreateFont(controlFont.OriginalFontName, FontStyle.Regular);

            baseFont.Size = controlFont.SizeInPoints;
            _baseFont = baseFont;

            switch (RenderMode) {
                case PreviewerRenderMode.Standard:
                    PreviewerRenderer.CreateResources(ClientRectangle.ToXna());
                    break;
                case PreviewerRenderMode.EditorLike:
                    EditorRenderer.CreateResources(baseFont);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnUnloadContents() {
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

            _fontManager?.Dispose();

            base.OnUnloadContents();
        }

        protected override void Dispose(bool disposing) {
            _graphicsBatch?.Dispose();

            base.Dispose(disposing);
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

        private Font _baseFont;

        private readonly GraphicsBatch _graphicsBatch;

        private FontManager _fontManager;

    }
}
