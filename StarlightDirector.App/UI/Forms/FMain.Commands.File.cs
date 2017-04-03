using System;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdFileOpen_Executed(object sender, EventArgs eventArgs) {
            openFileDialog.CheckFileExists = true;
            openFileDialog.ReadOnlyChecked = false;
            openFileDialog.ShowReadOnly = false;
            openFileDialog.ValidateNames = true;
            openFileDialog.Filter = "StarlightDirector Project (*.sldproj)|*.sldproj";
            openFileDialog.ShowDialog(this);
        }

        private void CmdFileSave_Executed(object sender, EventArgs eventArgs) {
            saveFileDialog.CheckFileExists = true;
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "StarlightDirector Project (*.sldproj)|*.sldproj";
            saveFileDialog.ShowDialog(this);
        }

        private void CmdFileExit_Executed(object sender, EventArgs eventArgs) {
            Close();
        }

        private readonly Command CmdFileOpen = CommandManager.CreateCommand();
        private readonly Command CmdFileExit = CommandManager.CreateCommand();
        private readonly Command CmdFileSave = CommandManager.CreateCommand();

    }
}
