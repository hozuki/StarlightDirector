using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.Input;
using OpenCGSS.StarlightDirector.Models.Beatmap;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private void CmdScoreDifficultySelect_Executed(object sender, ExecutedEventArgs e) {
            Debug.Assert(e.Parameter != null, "e.Parameter != null");

            var difficulty = (Difficulty)e.Parameter;

            if (difficulty == visualizer.Editor.Difficulty) {
                return;
            }

            foreach (ToolStripMenuItem item in mnuScoreDifficulty.DropDownItems) {
                var commandSource = CommandHelper.FindCommandSource(item);

                Debug.Assert(commandSource != null, nameof(commandSource) + " != null");
                Debug.Assert(commandSource.CommandParameter != null, "commandSource.CommandParameter != null");

                item.Checked = (Difficulty)commandSource.CommandParameter == difficulty;
            }

            visualizer.Editor.Difficulty = difficulty;
            UpdateUIIndications(difficulty);
        }


        internal readonly CommandBinding CmdScoreDifficultySelect = CommandHelper.CreateUIBinding();

    }
}
