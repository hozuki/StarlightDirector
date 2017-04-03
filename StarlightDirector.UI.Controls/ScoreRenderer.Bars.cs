using System.Linq;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls {
    partial class ScoreRenderer {

        private void RenderBars(RenderContext context, Score score) {
            var config = Config;
            var bars = score.Bars;
            var primaryBeatMode = PrimaryBeat;
            var clientSize = context.ClientSize;
            var barStartY = clientSize.Height - ScrollOffsetY;

            foreach (var bar in bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                var firstClearDrawnRatio = BarZoomRatio.FirstOrDefault(i => config.BarLineSpaceUnit * i >= config.NoteRadius * 2);
                if (firstClearDrawnRatio == 0) {
                    firstClearDrawnRatio = 96;
                }
                var primaryBeatIndex = numberOfGrids / primaryBeatMode;
                var secondaryBeatIndex = primaryBeatIndex / 2;
                for (var i = 0; i < numberOfGrids; ++i) {
                    if (i % firstClearDrawnRatio != 0) {
                        continue;
                    }

                    var currentY = barStartY - config.BarLineSpaceUnit * i;
                    Pen pen;
                    if (i == 0) {
                        // Grid start.
                        pen = _barGridStartBeatPen;
                    } else if (i % primaryBeatIndex == 0) {
                        // Primary beat.
                        pen = _barPrimaryBeatPen;
                    } else if (i % secondaryBeatIndex == 0) {
                        // Secondary beat.
                        pen = _barSecondaryBeatPen;
                    } else {
                        // Normal grid.
                        pen = _barNormalGridPen;
                    }
                    context.DrawLine(pen, 0, currentY, clientSize.Width, currentY);
                }

                barStartY -= numberOfGrids * config.BarLineSpaceUnit;
            }
        }

        private static readonly int[] BarZoomRatio = { 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 96 };

        private D2DPen _barGridOutlinePen;
        private D2DPen _barGridStartBeatPen;
        private D2DPen _barPrimaryBeatPen;
        private D2DPen _barSecondaryBeatPen;
        private D2DPen _barNormalGridPen;
        private D2DFont _scoreBarFont;

    }
}
