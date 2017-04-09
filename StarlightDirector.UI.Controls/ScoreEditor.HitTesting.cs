using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        public ScoreEditorHitTestResult HitTest(Point location, Control relativeTo) {
            location = relativeTo.PointToScreen(location);
            location = PointToClient(location);
            return HitTest(location);
        }

        public ScoreEditorHitTestResult HitTest(int x, int y, Control relativeTo) {
            return HitTest(new Point(x, y), relativeTo);
        }

        public ScoreEditorHitTestResult HitTest(Point location) {
            return HitTest(location.X, location.Y);
        }

        /// <summary>
        /// Performs a hit test and returns the result.
        /// </summary>
        /// <param name="x">The X coordinate of the hit test point, relative to this control.</param>
        /// <param name="y">The Y coordinate of the hit test point, relative to this control.</param>
        /// <returns>The result of this hit test.</returns>
        public ScoreEditorHitTestResult HitTest(int x, int y) {
            var score = Project?.GetScore(Difficulty);
            if (score == null || !score.HasAnyBar) {
                return ScoreEditorHitTestResult.Invalid;
            }

            var config = Config;
            var barArea = GetBarArea();
            var gridArea = GetGridArea();
            var columnWidth = gridArea.Width / (5 - 1);
            var hitArea = barArea;
            if (!hitArea.Contains(x, y)) {
                return ScoreEditorHitTestResult.Invalid;
            }

            var barStartY = (float)ScrollOffsetY;
            if (y > barStartY + config.NoteRadius) {
                return ScoreEditorHitTestResult.Invalid;
            }

            var unit = BarLineSpaceUnit;
            foreach (var bar in score.Bars) {
                var numGrids = bar.GetNumberOfGrids();
                var barHeight = numGrids * unit;

                var hitInThisBar = barStartY + config.NoteRadius >= y && y > barStartY - (barHeight - config.NoteRadius);
                if (!hitInThisBar) {
                    // Continue to the next bar.
                    barStartY -= barHeight;
                    continue;
                }

                // Calculate zooming compensation.
                var firstClearDrawnRatio = BarZoomRatio.FirstOrDefault(i => BarLineSpaceUnit * i >= config.NoteRadius * 2);
                if (firstClearDrawnRatio == 0) {
                    firstClearDrawnRatio = numGrids;
                }
                var newUnit = unit * firstClearDrawnRatio;

                // Locate the row.
                var relativeY = -(y - barStartY);
                var testRow = (int)((relativeY + config.NoteRadius) / newUnit);
                if (testRow < 0) {
                    break;
                }
                var testY = testRow * newUnit;
                int row;
                if (Math.Abs(relativeY - testY) < config.NoteRadius) {
                    row = testRow;
                } else if (Math.Abs(relativeY - (testY + newUnit)) < config.NoteRadius) {
                    row = testRow + 1;
                } else {
                    return new ScoreEditorHitTestResult(bar, null, -1, NotePosition.Nowhere);
                }
                row *= firstClearDrawnRatio;

                // Locate the column.
                // Remember, gridArea is already adjusted.
                var relativeX = x - gridArea.Left;
                var testCol = (int)((relativeX + config.NoteRadius) / columnWidth);
                if (testCol < 0) {
                    break;
                }
                var testX = testCol * columnWidth;
                int col;
                if (Math.Abs(relativeX - testX) < config.NoteRadius) {
                    col = testCol;
                } else if (Math.Abs(relativeX - (testX + columnWidth)) < config.NoteRadius) {
                    col = testCol + 1;
                } else {
                    return new ScoreEditorHitTestResult(bar, null, -1, NotePosition.Nowhere);
                }

                // Hit any note?
                var note = bar.Notes.FirstOrDefault(n => n.Basic.IndexInGrid == row && (int)n.Basic.FinishPosition == col + 1);

                var result = new ScoreEditorHitTestResult(bar, note, row, col + 1);
                return result;
            }

            return ScoreEditorHitTestResult.Invalid;
        }
    }

}
