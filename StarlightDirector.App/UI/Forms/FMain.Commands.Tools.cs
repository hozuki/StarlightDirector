using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdToolsExportCsv_Executed(object sender, ExecutedEventArgs e) {
            FBuildBeatmap.ShowDialog(this, visualizer.Editor.Project, visualizer.Editor.Difficulty);
        }

        private void CmdToolsBuildBdb_Executed(object sender, ExecutedEventArgs e) {
            FBuildBeatmap.ShowDialog(this, visualizer.Editor.Project, Difficulty.Invalid);
        }

        private void CmdToolsSettings_Executed(object sender, ExecutedEventArgs e) {
            var settings = EditorSettingsManager.CurrentSettings;
            var r = FEditorSettings.ChangeSettings(this, settings);
            if (r == DialogResult.Cancel) {
                return;
            }
            // Apply settings.
            ApplySettings(settings);
        }

        private readonly Command CmdToolsExportCsv = CommandManager.CreateCommand();
        private readonly Command CmdToolsBuildBdb = CommandManager.CreateCommand();
        private readonly Command CmdToolsSettings = CommandManager.CreateCommand();

    }
}
