using System;
using System.IO;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.IO;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdFileOpen_Executed(object sender, EventArgs e) {
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
            visualizer.Renderer.Project = project;
            UpdateUIIndications(openFileDialog.SafeFileName);
        }

        private void CmdFileSave_Executed(object sender, EventArgs e) {
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "StarlightDirector Project (*.sldproj)|*.sldproj";
            saveFileDialog.ShowDialog(this);
        }

        private void CmdFileExit_Executed(object sender, EventArgs e) {
            Close();
        }

        private readonly Command CmdFileOpen = CommandManager.CreateCommand();
        private readonly Command CmdFileExit = CommandManager.CreateCommand();
        private readonly Command CmdFileSave = CommandManager.CreateCommand();

    }
}
