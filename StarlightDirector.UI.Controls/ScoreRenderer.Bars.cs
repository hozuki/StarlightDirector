using System;
using System.Drawing;
using System.Linq;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;
using Pen = StarlightDirector.UI.Rendering.Pen;

namespace StarlightDirector.UI.Controls {
    partial class ScoreRenderer {

        private void RenderBars(RenderContext context, Score score) {
            var config = Config;
            var bars = score.Bars;
            var primaryBeatMode = PrimaryBeatMode;
            var clientSize = context.ClientSize;
            var barArea = new RectangleF((clientSize.Width - config.EditingAreaWidth) / 2 + (config.LeftAreaWidth + config.GridNumberAreaWidth), 0, config.BarAreaWidth, clientSize.Height);
            var barStartY = (float)ScrollOffsetY;

            foreach (var bar in bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                RenderBarGrid(context, barArea, barStartY, numberOfGrids, config, primaryBeatMode);
                barStartY -= numberOfGrids * BarLineSpaceUnit;
            }
        }

        private void RenderBarGrid(RenderContext context, RectangleF barArea, float barStartY, int numberOfGrids, ScoreRendererConfig config, PrimaryBeatMode primaryBeatMode) {
            // Vertical
            var verticalY1 = barStartY;
            var verticalY2 = barStartY - numberOfGrids * BarLineSpaceUnit;
            var verticalPen = _barNormalGridPen;
            for (var i = 0; i < 5; ++i) {
                var x = barArea.Left + barArea.Width * i / 4;
                context.DrawLine(verticalPen, x, verticalY1, x, verticalY2);
            }

            // Calculate zooming compensation.
            var firstClearDrawnRatio = BarZoomRatio.FirstOrDefault(i => BarLineSpaceUnit * i >= config.NoteRadius * 2);
            if (firstClearDrawnRatio == 0) {
                firstClearDrawnRatio = numberOfGrids;
            }

            // Calculate primary beat grids.
            int numBeats;
            switch (primaryBeatMode) {
                case PrimaryBeatMode.EveryFourBeats:
                    numBeats = 4;
                    break;
                case PrimaryBeatMode.EveryThreeBeats:
                    numBeats = 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(primaryBeatMode), primaryBeatMode, null);
            }
            var primaryBeatIndex = numberOfGrids / numBeats;
            var secondaryBeatIndex = primaryBeatIndex / 2;

            var textBrush = _gridNumberBrush;
            var textFont = _scoreBarFont;
            // Horizontal
            for (var i = 0; i <= numberOfGrids; ++i) {
                if (i % firstClearDrawnRatio != 0) {
                    continue;
                }

                var currentY = barStartY - BarLineSpaceUnit * i;
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
                context.DrawLine(pen, barArea.Left, currentY, barArea.Right, currentY);

                if (i < numberOfGrids) {
                    var text = (i + 1).ToString();
                    var textSize = context.MeasureText(text, _scoreBarFont);
                    var textLeft = barArea.Left - textSize.Width - GridNumberMargin;
                    var textTop = currentY - textSize.Height / 2;
                    context.DrawText(text, textBrush, textFont, textLeft, textTop, textSize.Width, textSize.Height);
                }
            }
        }

        private static readonly int[] BarZoomRatio = { 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 96 };

        private static readonly float GridNumberMargin = 22;

        private D2DPen _barGridOutlinePen;
        private D2DPen _barGridStartBeatPen;
        private D2DPen _barPrimaryBeatPen;
        private D2DPen _barSecondaryBeatPen;
        private D2DPen _barNormalGridPen;

        private D2DBrush _gridNumberBrush;

        private D2DFont _scoreBarFont;
        private D2DFont _scoreBarBoldFont;

    }
}
