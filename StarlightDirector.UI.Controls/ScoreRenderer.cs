using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering.Direct2D;
using FontStyle = StarlightDirector.UI.Rendering.FontStyle;

namespace StarlightDirector.UI.Controls {
    public sealed partial class ScoreRenderer : Direct2DCanvas {

        public Score Score { get; set; }

        public ScoreRendererConfig Config { get; } = new ScoreRendererConfig();

        protected override void OnRender(D2DRenderContext context) {
            var score = Score;
            var hasAnyBar = score?.HasAnyBar ?? false;
            if (hasAnyBar) {
                RenderBars(context);
            }
            var hasAnyNote = score?.HasAnyNote ?? false;
            if (hasAnyNote) {
                RenderNotes(context);
            }
        }

        protected override void OnCreateResources(D2DRenderContext context) {
            _pen = new D2DPen(Color.Red, 4, context);
            _scoreBarFont = new D2DFont(context.DirectWriteFactory, DefaultFont.Name, 10, FontStyle.None, 10);
        }

        protected override void OnDisposeResources(D2DRenderContext context) {
            _pen?.Dispose();
            _scoreBarFont?.Dispose();
        }

    }
}
