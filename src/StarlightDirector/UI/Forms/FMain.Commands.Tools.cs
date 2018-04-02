using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.DirectorApplication;
using OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs.Models;
using OpenCGSS.StarlightDirector.Input;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Editor;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private void CmdToolsExportCsv_Executed(object sender, ExecutedEventArgs e) {
            FBuildBeatmap.ShowDialog(this, visualizer.Editor.Project, visualizer.Editor.Difficulty);
        }

        private void CmdToolsBuildBdb_Executed(object sender, ExecutedEventArgs e) {
            FBuildBeatmap.ShowDialog(this, visualizer.Editor.Project, Difficulty.Invalid);
        }

        private void CmdToolsBuildAcb_Executed(object sender, ExecutedEventArgs e) {
            throw new NotImplementedException();
        }

        private void CmdToolsBuildAcb_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            e.CanExecute = false;
        }

        private void CmdToolsSettings_Executed(object sender, ExecutedEventArgs e) {
            var settings = DirectorSettingsManager.CurrentSettings;
            var r = FEditorSettings.ChangeSettings(this, settings);
            if (r == DialogResult.Cancel) {
                return;
            }
            // Apply settings.
            ApplySettings(settings);
        }

        private void CmdToolsTestReload_Executed(object sender, ExecutedEventArgs e) {
            if (_communication == null) {
                return;
            }

            var tempFileName = Path.GetTempFileName();
            var fileInfo = new FileInfo(tempFileName);
            var newFileName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);

            newFileName = "sldproj_" + newFileName + ".sldproj";

            Debug.Assert(fileInfo.DirectoryName != null, "fileInfo.DirectoryName != null");

            tempFileName = Path.Combine(fileInfo.DirectoryName, newFileName);

            var project = visualizer.Editor.Project.Project;

            var writer = ProjectWriter.CreateLatest();

            writer.WriteProject(project, tempFileName);

            var @params = new EditReloadRequestParams {
                BackgroundMusicFile = project.MusicFileName,
                BeatmapFile = tempFileName,
                BeatmapOffset = 0,
                BeatmapIndex = (int)(visualizer.Editor.Difficulty - 1)
            };

            _communication.Client.SendReloadRequest(@params);
        }

        private void CmdToolsTestLaunch_Executed(object sender, ExecutedEventArgs e) {
            if (_communication == null) {
                return;
            }

            var settings = DirectorSettingsManager.CurrentSettings;

            var endPoint = _communication.Server.EndPoint;
            var program = settings.ExternalPreviewerFile;
            var argsTemplate = settings.ExternalPreviwerArgs;
            var args = argsTemplate
                .Replace("%port%", endPoint.Port.ToString())
                .Replace("%server_uri%", $"http://{endPoint}/");

            var programFileInfo = new FileInfo(program);

            var startInfo = new ProcessStartInfo(program, args);

            Debug.Assert(programFileInfo.DirectoryName != null, "programFileInfo.DirectoryName != null");

            startInfo.WorkingDirectory = programFileInfo.DirectoryName;

            var process = Process.Start(startInfo);
        }

        internal readonly Command CmdToolsExportCsv = CommandManager.CreateCommand();
        internal readonly Command CmdToolsBuildBdb = CommandManager.CreateCommand();
        internal readonly Command CmdToolsBuildAcb = CommandManager.CreateCommand();
        internal readonly Command CmdToolsSettings = CommandManager.CreateCommand();

        internal readonly Command CmdToolsTestReload = CommandManager.CreateCommand();
        internal readonly Command CmdToolsTestLaunch = CommandManager.CreateCommand();

    }
}
