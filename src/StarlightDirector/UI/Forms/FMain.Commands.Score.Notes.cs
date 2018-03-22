using System.Linq;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.Input;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Forms {
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
                var newPos = pos > NotePosition.P1 ? pos - 1 : NotePosition.P5;
                note.Basic.StartPosition = newPos;
            }
            InformProjectModified();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreNoteStartPositionMoveRight_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Editor.HasSelectedNotes) {
                return;
            }
            var notes = visualizer.Editor.GetSelectedNotes();
            foreach (var note in notes) {
                var pos = note.Basic.StartPosition;
                var newPos = pos < NotePosition.P5 ? pos + 1 : NotePosition.P1;
                note.Basic.StartPosition = newPos;
            }
            InformProjectModified();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreNoteStartPositionSetTo_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Editor.HasSelectedNotes) {
                return;
            }
            var startPosition = (NotePosition)e.Parameter;
            var notes = visualizer.Editor.GetSelectedNotes().ToList();
            // By sorting, we ensure to visit hold end after hold start.
            notes.Sort(Note.TimingThenPositionComparison);
            foreach (var note in notes) {
                if (!note.Helper.IsHoldEnd) {
                    note.Basic.StartPosition = startPosition == NotePosition.Default ? note.Basic.FinishPosition : startPosition;
                    if (note.Helper.IsHoldStart) {
                        // Start position of hold end must be the same as hold start.
                        note.Editor.HoldPair.Basic.StartPosition = note.Basic.StartPosition;
                    }
                }
            }
            InformProjectModified();
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

        private void CmdScoreNoteResetToTap_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Editor.HasSelectedNotes) {
                return;
            }
            foreach (var note in visualizer.Editor.GetSelectedNotes()) {
                note.ResetToTap(true);
            }
            InformProjectModified();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreNoteDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedNotes();
            InformProjectModified();
            visualizer.Editor.Invalidate();
        }

        private void CmdScoreNoteInsertSpecial_Executed(object sender, ExecutedEventArgs e) {
            var hit = (ScoreEditorHitTestResult)e.Parameter;
            double newBpm;
            Bar bar;
            int newRowIndex;
            if (hit == null) {
                var r = FSpecialNote.VariantBpmRequestInput(this, visualizer.Editor.CurrentScore);
                if (r.DialogResult == DialogResult.Cancel) {
                    return;
                }
                newBpm = r.NewBpm;
                bar = visualizer.Editor.CurrentScore?.Bars.Find(b => b.Basic.Index == r.BarIndex);
                if (bar == null) {
                    return;
                }
                newRowIndex = r.RowIndex;
            } else {
                if (!hit.HitAnyBar) {
                    return;
                }
                var r = FSpecialNote.VariantBpmRequestInput(this, hit.Bar.Basic.Index, hit.Row);
                if (r.DialogResult == DialogResult.Cancel) {
                    return;
                }
                newBpm = r.NewBpm;
                bar = hit.Bar;
                newRowIndex = hit.Row;
            }
            var note = bar.AddSpecialNote(NoteType.VariantBpm);
            note.Params.NewBpm = newBpm;
            note.Basic.IndexInGrid = newRowIndex;

            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();

            ctxScoreNoteInsertSpecial.DeleteParameter();
            ctxScoreNoteModifySpecial.DeleteParameter();
            ctxScoreNoteDeleteSpecial.DeleteParameter();
        }

        private void CmdScoreNoteModifySpecial_Executed(object sender, ExecutedEventArgs e) {
            var hit = (ScoreEditorHitTestResult)e.Parameter;
            if (hit == null || !hit.HitAnyNote) {
                return;
            }
            var note = hit.Note;
            var originalBpm = note.Params.NewBpm;
            var (r, newBpm) = FSpecialNote.VariantBpmRequestInput(this, hit.Bar.Basic.Index, hit.Row, originalBpm);
            if (r == DialogResult.Cancel) {
                return;
            }
            note.Params.NewBpm = newBpm;
            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();

            ctxScoreNoteInsertSpecial.DeleteParameter();
            ctxScoreNoteModifySpecial.DeleteParameter();
            ctxScoreNoteDeleteSpecial.DeleteParameter();
        }

        private void CmdScoreNoteDeleteSpecial_Executed(object sender, ExecutedEventArgs e) {
            var hit = (ScoreEditorHitTestResult)e.Parameter;
            if (hit == null || !hit.HitAnyNote) {
                return;
            }
            var note = hit.Note;
            note.Basic.Bar.RemoveNote(note);
            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();

            ctxScoreNoteInsertSpecial.DeleteParameter();
            ctxScoreNoteModifySpecial.DeleteParameter();
            ctxScoreNoteDeleteSpecial.DeleteParameter();
        }

        private readonly Command CmdScoreNoteStartPositionSetAt = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt0 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt1 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt2 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt3 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt4 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionAt5 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionMoveLeft = CommandManager.CreateCommand("Alt+A");
        private readonly Command CmdScoreNoteStartPositionMoveRight = CommandManager.CreateCommand("Alt+D");
        private readonly Command CmdScoreNoteStartPositionSetTo = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo0 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo1 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo2 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo3 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo4 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteStartPositionTo5 = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteResetToTap = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteDelete = CommandManager.CreateCommand("Delete");
        private readonly Command CmdScoreNoteInsertSpecial = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteModifySpecial = CommandManager.CreateCommand();
        private readonly Command CmdScoreNoteDeleteSpecial = CommandManager.CreateCommand();

    }
}
