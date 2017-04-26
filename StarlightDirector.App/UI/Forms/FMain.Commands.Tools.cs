using System.Windows.Forms;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Commanding;
using StarlightDirector.Core;
using System.IO;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdToolsExportCsv_Executed(object sender, ExecutedEventArgs e) {
            var score = visualizer.Editor.CurrentScore;
            if (score == null) {
                MessageBox.Show(this, "There is no score to compile and export.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.ValidateNames = true;
            saveFileDialog.Filter = "CGSS Single Beatmap (*.csv)|*.csv";
            var r = saveFileDialog.ShowDialog(this);
            if (r == DialogResult.Cancel) {
                return;
            }

            var compiledScore = score.Compile();
            var csv = compiledScore.GetCsvString();
            using (var fileStream = File.Open(saveFileDialog.FileName, FileMode.Create, FileAccess.Write)) {
                using (var writer = new StreamWriter(fileStream)) {
                    writer.Write(csv);
                }
            }
            MessageBox.Show(this, $"The score is exported to '{saveFileDialog.FileName}'.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private readonly Command CmdToolsExportCsv = CommandManager.CreateCommand();

    }
}
