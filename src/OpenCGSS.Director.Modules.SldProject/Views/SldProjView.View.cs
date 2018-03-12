using System.Linq;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using OpenCGSS.Director.Modules.SldProject.Models;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Extensions;
using OpenCGSS.Director.Modules.SldProject.Rendering;

namespace OpenCGSS.Director.Modules.SldProject.Views {
    partial class SldProjView {

        public void ZoomIn() {
            var location = Mouse.GetPosition(Game);

            ZoomIn(location);
        }

        public void ZoomOut() {
            var location = Mouse.GetPosition(Game);

            ZoomOut(location);
        }

        public void ResetZoom() {
            var location = Mouse.GetPosition(Game);

            ResetZoom(location);
        }

        public void ZoomIn(System.Windows.Point mouseLocation) {
            var max = ScoreEditorLayout.SpaceUnitRadiusRatio * ScoreEditorRenderer.EditorConfig.NoteRadius;
            var min = max / ScoreEditorLayout.MaxNumberOfGrids;
            var oldUnit = ScoreEditorRenderer.Look.BarLineSpaceUnit;
            var newUnit = oldUnit * ZoomScale;

            newUnit = MathHelper.Clamp(newUnit, min, max);

            Zoom(mouseLocation, oldUnit, newUnit);
        }

        public void ZoomOut(System.Windows.Point mouseLocation) {
            var max = ScoreEditorLayout.SpaceUnitRadiusRatio * ScoreEditorRenderer.EditorConfig.NoteRadius;
            var min = max / ScoreEditorLayout.MaxNumberOfGrids;
            var oldUnit = ScoreEditorRenderer.Look.BarLineSpaceUnit;
            var newUnit = oldUnit / ZoomScale;

            newUnit = MathHelper.Clamp(newUnit, min, max);

            Zoom(mouseLocation, oldUnit, newUnit);
        }

        public void ResetZoom(System.Windows.Point mouseLocation) {
            var oldUnit = ScoreEditorRenderer.Look.BarLineSpaceUnit;
            var newUnit = ScoreEditorLook.DefaultBarLineSpaceUnit;

            Zoom(mouseLocation, oldUnit, newUnit);
        }

        private bool Zoom(System.Windows.Point mouseLocation, float oldUnit, float newUnit) {
            var score = SldProject?.GetCurrentScore();

            if (score == null) {
                return false;
            }

            if (!score.HasAnyBar) {
                return false;
            }

            if (oldUnit.Equals(newUnit)) {
                return false;
            }

            // Zooming the Editor
            // Bars are rendered as 50px lower (set in ScoreEditorConfig.FirstBarBottomY) than its literal Y offset.
            // For example, when a start Y of a bar is 0, the rendered start Y is 50px (= 0 + 50; origin: top left).
            // In order to correctly zoom the contents, the zooming process contains several steps:
            //   1. Gets the mouse position (MP1) relative to Game control;
            //   2. Transform MP1 to its "literal" position (move 50px up);
            //   3. Set the zooming transform center to "literal" mouse position;
            //   4. Enlarge/shrink the bars;
            //   5. Calculate the new start Y of the first bar;
            //   6. Reflect on CurrentVerticalPosition;

            // Grid count variables.
            var barGridCounts = score.Bars.Select(BarExtensions.GetNumberOfGrids).ToArray();
            var totalNumberOfGrids = barGridCounts.Sum();
            // Total height of the score, when using old unit length.
            var oldMaxHeight = oldUnit * totalNumberOfGrids;
            // Original scroll offset.
            var oldScrollOffset = (float)ScrollOffsetY;

            // Client size of the Game control.
            var clientSizeY = (float)Game.ActualHeight;
            // The bottom Y of the first bar (origin: top left)
            //var firstBarBottomY = clientSizeY - _scoreEditorRenderer.EditorConfig.FirstBarBottomY;
            //var transformedMouseLocationY = (float)mouseLocation.Y - firstBarBottomY;

            // Transform mouse location Y to center of Game
            var mouseYDelta = clientSizeY / 2 - (float)mouseLocation.Y;

            // Transform center of Game relative to the bottom of the first bar
            var oldRelativeY = oldScrollOffset + mouseYDelta;

            // In case of the user points his mouse out of the usual score region, we need to clamp the Y position.
            oldRelativeY = MathHelper.Clamp(oldRelativeY, 0, oldMaxHeight);

            // The bar index that the mouse is on.
            var mouseBarIndex = 0;
            var yRelativeToCurrentBar = oldRelativeY;
            // Original ratio value of:
            // (mouseYRelativeToTheBarTheMouseIsOn - bottomYOfTheBarTheMouseIsOn) / heightOfTheBarTheMouseIsOn
            var mouseInBarRatio = 0f;

            for (var i = 0; i < barGridCounts.Length; i++) {
                var barHeight = barGridCounts[i] * oldUnit;

                if (yRelativeToCurrentBar > barHeight) {
                    yRelativeToCurrentBar -= barHeight;
                    ++mouseBarIndex;
                } else {
                    mouseInBarRatio = yRelativeToCurrentBar / barHeight;
                    break;
                }
            }

            // Heights of the grids "below" the mouse (including all the bars that are before the bar the mouse is on),
            // in new unit length;
            var newRelativeY = 0f;

            for (var i = 0; i < mouseBarIndex; ++i) {
                newRelativeY += barGridCounts[i] * newUnit;
            }

            newRelativeY += barGridCounts[mouseBarIndex] * newUnit * mouseInBarRatio;

            var newScrollOffset = newRelativeY - mouseYDelta;

            if (newScrollOffset < 0) {
                newScrollOffset = 0;
            }

            var anythingChanged = !newScrollOffset.Equals(oldScrollOffset);

            if (anythingChanged) {
                ScoreEditorRenderer.Look.BarLineSpaceUnit = newUnit;

                UpdateVerticalLength();
                ScrollOffsetY = newScrollOffset;

                Redraw();
            }

            return anythingChanged;
        }

    }
}
