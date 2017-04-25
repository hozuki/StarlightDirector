using System;
using System.Linq;
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

        private void CmdEditModeSet_Executed(object sender, ExecutedEventArgs e) {
            var modeMenuItems = new[] { mnuEditModeSelect, mnuEditModeTap, mnuEditModeHold, mnuEditModeFlick, mnuEditModeSlide };
            var mode = (ScoreEditMode)e.Parameter;
            var pressedItem = modeMenuItems.First(m => (ScoreEditMode)m.GetParameter() == mode);
            foreach (var item in modeMenuItems) {
                item.Checked = item == pressedItem;
            }
            visualizer.Editor.EditMode = mode;
            tsbEditMode.Image = pressedItem.Image;
        }

        private void CmdEditModeSelect_Executed(object sender, ExecutedEventArgs e) {
            CmdEditModeSet.Execute(sender, e.Parameter);
        }

        private void CmdEditModeTap_Executed(object sender, ExecutedEventArgs e) {
            CmdEditModeSet.Execute(sender, e.Parameter);
        }

        private void CmdEditModeHold_Executed(object sender, ExecutedEventArgs e) {
            CmdEditModeSet.Execute(sender, e.Parameter);
        }

        private void CmdEditModeFlick_Executed(object sender, ExecutedEventArgs e) {
            CmdEditModeSet.Execute(sender, e.Parameter);
        }

        private void CmdEditModeSlide_Executed(object sender, ExecutedEventArgs e) {
            CmdEditModeSet.Execute(sender, e.Parameter);
        }

        private void CmdEditModePrevious_Executed(object sender, ExecutedEventArgs e) {
            var currentEditMode = visualizer.Editor.EditMode;
            if (currentEditMode <= ScoreEditMode.Min) {
                return;
            }
            var newEditMode = currentEditMode - 1;
            CmdEditModeSet.Execute(sender, newEditMode);
        }

        private void CmdEditModeNext_Executed(object sender, ExecutedEventArgs e) {
            var currentEditMode = visualizer.Editor.EditMode;
            if (currentEditMode >= ScoreEditMode.Max) {
                return;
            }
            var newEditMode = currentEditMode + 1;
            CmdEditModeSet.Execute(sender, newEditMode);
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

        private void CmdEditSelectClearAll_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.ClearSelectedBars();
            visualizer.Editor.ClearSelectedNotes();
            visualizer.Editor.Invalidate();
        }

        private readonly Command CmdEditDifficultySelect = CommandManager.CreateCommand();
        private readonly Command CmdEditModeSet = CommandManager.CreateCommand();
        private readonly Command CmdEditModeSelect = CommandManager.CreateCommand();
        private readonly Command CmdEditModeTap = CommandManager.CreateCommand();
        private readonly Command CmdEditModeHold = CommandManager.CreateCommand();
        private readonly Command CmdEditModeFlick = CommandManager.CreateCommand();
        private readonly Command CmdEditModeSlide = CommandManager.CreateCommand();
        private readonly Command CmdEditModePrevious = CommandManager.CreateCommand("Alt+A");
        private readonly Command CmdEditModeNext = CommandManager.CreateCommand("Alt+D");
        private readonly Command CmdEditBeatmapSettings = CommandManager.CreateCommand();
        private readonly Command CmdEditSelectAllMeasures = CommandManager.CreateCommand("Ctrl+Shift+A");
        private readonly Command CmdEditSelectAllNotes = CommandManager.CreateCommand("Ctrl+A");
        private readonly Command CmdEditSelectClearAll = CommandManager.CreateCommand();

    }
}
