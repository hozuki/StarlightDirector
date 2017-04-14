using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Rendering;
using Pen = StarlightDirector.UI.Rendering.Pen;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        private void RenderBars(RenderContext context, Score score) {
            var config = Config;
            var bars = score.Bars;
            var primaryBeatMode = PrimaryBeatMode;
            var gridArea = GetGridArea(context.ClientSize);
            var barArea = GetBarArea(context.ClientSize);
            var barStartY = (float)ScrollOffsetY;

            var unit = BarLineSpaceUnit;
            var noteRadius = config.NoteRadius;
            foreach (var bar in bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                var visible = IsBarVisible(barArea, barStartY, numberOfGrids, unit);
                if (visible) {
                    RenderBarOutline(context, bar, barArea, barStartY, numberOfGrids, unit);
                    RenderBarGrid(context, gridArea, barStartY, numberOfGrids, unit, noteRadius, primaryBeatMode);
                }
                barStartY -= numberOfGrids * BarLineSpaceUnit;
            }
        }

        private void RenderBarOutline(RenderContext context, Bar bar, RectangleF barArea, float barStartY, int numberOfGrids, float unit) {
            if (!bar.IsSelected) {
                return;
            }
            var barHeight = numberOfGrids * unit;
            var rect = new RectangleF(barArea.Left, barStartY, barArea.Width, -barHeight);
            context.DrawRectangle(_barSelectedOutlinePen, rect);
        }

        private void RenderBarGrid(RenderContext context, RectangleF gridArea, float barStartY, int numberOfGrids, float unit, float noteRadius, PrimaryBeatMode primaryBeatMode) {
            // Vertical
            var verticalY1 = barStartY;
            var verticalY2 = barStartY - numberOfGrids * unit;
            var verticalPen = _barNormalGridPen;
            for (var i = 0; i < 5; ++i) {
                var x = gridArea.Left + gridArea.Width * i / (5 - 1);
                context.DrawLine(verticalPen, x, verticalY1, x, verticalY2);
            }

            // Calculate zooming compensation.
            var firstClearDrawnRatio = BarZoomRatio.FirstOrDefault(i => unit * i >= noteRadius * 2);
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

                var currentY = barStartY - unit * i;
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
                context.DrawLine(pen, gridArea.Left, currentY, gridArea.Right, currentY);

                if (i < numberOfGrids) {
                    var text = (i + 1).ToString();
                    var textSize = context.MeasureText(text, _scoreBarFont);
                    var textLeft = gridArea.Left - textSize.Width - GridNumberMargin;
                    var textTop = currentY - textSize.Height / 2;
                    context.DrawText(text, textBrush, textFont, textLeft, textTop, textSize.Width, textSize.Height);
                }
            }
        }

        private bool IsBarVisible(RectangleF barArea, float barStartY, float numberOfGrids, float unit) {
            var barHeight = numberOfGrids * unit;
            var visible = 0 <= barStartY && barStartY - barHeight <= barArea.Bottom;
            return visible;
        }

        [DebuggerStepThrough]
        private RectangleF GetGridArea() {
            return GetGridArea(ClientSize);
        }

        [DebuggerStepThrough]
        private RectangleF GetGridArea(SizeF clientSize) {
            var config = Config;
            var area = new RectangleF((clientSize.Width - config.EditingAreaWidth) / 2 + (config.LeftAreaWidth + config.GridNumberAreaWidth), 0, config.GridAreaWidth, clientSize.Height);
            return area;
        }

        [DebuggerStepThrough]
        private RectangleF GetGridArea(Size clientSize) {
            var config = Config;
            var area = new RectangleF((clientSize.Width - config.EditingAreaWidth) / 2 + (config.LeftAreaWidth + config.GridNumberAreaWidth), 0, config.GridAreaWidth, clientSize.Height);
            return area;
        }

        [DebuggerStepThrough]
        private RectangleF GetBarArea() {
            return GetBarArea(ClientSize);
        }

        [DebuggerStepThrough]
        private RectangleF GetBarArea(SizeF clientSize) {
            var config = Config;
            var area = new RectangleF((clientSize.Width - config.EditingAreaWidth) / 2, 0, config.EditingAreaWidth, clientSize.Height);
            return area;
        }

        [DebuggerStepThrough]
        private RectangleF GetBarArea(Size clientSize) {
            var config = Config;
            var area = new RectangleF((clientSize.Width - config.EditingAreaWidth) / 2, 0, config.EditingAreaWidth, clientSize.Height);
            return area;
        }

        private static readonly int[] BarZoomRatio = { 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 96 };

        private static readonly float GridNumberMargin = 22;

    }
}
