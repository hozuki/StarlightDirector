using System;
using System.Diagnostics;
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
                return ScoreEditorHitTestResult.GetInvalidResult(x, y);
            }

            var barArea = GetBarArea();
            if (!barArea.Contains(x, y)) {
                return ScoreEditorHitTestResult.GetInvalidResult(x, y);
            }

            var config = Config;
            ScoreEditorHitRegion hitRegion;
            var relativeX = x - barArea.Left;
            if (relativeX < config.InfoAreaWidth) {
                hitRegion = ScoreEditorHitRegion.InfoArea;
            } else if (relativeX < config.InfoAreaWidth + config.GridNumberAreaWidth - config.NoteRadius) {
                hitRegion = ScoreEditorHitRegion.GridNumberArea;
            } else if (relativeX < config.InfoAreaWidth + config.GridNumberAreaWidth + config.GridAreaWidth + config.NoteRadius) {
                hitRegion = ScoreEditorHitRegion.GridArea;
            } else {
                hitRegion = ScoreEditorHitRegion.SpecialNoteArea;
            }

            var gridArea = GetGridArea();
            var columnWidth = gridArea.Width / (5 - 1);

            var barStartY = (float)ScrollOffsetY;
            if (y > barStartY + config.NoteRadius) {
                return ScoreEditorHitTestResult.GetInvalidResult(x, y);
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
                var firstClearDrawnRatio = BarZoomRatio.FirstOrDefault(i => unit * i >= config.NoteRadius * 2);
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
                    return new ScoreEditorHitTestResult(new Point(x, y), hitRegion, bar, null, -1, NotePosition.Default);
                }
                row *= firstClearDrawnRatio;

                // Locate the column.
                // Remember, gridArea is already adjusted.
                var relativeGridX = x - gridArea.Left;
                var testCol = (int)((relativeGridX + config.NoteRadius) / columnWidth);
                if (testCol < 0) {
                    return new ScoreEditorHitTestResult(new Point(x, y), hitRegion, bar, null, -1, NotePosition.Default);
                }
                var testX = testCol * columnWidth;
                int col;
                if (Math.Abs(relativeGridX - testX) < config.NoteRadius) {
                    col = testCol;
                } else if (Math.Abs(relativeGridX - (testX + columnWidth)) < config.NoteRadius) {
                    col = testCol + 1;
                } else {
                    if (hitRegion == ScoreEditorHitRegion.SpecialNoteArea) {
                        col = 0;
                    } else {
                        return new ScoreEditorHitTestResult(new Point(x, y), hitRegion, bar, null, -1, NotePosition.Default);
                    }
                }

                // Hit any gaming note?
                var note = bar.Notes.FirstOrDefault(n => n.Basic.IndexInGrid == row && (int)n.Basic.FinishPosition == col + 1);

                // Hit any special note?
                if (note == null && hitRegion == ScoreEditorHitRegion.SpecialNoteArea) {
                    note = bar.Notes.FirstOrDefault(n => n.Helper.IsSpecial && n.Basic.IndexInGrid == row);
                }

                var result = new ScoreEditorHitTestResult(new Point(x, y), hitRegion, bar, note, row, col + 1);
                return result;
            }

            return new ScoreEditorHitTestResult(new Point(x, y), hitRegion, null, null, -1, NotePosition.Default);
        }
    }

}
