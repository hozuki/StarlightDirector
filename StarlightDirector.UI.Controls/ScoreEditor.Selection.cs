using System.Drawing;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Extensions;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        internal Rectangle SelectionRectangle { get; set; }

        internal void UpdateNoteSelection(RegionSelectionMode selectionMode) {
            var rect = (RectangleF)SelectionRectangle;
            var gridArea = GetGridArea();
            var config = Config;
            var numColumns = config.NumberOfColumns;
            var unit = BarLineSpaceUnit;
            var score = CurrentScore;
            var noteStartY = (float)ScrollOffsetY;
            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.Helper.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        var x = GetNotePositionX(note, gridArea, numColumns);
                        var y = GetNotePositionY(note, unit, noteStartY);
                        var selectionHit = rect.ContainsAdjusted(x, y);

                        if (selectionHit) {
                            if (selectionMode != RegionSelectionMode.Subtraction) {
                                note.EditorSelect();
                            } else {
                                note.EditorUnselect();
                            }
                        } else {
                            if (selectionMode == RegionSelectionMode.Normal) {
                                note.EditorUnselect();
                            }
                        }
                    }
                }
                noteStartY -= numberOfGrids * unit;
            }
        }

    }
}
