using StarlightDirector.Beatmap;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdEditNoteStartPositionSetAt_Executed(object sender, ExecutedEventArgs e) {
            var startPosition = (NotePosition)e.Parameter;
            visualizer.Editor.NoteStartPosition = startPosition;

            var atMenuItems = new[] {
                mnuEditNoteStartPositionAt0, mnuEditNoteStartPositionAt1, mnuEditNoteStartPositionAt2,
                mnuEditNoteStartPositionAt3, mnuEditNoteStartPositionAt4, mnuEditNoteStartPositionAt5
            };
            foreach (var it in atMenuItems) {
                var param = it.GetParameter();
                if (param == null) {
                    continue;
                }
                var pos = (NotePosition)param;
                it.Checked = pos == startPosition;
            }

            tsbEditNoteStartPosition.Text = DescribedEnumConverter.GetEnumDescription(startPosition);
        }

        private void CmdEditNoteStartPositionAt0_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionAt1_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionAt2_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionAt3_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionAt4_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionAt5_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionMoveLeft_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Editor.HasSelectedNotes) {
                return;
            }
            var notes = visualizer.Editor.GetSelectedNotes();
            foreach (var note in notes) {
                var pos = note.Basic.StartPosition;
                var newPos = pos > NotePosition.Left ? pos - 1 : NotePosition.Right;
                note.Basic.StartPosition = newPos;
            }
            visualizer.Editor.Invalidate();
        }

        private void CmdEditNoteStartPositionMoveRight_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Editor.HasSelectedNotes) {
                return;
            }
            var notes = visualizer.Editor.GetSelectedNotes();
            foreach (var note in notes) {
                var pos = note.Basic.StartPosition;
                var newPos = pos < NotePosition.Right ? pos + 1 : NotePosition.Left;
                note.Basic.StartPosition = newPos;
            }
            visualizer.Editor.Invalidate();
        }

        private void CmdEditNoteStartPositionSetTo_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Editor.HasSelectedNotes) {
                return;
            }
            var startPosition = (NotePosition)e.Parameter;
            var notes = visualizer.Editor.GetSelectedNotes();
            foreach (var note in notes) {
                note.Basic.StartPosition = startPosition == NotePosition.Default ? note.Basic.FinishPosition : startPosition;
            }
            visualizer.Editor.Invalidate();
        }

        private void CmdEditNoteStartPositionTo0_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionTo1_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionTo2_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionTo3_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionTo4_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteStartPositionTo5_Executed(object sender, ExecutedEventArgs e) {
            CmdEditNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdEditNoteDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedNotes();
            visualizer.Editor.Invalidate();
        }

        private readonly Command CmdEditNoteStartPositionSetAt = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionAt0 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionAt1 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionAt2 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionAt3 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionAt4 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionAt5 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionMoveLeft = CommandManager.CreateCommand("Alt+Q");
        private readonly Command CmdEditNoteStartPositionMoveRight = CommandManager.CreateCommand("Alt+E");
        private readonly Command CmdEditNoteStartPositionSetTo = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionTo0 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionTo1 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionTo2 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionTo3 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionTo4 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteStartPositionTo5 = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteDelete = CommandManager.CreateCommand("Delete");

    }
}
