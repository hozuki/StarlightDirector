using OpenCGSS.StarlightDirector.Input;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private void CmdHelpAbout_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            var projectName = project.SaveFilePath ?? string.Empty;
            projectName = projectName.ToLowerInvariant();
            var easterEggEnabled = projectName.Contains("sakuma") && projectName.Contains("mayu");
            FAbout.ShowDialog(this, easterEggEnabled);
        }

        internal readonly Command CmdHelpAbout = CommandManager.CreateCommand();

    }
}
