using System;
using System.Drawing;
using System.Linq;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.UI.Controls.Editing {
    partial class ScoreEditor {

        public void ZoomIn() {
            var location = MousePosition;
            location = PointToClient(location);
            ZoomIn(location);
        }

        public void ZoomOut() {
            var location = MousePosition;
            location = PointToClient(location);
            ZoomOut(location);
        }

        public void ResetZoom() {
            var location = MousePosition;
            location = PointToClient(location);
            ResetZoom(location);
        }

        public void ZoomIn(Point mouseLocation) {
            var max = SpaceUnitRadiusRatio * Config.NoteRadius;
            var min = max / MaxNumberOfGrids;
            var oldUnit = BarLineSpaceUnit;
            var newUnit = oldUnit * ZoomScale;
            newUnit = newUnit.Clamp(min, max);
            Zoom(mouseLocation, oldUnit, newUnit);
        }

        public void ZoomOut(Point mouseLocation) {
            var max = SpaceUnitRadiusRatio * Config.NoteRadius;
            var min = max / MaxNumberOfGrids;
            var oldUnit = BarLineSpaceUnit;
            var newUnit = oldUnit / ZoomScale;
            newUnit = newUnit.Clamp(min, max);
            Zoom(mouseLocation, oldUnit, newUnit);
        }

        public void ResetZoom(Point mouseLocation) {
            var oldUnit = BarLineSpaceUnit;
            var newUnit = DefaultBarLineSpaceUnit;
            Zoom(mouseLocation, oldUnit, newUnit);
        }

        private bool Zoom(Point mouseLocation, float oldUnit, float newUnit) {
            var score = CurrentScore;
            if (score == null) {
                return false;
            }
            if (!score.HasAnyBar) {
                return false;
            }

            var oldScrollOffset = ScrollOffsetY;
            var totalNumberOfGrids = score.Bars.Sum(bar => bar.GetNumberOfGrids());
            var oldHeight = oldUnit * totalNumberOfGrids;

            var relativeY = (float)(oldScrollOffset - mouseLocation.Y);
            if (relativeY < 0) {
                relativeY = 0;
            }
            if (relativeY > oldHeight) {
                relativeY = oldHeight;
            }

            var barIndex = 0;
            var y2 = relativeY;
            var oldRatio = 0f;
            foreach (var bar in score.Bars) {
                var barHeight = bar.GetNumberOfGrids() * oldUnit;
                if (y2 > barHeight) {
                    y2 -= barHeight;
                    ++barIndex;
                } else {
                    oldRatio = y2 / barHeight;
                    break;
                }
            }

            var clientSize = ClientSize;
            var newHeight = newUnit * totalNumberOfGrids;

            var prevBarHeight = 0f;
            for (var i = 0; i < barIndex; ++i) {
                prevBarHeight += score.Bars[i].GetNumberOfGrids() * newUnit;
            }
            prevBarHeight += score.Bars[barIndex].GetNumberOfGrids() * newUnit * oldRatio;
            var newScrollOffset = (int)(prevBarHeight + (mouseLocation.Y - clientSize.Height / 2) + (float)clientSize.Height / 2);

            var anythingChanged = newScrollOffset != oldScrollOffset || !newHeight.Equals(oldHeight);
            BarLineSpaceUnit = newUnit;
            RecalcLayout();
            newScrollOffset = newScrollOffset.Clamp(ScrollBar.Minimum, ScrollBar.Maximum);
            ScrollBar.Value = newScrollOffset;

            return anythingChanged;
        }

        private static readonly float ZoomScale = 1.2f;

    }
}
