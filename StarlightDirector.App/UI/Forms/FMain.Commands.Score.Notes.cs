using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Commanding;
using StarlightDirector.UI.Controls;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdScoreNoteStartPositionSetAt_Executed(object sender, ExecutedEventArgs e) {
            var startPosition = (NotePosition)e.Parameter;
            visualizer.Editor.NoteStartPosition = startPosition;

            var atMenuItems = new[] {
                mnuScoreNoteStartPositionAt0, mnuScoreNoteStartPositionAt1, mnuScoreNoteStartPositionAt2,
                mnuScoreNoteStartPositionAt3, mnuScoreNoteStartPositionAt4, mnuScoreNoteStartPositionAt5
            };
            foreach (var it in atMenuItems) {
                var param = it.GetParameter();
                if (param == null) {
                    continue;
                }
                var pos = (NotePosition)param;
                it.Checked = pos == startPosition;
            }

            tsbScoreNoteStartPosition.Text = DescribedEnumConverter.GetEnumDescription(startPosition);
        }

        private void CmdScoreNoteStartPositionAt0_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionAt1_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionAt2_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionAt3_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionAt4_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionAt5_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetAt.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionMoveLeft_Executed(object sender, ExecutedEventArgs e) {
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

        private void CmdScoreNoteStartPositionMoveRight_Executed(object sender, ExecutedEventArgs e) {
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

        private void CmdScoreNoteStartPositionSetTo_Executed(object sender, ExecutedEventArgs e) {
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

        private void CmdScoreNoteStartPositionTo0_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionTo1_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionTo2_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionTo3_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionTo4_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteStartPositionTo5_Executed(object sender, ExecutedEventArgs e) {
            CmdScoreNoteStartPositionSetTo.Execute(sender, e.Parameter);
        }

        private void CmdScoreNoteDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedNotes();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreNoteInsertSpecial_Executed(object sender, ExecutedEventArgs e) {
            var hit = (ScoreEditorHitTestResult)e.Parameter;
            if (hit == null || !hit.HitAnyBar) {
                return;
            }
            var (r, newBpm) = FSpecialNote.VariantBpmRequestInput(this);
            if (r == DialogResult.Cancel) {
                return;
            }
            var bar = hit.Bar;
            var note = bar.AddSpecialNote(NoteType.VariantBpm);
            note.Params.NewBpm = newBpm;
            note.Basic.IndexInGrid = hit.Row;

            visualizer.Editor.Invalidate();
        }

        private void CmdScoreNoteModifySpecial_Executed(object sender, ExecutedEventArgs e) {
            var hit = (ScoreEditorHitTestResult)e.Parameter;
            if (hit == null || !hit.HitAnyNote) {
                return;
            }
            var note = hit.Note;
            var originalBpm = note.Params.NewBpm;
            var (r, newBpm) = FSpecialNote.VariantBpmRequestInput(this, originalBpm);
            if (r == DialogResult.Cancel) {
                return;
            }
            note.Params.NewBpm = newBpm;
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreNoteDeleteSpecial_Executed(object sender, ExecutedEventArgs e) {
            var hit = (ScoreEditorHitTestResult)e.Parameter;
            if (hit == null || !hit.HitAnyNote) {
                return;
            }
            var note = hit.Note;
            note.Basic.Bar.RemoveNote(note);
            visualizer.Editor.Invalidate();
        }

        private readonly Command CmdScoreNoteStartPositionSetAt = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt0 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt1 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt2 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt3 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt4 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt5 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionMoveLeft = CommandManager.CreateCommand("Alt+Q");
        private readonly Command CmdScoreNoteStartPositionMoveRight = CommandManager.CreateCommand("Alt+E");
        private readonly Command CmdScoreNoteStartPositionSetTo = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo0 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo1 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo2 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo3 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo4 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo5 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteDelete = CommandManager.CreateCommand("Delete");
        private readonly Command CmdScoreNoteInsertSpecial = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteModifySpecial = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteDeleteSpecial = CommandManager.CreateCommand();

    }
}
