using System;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

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
