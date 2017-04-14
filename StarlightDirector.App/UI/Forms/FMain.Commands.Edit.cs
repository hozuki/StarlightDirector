using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdEditDifficultySelect_Executed(object sender, ExecutedEventArgs e) {
            var menuItem = (ToolStripMenuItem)sender;
            var difficulty = (Difficulty)e.Parameter;
            if (difficulty == visualizer.Editor.Difficulty) {
                return;
            }
            foreach (ToolStripMenuItem item in mnuEditDifficulty.DropDownItems) {
                item.Checked = false;
            }
            menuItem.Checked = true;
            visualizer.Editor.Difficulty = difficulty;
            UpdateUIIndications(difficulty);
        }

        private void CmdEditNoteStartPosition_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Editor.HasSelectedNotes) {
                return;
            }
            var startPosition = (NotePosition)e.Parameter;
            foreach (var note in visualizer.Editor.GetSelectedNotes()) {
                note.Basic.StartPosition = startPosition;
            }
            visualizer.RedrawScore();
        }

        private void CmdEditSelectAllMeasures_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.SelectAllBars();
            visualizer.RedrawScore();
        }

        private void CmdEditSelectAllNotes_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.SelectAllNotes();
            visualizer.RedrawScore();
        }

        private void CmdEditMeasureAppend_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.AppendBar();
            visualizer.RecalcLayout();

            var score = visualizer.Editor.CurrentScore;
            if (score != null) {
                var estY = (float)score.Bars.Take(score.Bars.Count - 1).Sum(b => b.GetNumberOfGrids());
                estY = estY * visualizer.Editor.BarLineSpaceUnit;
                estY += visualizer.ScrollBar.Minimum;
                visualizer.ScrollBar.Value = (int)estY;
            }

            visualizer.RedrawScore();
        }

        private void CmdEditMeasureDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedBars();
            visualizer.RecalcLayout();
            visualizer.RedrawScore();
        }

        private void CmdEditNoteDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedNotes();
            visualizer.RedrawScore();
        }

        private readonly Command CmdEditDifficultySelect = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPosition = CommandManager.CreateCommand();
        private readonly Command CmdEditSelectAllMeasures = CommandManager.CreateCommand();
        private readonly Command CmdEditSelectAllNotes = CommandManager.CreateCommand();
        private readonly Command CmdEditMeasureAppend = CommandManager.CreateCommand();
        private readonly Command CmdEditMeasureDelete = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteDelete = CommandManager.CreateCommand();

    }
}
