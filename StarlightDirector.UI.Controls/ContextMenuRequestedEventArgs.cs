using System;
using System.Drawing;

namespace StarlightDirector.UI.Controls {
    public sealed class ContextMenuRequestedEventArgs : EventArgs {

        public ContextMenuRequestedEventArgs(VisualizerContextMenu menuType, ScoreEditorHitTestResult hit) {
            MenuType = menuType;
            HitTestResult = hit;
            Location = hit.Location;
        }

        public VisualizerContextMenu MenuType { get; }

        public Point Location { get; }

        public int X => Location.X;

        public int Y => Location.Y;

        public ScoreEditorHitTestResult HitTestResult { get; }

    }
}
