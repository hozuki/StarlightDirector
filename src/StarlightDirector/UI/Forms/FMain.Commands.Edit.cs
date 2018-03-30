using System;
using System.Linq;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.Input;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private void CmdEditUndo_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdEditUndo_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private void CmdEditRedo_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdEditRedo_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private void CmdEditCut_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdEditCut_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private void CmdEditCopy_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdEditCopy_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private void CmdEditPaste_Executed(object sender, ExecutedEventArgs e) {
            // TODO
        }

        private void CmdEditPaste_QueryCanExecute(object sender, QueryCanExecuteEventArgs e) {
            // TODO
            e.CanExecute = false;
        }

        private void CmdEditModeSet_Executed(object sender, ExecutedEventArgs e) {
            var modeMenuItems = new[] { mnuEditModeSelect, mnuEditModeTap, mnuEditModeHoldFlick, mnuEditModeSlide };
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

        private void CmdEditModeHoldFlick_Executed(object sender, ExecutedEventArgs e) {
            CmdEditModeSet.Execute(sender, e.Parameter);
        }

        private void CmdEditModeSlide_Executed(object sender, ExecutedEventArgs e) {
            CmdEditModeSet.Execute(sender, e.Parameter);
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

        private void CmdEditGoToMeasure_Executed(object sender, ExecutedEventArgs e) {
            var score = visualizer.Editor.CurrentScore;
            if (!score.HasAnyBar) {
                return;
            }

            var (r, t) = FGoTo.RequestInput(this, 0, visualizer.Editor);
            if (r == DialogResult.Cancel) {
                return;
            }

            ExecuteCmdEditGoToMenuItems(score, (r, t));
        }

        private void CmdEditGoToTime_Executed(object sender, ExecutedEventArgs e) {
            var score = visualizer.Editor.CurrentScore;
            if (!score.HasAnyBar) {
                return;
            }

            var (r, t) = FGoTo.RequestInput(this, 1, visualizer.Editor);
            if (r == DialogResult.Cancel) {
                return;
            }

            ExecuteCmdEditGoToMenuItems(score, (r, t));
        }

        private void ExecuteCmdEditGoToMenuItems(Score score, (DialogResult, object) value) {
            var (_, t) = value;

            if (t is int measureIndex) {
                measureIndex -= 1;
                visualizer.Editor.ScrollToBar(measureIndex);
            } else if (t is TimeSpan time) {
                var bar = score.Bars[0];
                visualizer.Editor.UpdateBarStartTimes();
                foreach (var b in score.Bars.Skip(1)) {
                    if (b.Temporary.StartTime > time) {
                        break;
                    }
                    bar = b;
                }

                visualizer.Editor.ScrollToBar(bar);
            }
        }

        internal readonly Command CmdEditUndo = CommandManager.CreateCommand("Ctrl+Z");
        internal readonly Command CmdEditRedo = CommandManager.CreateCommand("Ctrl+Y");
        internal readonly Command CmdEditCut = CommandManager.CreateCommand("Ctrl+X");
        internal readonly Command CmdEditCopy = CommandManager.CreateCommand("Ctrl+C");
        internal readonly Command CmdEditPaste = CommandManager.CreateCommand("Ctrl+V");
        internal readonly Command CmdEditModeSet = CommandManager.CreateCommand();
        internal readonly Command CmdEditModeSelect = CommandManager.CreateCommand();
        internal readonly Command CmdEditModeTap = CommandManager.CreateCommand();
        internal readonly Command CmdEditModeHoldFlick = CommandManager.CreateCommand();
        internal readonly Command CmdEditModeSlide = CommandManager.CreateCommand();
        internal readonly Command CmdEditSelectAllMeasures = CommandManager.CreateCommand("Ctrl+Shift+A");
        internal readonly Command CmdEditSelectAllNotes = CommandManager.CreateCommand("Ctrl+A");
        internal readonly Command CmdEditSelectClearAll = CommandManager.CreateCommand();
        internal readonly Command CmdEditGoToMeasure = CommandManager.CreateCommand("Ctrl+G");
        internal readonly Command CmdEditGoToTime = CommandManager.CreateCommand("Ctrl+Shift+G");

    }
}
