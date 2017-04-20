using System;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        public event EventHandler<EventArgs> EditModeChanged;

        private void OnEditModeChanged(EventArgs e) {
            EditModeChanged?.Invoke(this, e);
        }

    }
}
