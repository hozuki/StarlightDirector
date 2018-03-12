using System.Linq;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Overlay;
using OpenCGSS.Director.Modules.SldProject.Models;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Extensions;

namespace OpenCGSS.Director.Modules.SldProject.Rendering {
    public sealed partial class ScoreEditorRenderer : DisposableBase {

        public ScoreEditorRenderer([NotNull] Font controlFont) {
            CreateResources(controlFont);
        }

        [CanBeNull]
        public Score Score { private get; set; }

        [NotNull]
        public ScoreEditorLook Look => _editorLook;

        [NotNull]
        public ScoreEditorConfig EditorConfig => _editorConfig;

        public void Render([NotNull] GraphicsDevice graphicsDevice, float currentVerticalPosition, Vector2 clientSize, [CanBeNull] Rectangle? selectionRectangle = null) {
            var score = Score;

            if (score == null) {
                return;
            }

            var shouldCreateNewGraphics = _graphics == null;

            if (!shouldCreateNewGraphics && graphicsDevice != _lastGraphicsDevice)
                shouldCreateNewGraphics = true;

            if (!shouldCreateNewGraphics && !_lastClientSize.Equals(clientSize))
                shouldCreateNewGraphics = true;

            if (shouldCreateNewGraphics) {
                _graphics?.Dispose();
                _graphics = new Graphics(graphicsDevice);
            }

            var graphics = _graphics;

            using (var spriteBatch = new SpriteBatch(graphicsDevice)) {
                spriteBatch.Begin(blendState: BlendState.AlphaBlend, samplerState: SamplerState.LinearClamp, depthStencilState: DepthStencilState.Default);

                graphics.Clear(Color.Black);

                RenderInternal(graphics, score, _editorConfig, _editorLook, currentVerticalPosition, clientSize, selectionRectangle);

                graphics.UpdateBackBuffer();
                spriteBatch.Draw(graphics.BackBuffer, Vector2.Zero, Color.White);

                spriteBatch.End();
            }

            _lastGraphicsDevice = graphicsDevice;
            _lastClientSize = clientSize;
        }

        public float GetFullScoreHeight() {
            var score = Score;

            if (score == null) {
                return 1;
            }

            var look = _editorLook;
            var height = score.Bars.Sum(bar => look.BarLineSpaceUnit * bar.GetNumberOfGrids());

            return height;
        }

        private void RenderInternal([NotNull] Graphics graphics, [NotNull] Score score, [NotNull] ScoreEditorConfig config, [NotNull] ScoreEditorLook look, float scrollOffsetY, Vector2 clientSize, [CanBeNull] Rectangle? selectionRectangle) {
            var hasAnyBar = score.HasAnyBar;

            if (hasAnyBar) {
                RenderBars(graphics, score, config, look, scrollOffsetY, clientSize);
            }

            var hasAnyNote = score.HasAnyNote;

            if (hasAnyNote) {
                RenderNotes(graphics, score, config, look, scrollOffsetY, clientSize);
            }

            RenderSelectionRectangle(graphics, selectionRectangle);
        }

        protected override void Dispose(bool disposing) {
            _graphics?.Dispose();
            _graphics = null;

            DisposeResources();
        }

        private readonly ScoreEditorConfig _editorConfig = new ScoreEditorConfig();
        private readonly ScoreEditorLook _editorLook = new ScoreEditorLook();

        [CanBeNull]
        private Graphics _graphics;
        [CanBeNull]
        private GraphicsDevice _lastGraphicsDevice;
        private Vector2 _lastClientSize;

    }
}
