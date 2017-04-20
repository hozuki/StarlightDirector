using System;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;
using StarlightDirector.UI.Controls;

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

        private void CmdEditModeSelect_Executed(object sender, ExecutedEventArgs e) {
            var menuItem = (ToolStripMenuItem)sender;
            var parentItem = menuItem.OwnerItem;
            ToolStripItemCollection menuItemCollection;
            switch (parentItem) {
                case ToolStripMenuItem m:
                    menuItemCollection = m.DropDownItems;
                    break;
                case ToolStripDropDownButton b:
                    menuItemCollection = b.DropDownItems;
                    break;
                default:
                    throw new NotSupportedException();
            }
            foreach (ToolStripItem item in menuItemCollection) {
                if (item is ToolStripMenuItem tsmi) {
                    tsmi.Checked = tsmi == menuItem;
                }
            }

            var mode = (ScoreEditMode)e.Parameter;
            visualizer.Editor.EditMode = mode;
            tsbEditMode.Image = menuItem.Image;
        }

        private void CmdEditBeatmapSettings_Executed(object sender, ExecutedEventArgs e) {
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

        private void CmdEditSelectAllMeasures_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.SelectAllBars();
            visualizer.Editor.Invalidate();
        }

        private void CmdEditSelectAllNotes_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.SelectAllNotes();
            visualizer.Editor.Invalidate();
        }

        private readonly Command CmdEditDifficultySelect = CommandManager.CreateCommand();
        private readonly Command CmdEditModeSelect = CommandManager.CreateCommand();
        private readonly Command CmdEditBeatmapSettings = CommandManager.CreateCommand();
        private readonly Command CmdEditSelectAllMeasures = CommandManager.CreateCommand("Ctrl+Shift+A");
        private readonly Command CmdEditSelectAllNotes = CommandManager.CreateCommand("Ctrl+A");

    }
}
