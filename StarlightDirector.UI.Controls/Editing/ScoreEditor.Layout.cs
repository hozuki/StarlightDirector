using System;
using StarlightDirector.Beatmap.Extensions;

namespace StarlightDirector.UI.Controls.Editing {
    partial class ScoreEditor {

        public void UpdateBarStartTimeText() {
            CurrentScore.UpdateAllStartTimes();
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
