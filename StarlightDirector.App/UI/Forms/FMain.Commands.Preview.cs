using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdPreviewFromThisMeasure_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdPreviewFromThisMeasure_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private void CmdPreviewFromStart_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdPreviewFromStart_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private void CmdPreviewStop_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdPreviewStop_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private readonly Command CmdPreviewFromThisMeasure = CommandManager.CreateCommand("F5");
        private readonly Command CmdPreviewFromStart = CommandManager.CreateCommand("Ctrl+F5");
        private readonly Command CmdPreviewStop = CommandManager.CreateCommand("F6");

    }
}
