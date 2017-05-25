using System;
using System.Linq;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;

namespace StarlightDirector.UI.Controls.Editing {
    partial class ScoreEditor {

        public void UpdateBarStartTimeText() {
            CurrentScore.UpdateAllStartTimes();
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
            estY = estY * BarLineSpaceUnit;
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

    }
}
