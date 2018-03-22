using System;
using System.Linq;
using Microsoft.Xna.Framework;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Controls.Editing {
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

        public void ZoomIn(System.Drawing.Point mouseLocation) {
            var max = ScoreEditorLayout.SpaceUnitRadiusRatio * Config.NoteRadius;
            var min = max / MaxNumberOfGrids;
            var oldUnit = Look.BarLineSpaceUnit;
            var newUnit = oldUnit * ZoomScale;
            newUnit = MathHelper.Clamp(newUnit, min, max);
            Zoom(mouseLocation, oldUnit, newUnit);
        }

        public void ZoomOut(System.Drawing.Point mouseLocation) {
            var max = ScoreEditorLayout.SpaceUnitRadiusRatio * Config.NoteRadius;
            var min = max / MaxNumberOfGrids;
            var oldUnit = Look.BarLineSpaceUnit;
            var newUnit = oldUnit / ZoomScale;
            newUnit = MathHelper.Clamp(newUnit, min, max);
            Zoom(mouseLocation, oldUnit, newUnit);
        }

        public void ResetZoom(System.Drawing.Point mouseLocation) {
            var oldUnit = Look.BarLineSpaceUnit;
            var newUnit = ScoreEditorLook.DefaultBarLineSpaceUnit;
            Zoom(mouseLocation, oldUnit, newUnit);
        }

        public void UpdateBarStartTimes() {
            CurrentScore.UpdateAllStartTimes();
        }

        public void UpdateBarStartTimeText() {
            UpdateBarStartTimes();
            Invalidate();
        }

        public void ScrollToBar(int index) {
            var score = CurrentScore;
            if (score == null) {
                return;
            }
            if (index < 0 || score.Bars.Count - 1 < index) {
                return;
            }
            var bar = score.Bars[index];
            ScrollToBar(bar);
        }

        public void ScrollToBar(Bar bar) {
            var score = CurrentScore;
            if (score == null || !score.Bars.Contains(bar)) {
                return;
            }

            var estY = (float)score.Bars.Take(bar.Basic.Index).Sum(b => b.GetNumberOfGrids());
            estY = estY * Look.BarLineSpaceUnit;
            estY += ScrollBar.Minimum;
            ScrollBar.Value = (int)estY;
        }

        internal void RecalcLayout() {
            var scrollBar = ScrollBar;
            if (scrollBar != null) {
                var clientSize = ClientSize;
                var expectedHeight = GetFullHeight();
                scrollBar.Minimum = clientSize.Height / 2;
                scrollBar.Maximum = clientSize.Height / 2 + (int)Math.Round(expectedHeight);
            }
        }

        private bool Zoom(System.Drawing.Point mouseLocation, float oldUnit, float newUnit) {
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
            Look.BarLineSpaceUnit = newUnit;
            RecalcLayout();
            newScrollOffset = MathHelper.Clamp(newScrollOffset, ScrollBar.Minimum, ScrollBar.Maximum);
            ScrollBar.Value = newScrollOffset;

            return anythingChanged;
        }

        private static readonly float ZoomScale = 1.2f;

    }
}
