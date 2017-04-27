using System;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.IO;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdProjectNew_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.Project = new Project();
            UpdateUIIndications(DefaultDocumentName);
        }

        private void CmdProjectOpen_Executed(object sender, ExecutedEventArgs e) {
            openFileDialog.CheckFileExists = true;
            openFileDialog.ReadOnlyChecked = false;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.ValidateNames = true;
            openFileDialog.Filter = "StarlightDirector Project (*.sldproj)|*.sldproj";
            var r = openFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return;
            }
            if (!File.Exists(openFileDialog.FileName)) {
                return;
            }

            var reader = new SldprojV3Reader();
            var project = reader.ReadProject(openFileDialog.FileName);
            visualizer.Editor.Project = project;
            UpdateUIIndications(openFileDialog.SafeFileName);
        }

        private void CmdProjectSave_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            if (project == null) {
                throw new InvalidOperationException();
            }
            if (!project.IsChanged) {
                return;
            }
            if (project.WasSaved) {
                // TODO: Silent save.
            } else {
                CmdProjectSaveAs.Execute(sender, e.Parameter);
            }
        }

        private void CmdProjectSaveAs_Executed(object sender, ExecutedEventArgs e) {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "StarlightDirector Project (*.sldproj)|*.sldproj";
            saveFileDialog.ShowDialog(this);
        }

        private void CmdProjectBeatmapSettings_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            if (project == null) {
                return;
            }
            var (r, bpm, offset) = FBeatmapSettings.RequestInput(this, project.Settings);
            if (r == DialogResult.Cancel) {
                return;
            }
            project.Settings.BeatPerMinute = bpm;
            project.Settings.StartTimeOffset = offset;
            visualizer.RecalcLayout();
            visualizer.Editor.Invalidate();
        }

        private void CmdProjectExit_Executed(object sender, ExecutedEventArgs e) {
            Close();
        }

        private readonly Command CmdProjectNew = CommandManager.CreateCommand("Ctrl+N");
        private readonly Command CmdProjectOpen = CommandManager.CreateCommand("Ctrl+O");
        private readonly Command CmdProjectSave = CommandManager.CreateCommand("Ctrl+S");
        private readonly Command CmdProjectSaveAs = CommandManager.CreateCommand("F12");
        private readonly Command CmdProjectBeatmapSettings = CommandManager.CreateCommand();
        private readonly Command CmdProjectExit = CommandManager.CreateCommand("Ctrl+W");

    }
}
