using System.Drawing;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Extensions;

namespace StarlightDirector.UI.Controls.Editing {
    partial class ScoreEditor {

        internal Rectangle SelectionRectangle { get; set; }

        internal void UpdateNoteSelection(RegionSelectionMode selectionMode) {
            var rect = (RectangleF)SelectionRectangle;
            var config = Config;
            var gridArea = ScoreEditorLayout.GetGridArea(config, ClientSize);
            var numColumns = config.NumberOfColumns;
            var unit = Look.BarLineSpaceUnit;
            var score = CurrentScore;
            var noteStartY = (float)ScrollOffsetY;
            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.Helper.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        if (!note.Helper.IsGaming) {
                            continue;
                        }

                        var x = ScoreEditorLayout.GetNotePositionX(note, gridArea, numColumns);
                        var y = ScoreEditorLayout.GetNotePositionY(note, unit, noteStartY);
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
