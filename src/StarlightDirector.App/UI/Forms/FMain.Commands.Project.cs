using System;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.IO;
using StarlightDirector.Beatmap.IO.Sldproj;
using StarlightDirector.Commanding;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdProjectNew_Executed(object sender, ExecutedEventArgs e) {
            var project = Project.CreateWithVersion(ProjectVersion.Current);
            visualizer.Editor.Project = project;
            UpdateUIIndications();
        }

        private void CmdProjectOpen_Executed(object sender, ExecutedEventArgs e) {
            openFileDialog.CheckFileExists = true;
            openFileDialog.ReadOnlyChecked = false;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.ValidateNames = true;
            var filter = LanguageManager.TryGetString("misc.filter.sldproj") ?? "Starlight Director Project (*.sldproj)|*.sldproj";
            openFileDialog.Filter = filter;
            var r = openFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return;
            }

            ProjectReader reader;
            var projectVersion = KnownScoreFormats.CheckFormatVersion(openFileDialog.FileName);
            if (projectVersion == 0) {
                var projectVersionErrorMessage = LanguageManager.TryGetString("messages.fmain.unknown_project_version") ?? "Unable to open this project. Maybe it is corrupted or it is a project of a no longer supported version. If you created this project file using a previous version of Starlight Director, you can try to open it in v0.7.5 and save it again.";
                MessageBox.Show(this, projectVersionErrorMessage, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else {
                switch (projectVersion) {
                    case ProjectVersion.V0_2:
                        reader = new SldprojV2Reader();
                        break;
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
            var project = reader.ReadProject(openFileDialog.FileName, null);
            visualizer.Editor.Project = project;
            UpdateUIIndications();
        }

        private void CmdProjectSave_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            if (project == null) {
                throw new InvalidOperationException();
            }
            if (!project.IsModified) {
                return;
            }
            if (project.WasSaved) {
                var writer = new SldprojV4Writer();
                writer.WriteProject(project, project.SaveFileName);
                UpdateUIIndications();
            } else {
                CmdProjectSaveAs.Execute(sender, e.Parameter);
            }
        }

        private void CmdProjectSaveAs_Executed(object sender, ExecutedEventArgs e) {
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            var filter = LanguageManager.TryGetString("misc.filter.sldproj") ?? "Starlight Director Project (*.sldproj)|*.sldproj";
            saveFileDialog.Filter = filter;
            var r = saveFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return;
            }

            var project = visualizer.Editor.Project;
            var writer = new SldprojV4Writer();
            writer.WriteProject(project, saveFileDialog.FileName);
            UpdateUIIndications();
        }

        private void CmdProjectBeatmapSettings_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            if (project == null) {
                return;
            }
            var (r, bpm, offset) = FBeatmapSettings.RequestInput(this, project.Settings, visualizer.Editor.Look.PrimaryBeatMode);
            if (r == DialogResult.Cancel) {
                return;
            }
            project.Settings.BeatPerMinute = bpm;
            project.Settings.StartTimeOffset = offset;
            InformProjectModified();
            visualizer.RecalcLayout();
            visualizer.Editor.UpdateBarStartTimeText();
        }

        private void CmdProjectBeatmapStats_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            FBeatmapStats.ShowDialog(this, project);
        }

        private void CmdProjectMusicSettings_Executed(object sender, ExecutedEventArgs e) {
            var project = visualizer.Editor.Project;
            var (r, fileName) = FMusicSettings.RequestInput(this, project.MusicFileName);
            if (r == DialogResult.Cancel) {
                return;
            }
            project.MusicFileName = fileName;
            InformProjectModified();
        }

        private void CmdProjectExit_Executed(object sender, ExecutedEventArgs e) {
            Close();
        }

        private readonly Command CmdProjectNew = CommandManager.CreateCommand("Ctrl+N");
        private readonly Command CmdProjectOpen = CommandManager.CreateCommand("Ctrl+O");
        private readonly Command CmdProjectSave = CommandManager.CreateCommand("Ctrl+S");
        private readonly Command CmdProjectSaveAs = CommandManager.CreateCommand("F12");
        private readonly Command CmdProjectBeatmapSettings = CommandManager.CreateCommand();
        private readonly Command CmdProjectBeatmapStats = CommandManager.CreateCommand();
        private readonly Command CmdProjectMusicSettings = CommandManager.CreateCommand();
        private readonly Command CmdProjectExit = CommandManager.CreateCommand("Ctrl+W");

    }
}
