using System;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.IO;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdFileNew_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.Project = new Project();
            UpdateUIIndications(DefaultDocumentName);
        }

        private void CmdFileOpen_Executed(object sender, ExecutedEventArgs e) {
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

        private void CmdFileSave_Executed(object sender, ExecutedEventArgs e) {
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
                CmdFileSaveAs.Execute(sender, e.Parameter);
            }
        }

        private void CmdFileSaveAs_Executed(object sender, ExecutedEventArgs e) {
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "StarlightDirector Project (*.sldproj)|*.sldproj";
            saveFileDialog.ShowDialog(this);
        }

        private void CmdFileExit_Executed(object sender, ExecutedEventArgs e) {
            Close();
        }

        private readonly Command CmdFileNew = CommandManager.CreateCommand("Ctrl+N");
        private readonly Command CmdFileOpen = CommandManager.CreateCommand("Ctrl+O");
        private readonly Command CmdFileSave = CommandManager.CreateCommand("Ctrl+S");
        private readonly Command CmdFileSaveAs = CommandManager.CreateCommand("F12");
        private readonly Command CmdFileExit = CommandManager.CreateCommand("Ctrl+W");

    }
}
