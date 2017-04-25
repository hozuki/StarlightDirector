using System;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdEditNoteStartPositionSet_Executed(object sender, ExecutedEventArgs e) {
            var startPosition = (NotePosition)e.Parameter;
            var hasSelectedNotes = visualizer.Editor.HasSelectedNotes;

            if (hasSelectedNotes) {
                foreach (var note in visualizer.Editor.GetSelectedNotes()) {
                    note.Basic.StartPosition = startPosition == NotePosition.Nowhere ? note.Basic.FinishPosition : startPosition;
                }
                visualizer.Editor.ClearSelectedNotes();
            }

            visualizer.Editor.NoteStartPosition = startPosition;

            if (hasSelectedNotes) {
                visualizer.Editor.Invalidate();
            }

            var item = (ToolStripMenuItem)sender;
            ToolStripItemCollection itemCollection;
            switch (item.OwnerItem) {
                case ToolStripMenuItem menuItem:
                    itemCollection = menuItem.DropDownItems;
                    break;
                case ToolStripDropDownButton button:
                    itemCollection = button.DropDownItems;
                    break;
                default:
                    throw new NotSupportedException();
            }
            foreach (ToolStripMenuItem it in itemCollection) {
                it.Checked = it == item;
            }

            tsbEditNoteStartPosition.Text = DescribedEnumConverter.GetEnumDescription(startPosition);
        }

        private void CmdEditNoteStartPosition0_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSet.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPosition1_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSet.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPosition2_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSet.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPosition3_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSet.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPosition4_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSet.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPosition5_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSet.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedNotes();
            visualizer.Editor.Invalidate();
        }

        private readonly Command CmdEditNoteStartPositionSet = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPosition0 = CommandManager.CreateCommand("Alt+0");
        private readonly Command CmdEditNoteStartPosition1 = CommandManager.CreateCommand("Alt+1");
        private readonly Command CmdEditNoteStartPosition2 = CommandManager.CreateCommand("Alt+2");
        private readonly Command CmdEditNoteStartPosition3 = CommandManager.CreateCommand("Alt+3");
        private readonly Command CmdEditNoteStartPosition4 = CommandManager.CreateCommand("Alt+4");
        private readonly Command CmdEditNoteStartPosition5 = CommandManager.CreateCommand("Alt+5");
        private readonly Command CmdEditNoteDelete = CommandManager.CreateCommand("Delete");

    }
}
