using System.Drawing;
using StarlightDirector.Commanding;

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

        private readonly Command CmdViewZoomIn = CommandManager.CreateCommand();
        private readonly Command CmdViewZoomOut = CommandManager.CreateCommand();

    }
}
