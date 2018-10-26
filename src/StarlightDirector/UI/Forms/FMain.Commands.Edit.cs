using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Input;
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
            Debug.Assert(e.Parameter != null, "e.Parameter != null");

            var modeMenuItems = new[] { mnuEditModeSelect, mnuEditModeTap, mnuEditModeHoldFlick, mnuEditModeSlide };
            var mode = (ScoreEditMode)e.Parameter;
            var pressedItem = modeMenuItems.First(m => {
                var commandSource = CommandHelper.FindCommandSource(m);

                Debug.Assert(commandSource != null, nameof(commandSource) + " != null");
                Debug.Assert(commandSource.CommandParameter != null);

                return (ScoreEditMode)commandSource.CommandParameter == mode;
            });

            foreach (var item in modeMenuItems) {
                item.Checked = item == pressedItem;
            }

            visualizer.Editor.EditMode = mode;
            tsbEditMode.Image = pressedItem.Image;
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

            if (score == null) {
                return;
            }

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

        internal readonly CommandBinding CmdEditUndo = CommandHelper.CreateUIBinding("Ctrl+Z");
        internal readonly CommandBinding CmdEditRedo = CommandHelper.CreateUIBinding("Ctrl+Y");
        internal readonly CommandBinding CmdEditCut = CommandHelper.CreateUIBinding("Ctrl+X");
        internal readonly CommandBinding CmdEditCopy = CommandHelper.CreateUIBinding("Ctrl+C");
        internal readonly CommandBinding CmdEditPaste = CommandHelper.CreateUIBinding("Ctrl+V");
        internal readonly CommandBinding CmdEditModeSet = CommandHelper.CreateUIBinding();
        internal readonly CommandBinding CmdEditSelectAllMeasures = CommandHelper.CreateUIBinding("Ctrl+Shift+A");
        internal readonly CommandBinding CmdEditSelectAllNotes = CommandHelper.CreateUIBinding("Ctrl+A");
        internal readonly CommandBinding CmdEditSelectClearAll = CommandHelper.CreateUIBinding();
        internal readonly CommandBinding CmdEditGoToMeasure = CommandHelper.CreateUIBinding("Ctrl+G");
        internal readonly CommandBinding CmdEditGoToTime = CommandHelper.CreateUIBinding("Ctrl+Shift+G");

    }
}
