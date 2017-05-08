using System.Drawing;
using StarlightDirector.Commanding;
using StarlightDirector.UI.Controls;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdViewZoomIn_Executed(object sender, ExecutedEventArgs e) {
            var clientSize = visualizer.Editor.ClientSize;
            var clientCenter = new Point(clientSize.Width / 2, clientSize.Height / 2);
            visualizer.Editor.ZoomIn(clientCenter);
        }

        private void CmdViewZoomOut_Executed(object sender, ExecutedEventArgs e) {
            var clientSize = visualizer.Editor.ClientSize;
            var clientCenter = new Point(clientSize.Width / 2, clientSize.Height / 2);
            visualizer.Editor.ZoomOut(clientCenter);
        }

        private void CmdViewHighlightModeSet_Executed(object sender, ExecutedEventArgs e) {
            var primaryBeatMode = (PrimaryBeatMode)e.Parameter;

            foreach (var item in new[] { mnuViewHighlightModeFourBeats, mnuViewHighlightModeThreeBeats }) {
                var bm = (PrimaryBeatMode)item.GetParameter();
                item.Checked = bm == primaryBeatMode;
            }

            visualizer.Editor.PrimaryBeatMode = primaryBeatMode;
            visualizer.Editor.Invalidate();
        }

        private readonly Command CmdViewZoomIn = CommandManager.CreateCommand("Ctrl+=");
        private readonly Command CmdViewZoomOut = CommandManager.CreateCommand("Ctrl+-");
        private readonly Command CmdViewHighlightModeSet = CommandManager.CreateCommand();

    }
}
