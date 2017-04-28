using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdHelpAbout_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            var projectName = project.SaveFileName ?? string.Empty;
            projectName = projectName.ToLowerInvariant();
            var easterEggEnabled = projectName.Contains("sakuma") && projectName.Contains("mayu");
            FAbout.ShowDialog(this, easterEggEnabled);
        }

        private readonly Command CmdHelpAbout = CommandManager.CreateCommand();

    }
}
