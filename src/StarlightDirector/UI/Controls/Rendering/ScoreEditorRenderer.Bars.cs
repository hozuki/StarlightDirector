using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    partial class ScoreEditorRenderer {

        private void RenderBars(Graphics context, Score score, ScoreEditorConfig config, ScoreEditorLook look, float barStartY) {
            var bars = score.Bars;
            var primaryBeatMode = look.PrimaryBeatMode;
            var gridArea = ScoreEditorLayout.GetGridArea(config, context.Bounds);
            var barArea = ScoreEditorLayout.GetBarArea(config, context.Bounds);
            var infoArea = ScoreEditorLayout.GetInfoArea(config, context.Bounds);

            var unit = look.BarLineSpaceUnit;
            var noteRadius = config.NoteRadius;
            foreach (var bar in bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                var visible = ScoreEditorLayout.IsBarVisible(barArea, barStartY, numberOfGrids, unit);
                if (visible) {
                    if (look.BarInfoTextVisible) {
                        DrawBarInfoText(context, bar, infoArea, barStartY);
                    }
                    DrawBarGrid(context, gridArea, config, barStartY, numberOfGrids, unit, noteRadius, primaryBeatMode);
                    DrawBarOutline(context, bar, barArea, barStartY, numberOfGrids, unit);
                }
                barStartY -= numberOfGrids * unit;
            }
        }

        private void DrawBarOutline(Graphics context, Bar bar, Rectangle barArea, float barStartY, int numberOfGrids, float unit) {
            if (!bar.Editor.IsSelected) {
                return;
            }
            var barHeight = numberOfGrids * unit;
            var rect = RectangleHelper.Build(barArea.Left, barStartY, barArea.Width, -barHeight);
            context.DrawRectangle(_barSelectedOutlinePen, rect);
        }

        private void DrawBarInfoText(Graphics context, Bar bar, Rectangle infoArea, float barStartY) {
            var textFont = _barInfoFont;
            var lineHeight = textFont.Size * 1.2f;
            var x = infoArea.Left + 2;
            var y = barStartY - lineHeight;
            var textBrush = _gridNumberBrush;

            // Other text
            // Bar start time
            var infoText = bar.Temporary.StartTime.ToString(@"mm\:ss\.fff");
            var infoTextSize = context.MeasureString(textFont, infoText);
            context.FillString(textBrush, textFont, infoText, x, y + infoTextSize.Y);
            y -= lineHeight;
            // Bar index, at the bottom.
            infoText = "#" + (bar.Basic.Index + 1).ToString();
            infoTextSize = context.MeasureString(textFont, infoText);
            context.FillString(textBrush, textFont, infoText, x, y + infoTextSize.Y);
            // y -- lineHeight;
        }

        private void DrawBarGrid(Graphics context, Rectangle gridArea, ScoreEditorConfig config, float barStartY, int numberOfGrids, float unit, float noteRadius, PrimaryBeatMode primaryBeatMode) {
            // Vertical
            var verticalY1 = barStartY;
            var verticalY2 = barStartY - numberOfGrids * unit;
            var verticalPen = _barNormalGridPen;
            var numColumns = config.NumberOfColumns;
            for (var i = 0; i < numColumns; ++i) {
                var x = gridArea.Left + gridArea.Width * i / (numColumns - 1);
                context.DrawLine(verticalPen, x, verticalY1, x, verticalY2);
            }

            // Calculate zooming compensation.
            var firstClearDrawnRatio = ScoreEditorLayout.BarZoomRatio.FirstOrDefault(i => unit * i >= noteRadius * ScoreEditorLayout.SpaceUnitRadiusRatio);
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
            var textFont = _gridNumberFont;
            int visibleGridsPerBeat;
            // For the usage, see below.
            if (primaryBeatIndex % firstClearDrawnRatio == 0) {
                visibleGridsPerBeat = primaryBeatIndex / firstClearDrawnRatio;
            } else {
                // This patch does not actually 'patch' very well. But it is still better than splitting
                // the non-separatable 3-beat measures into some weird 5-beat(=32/6) rendering.
                visibleGridsPerBeat = primaryBeatIndex * numBeats / 4 / firstClearDrawnRatio;
            }
            if (visibleGridsPerBeat <= 0) {
                visibleGridsPerBeat = 1;
            }
            var visibleGridCounter = 1;
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
                    // Prints like
                    // 1/4 2/4 3/4 4/4 1/4 2/4 ...
                    var text = $"{visibleGridCounter}/{visibleGridsPerBeat}";
                    var textSize = context.MeasureString(_gridNumberFont, text);
                    var textLeft = gridArea.Left - textSize.X - ScoreEditorLayout.GridNumberMargin;
                    var textTop = currentY + textSize.Y / 2;
                    context.FillString(textBrush, textFont, text, textLeft, textTop);
                    if (visibleGridCounter >= visibleGridsPerBeat) {
                        visibleGridCounter = 1;
                    } else {
                        ++visibleGridCounter;
                    }
                }
            }
        }

        private void DrawBarTimePreviewInfo(Graphics context, Rectangle controlArea, TimeSpan now) {
            var middle = controlArea.Height / 2;
            context.DrawLine(_timeLineStroke, controlArea.Left, middle, controlArea.Right, middle);

            var timeText = now.ToString("g");
            var textSize = context.MeasureString(_timeInfoFont, timeText);
            var timeBottom = middle - textSize.Y / 2;
            context.FillString(_timeInfoBrush, _timeInfoFont, timeText, controlArea.Left, timeBottom);
        }

    }
}
