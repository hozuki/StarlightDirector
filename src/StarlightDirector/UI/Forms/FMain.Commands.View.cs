using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms.Input;
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
            Debug.Assert(e.Parameter != null, "e.Parameter != null");

            var primaryBeatMode = (PrimaryBeatMode)e.Parameter;

            foreach (var item in new[] { mnuViewHighlightModeFourBeats, mnuViewHighlightModeThreeBeats }) {
                var commandSource = CommandHelper.FindCommandSource(item);

                Debug.Assert(commandSource != null, nameof(commandSource) + " != null");
                Debug.Assert(commandSource.CommandParameter != null, "commandSource.CommandParameter != null");

                var bm = (PrimaryBeatMode)commandSource.CommandParameter;

                item.Checked = bm == primaryBeatMode;
            }

            visualizer.Editor.Look.PrimaryBeatMode = primaryBeatMode;
            visualizer.Editor.Invalidate();
        }

        internal readonly CommandBinding CmdViewZoomIn = CommandHelper.CreateUIBinding("Ctrl+=");
        internal readonly CommandBinding CmdViewZoomOut = CommandHelper.CreateUIBinding("Ctrl+-");
        internal readonly CommandBinding CmdViewZoomToBeat = CommandHelper.CreateUIBinding();
        internal readonly CommandBinding CmdViewHighlightModeSet = CommandHelper.CreateUIBinding();

    }
}
