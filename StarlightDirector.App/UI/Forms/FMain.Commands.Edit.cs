using System.Linq;
using StarlightDirector.Commanding;
using StarlightDirector.UI.Controls;

namespace StarlightDirector.App.UI.Forms {
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
            var newEditMode = currentEditMode > ScoreEditMode.Min ? currentEditMode - 1 : ScoreEditMode.Max;
            CmdEditModeSet.Execute(sender, newEditMode);
        }

        private void CmdEditModeNext_Executed(object sender, ExecutedEventArgs e) {
            var currentEditMode = visualizer.Editor.EditMode;
            var newEditMode = currentEditMode < ScoreEditMode.Max ? currentEditMode + 1 : ScoreEditMode.Min;
            CmdEditModeSet.Execute(sender, newEditMode);
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

        private readonly Command CmdEditUndo = CommandManager.CreateCommand("Ctrl+Z");
        private readonly Command CmdEditRedo = CommandManager.CreateCommand("Ctrl+Y");
        private readonly Command CmdEditCut = CommandManager.CreateCommand("Ctrl+X");
        private readonly Command CmdEditCopy = CommandManager.CreateCommand("Ctrl+C");
        private readonly Command CmdEditPaste = CommandManager.CreateCommand("Ctrl+V");
        private readonly Command CmdEditModeSet = CommandManager.CreateCommand();
        private readonly Command CmdEditModeSelect = CommandManager.CreateCommand();
        private readonly Command CmdEditModeTap = CommandManager.CreateCommand();
        private readonly Command CmdEditModeHold = CommandManager.CreateCommand();
        private readonly Command CmdEditModeFlick = CommandManager.CreateCommand();
        private readonly Command CmdEditModeSlide = CommandManager.CreateCommand();
        private readonly Command CmdEditModePrevious = CommandManager.CreateCommand("Alt+A");
        private readonly Command CmdEditModeNext = CommandManager.CreateCommand("Alt+D");
        private readonly Command CmdEditSelectAllMeasures = CommandManager.CreateCommand("Ctrl+Shift+A");
        private readonly Command CmdEditSelectAllNotes = CommandManager.CreateCommand("Ctrl+A");
        private readonly Command CmdEditSelectClearAll = CommandManager.CreateCommand();

    }
}
