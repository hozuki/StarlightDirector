using System;
using System.Drawing;
using OpenCGSS.StarlightDirector.Input;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Forms {
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

        private void CmdViewZoomToBeat_Executed(object sender, ExecutedEventArgs e) {
            throw new NotImplementedException();
        }

        private void CmdViewZoomToBeat_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            e.CanExecute = false;
        }

        private void CmdViewHighlightModeSet_Executed(object sender, ExecutedEventArgs e) {
            var primaryBeatMode = (PrimaryBeatMode)e.Parameter;

            foreach (var item in new[] { mnuViewHighlightModeFourBeats, mnuViewHighlightModeThreeBeats }) {
                var bm = (PrimaryBeatMode)item.GetParameter();
                item.Checked = bm == primaryBeatMode;
            }

            visualizer.Editor.Look.PrimaryBeatMode = primaryBeatMode;
            visualizer.Editor.Invalidate();
        }

        internal readonly Command CmdViewZoomIn = CommandManager.CreateCommand("Ctrl+=");
        internal readonly Command CmdViewZoomOut = CommandManager.CreateCommand("Ctrl+-");
        internal readonly Command CmdViewZoomToBeat = CommandManager.CreateCommand();
        internal readonly Command CmdViewHighlightModeSet = CommandManager.CreateCommand();

    }
}
