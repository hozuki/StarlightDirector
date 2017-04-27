using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdToolsExportCsv_Executed(object sender, ExecutedEventArgs e) {
            FBuildBeatmap.ShowDialog(this, visualizer.Editor.Project, visualizer.Editor.Difficulty);
        }

        private readonly Command CmdToolsExportCsv = CommandManager.CreateCommand();

    }
}
