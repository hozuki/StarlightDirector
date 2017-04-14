using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdHelpAbout_Executed(object sender, ExecutedEventArgs e) {
            using (var f = new FAbout()) {
                f.ShowDialog(this);
            }
        }

        private readonly Command CmdHelpAbout = CommandManager.CreateCommand();

    }
}
