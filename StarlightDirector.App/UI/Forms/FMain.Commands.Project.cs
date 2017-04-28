using System;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.IO;
using StarlightDirector.Commanding;
using StarlightDirector.Core;

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

            ProjectReader reader;
            var projectVersion = KnownScoreFormats.CheckFormatVersion(openFileDialog.FileName);
            if (projectVersion == 0) {
                MessageBox.Show(this, "Unable to detect the version of this project. Maybe it is corrupted or it is an older version of project. If you created this project file using a previous version of Starlight Director, you can try to open it in v0.7.5 and save it again.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else {
                switch (projectVersion) {
                    case ProjectVersion.V0_3:
                    case ProjectVersion.V0_3_1:
                        reader = new SldprojV3Reader();
                        break;
                    case ProjectVersion.V0_4:
                        reader = new SldprojV4Reader();
                        break;
                    default:
                        MessageBox.Show(this, "You should not have seen this message.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }
            }
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
                var writer = new SldprojV4Writer();
                writer.WriteProject(project, project.SaveFileName);
            } else {
                CmdProjectSaveAs.Execute(sender, e.Parameter);
            }
        }

        private void CmdProjectSaveAs_Executed(object sender, ExecutedEventArgs e) {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "StarlightDirector Project (*.sldproj)|*.sldproj";
            var r = saveFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return;
            }

            var project = visualizer.Editor.Project;
            var writer = new SldprojV4Writer();
            writer.WriteProject(project, saveFileDialog.FileName);
            var fileInfo = new FileInfo(saveFileDialog.FileName);
            UpdateUIIndications(fileInfo.Name);
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

        private void CmdProjectBeatmapInfo_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            FBeatmapInfo.ShowDialog(this, project);
        }

        private void CmdProjectExit_Executed(object sender, ExecutedEventArgs e) {
            Close();
        }

        private readonly Command CmdProjectNew = CommandManager.CreateCommand("Ctrl+N");
        private readonly Command CmdProjectOpen = CommandManager.CreateCommand("Ctrl+O");
        private readonly Command CmdProjectSave = CommandManager.CreateCommand("Ctrl+S");
        private readonly Command CmdProjectSaveAs = CommandManager.CreateCommand("F12");
        private readonly Command CmdProjectBeatmapSettings = CommandManager.CreateCommand();
        private readonly Command CmdProjectBeatmapInfo = CommandManager.CreateCommand();
        private readonly Command CmdProjectExit = CommandManager.CreateCommand("Ctrl+W");

    }
}
