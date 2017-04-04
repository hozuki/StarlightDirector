using System;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdEditDifficultySelect_Executed(object sender, EventArgs e) {
            var menuItem = (ToolStripMenuItem)sender;
            var difficulty = (Difficulty)menuItem.Tag;
            if (difficulty == visualizer.Renderer.Difficulty) {
                return;
            }
            foreach (ToolStripMenuItem item in mnuEditDifficulty.DropDownItems) {
                item.Checked = false;
            }
            menuItem.Checked = true;
            visualizer.Renderer.Difficulty = difficulty;
            UpdateUIIndications(difficulty);
        }

        private readonly Command CmdEditDifficultySelect = CommandManager.CreateCommand();

    }
}
