using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using OpenCGSS.Director.Modules.SldProject.Extensions;
using OpenCGSS.Director.Modules.SldProject.Models;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Extensions;
using OpenCGSS.Director.Modules.SldProject.Rendering;

namespace OpenCGSS.Director.Modules.SldProject.Views {
    partial class SldProjView {


        [CanBeNull]
        internal Rectangle? SelectionRectangle { get; set; }

        internal void UpdateNoteSelection(RegionSelectionMode selectionMode) {
            if (SelectionRectangle == null) {
                return;
            }

            var score = SldProject?.GetCurrentScore();

            if (score == null) {
                return;
            }

            var clientSize = new Vector2((float)Game.ActualWidth, (float)Game.ActualHeight);
            var rect = (Rectangle)SelectionRectangle;
            var config = ScoreEditorRenderer.EditorConfig;
            var gridArea = ScoreEditorLayout.GetGridArea(config, clientSize);
            var numColumns = config.NumberOfColumns;
            var unit = ScoreEditorRenderer.Look.BarLineSpaceUnit;
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
