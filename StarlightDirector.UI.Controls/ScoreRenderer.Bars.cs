using StarlightDirector.UI.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls {
    partial class ScoreRenderer {

        private void RenderBars(RenderContext context) {
            var config = Config;
            var bars = Score.Bars;
        }

        private D2DPen _pen;
        private D2DFont _scoreBarFont;

    }
}
