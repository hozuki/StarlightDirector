using System;
using System.Drawing;

namespace StarlightDirector.UI.Controls {
    public sealed class ContextMenuRequestedEventArgs : EventArgs {

        public ContextMenuRequestedEventArgs(VisualizerContextMenu menuType, Point location) {
            MenuType = menuType;
            Location = location;
        }

        public VisualizerContextMenu MenuType { get; }

        public Point Location { get; }

        public int X => Location.X;

        public int Y => Location.Y;

    }
}
