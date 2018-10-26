using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Input;
using OpenCGSS.StarlightDirector.Localization;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Beatmap.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMain {

        private void CmdScoreNoteStartPositionSetAt_Executed(object sender, ExecutedEventArgs e) {
            Debug.Assert(e.Parameter != null, "e.Parameter != null");

            var startPosition = (NotePosition)e.Parameter;
            visualizer.Editor.NoteStartPosition = startPosition;

            var atMenuItems = new[] {
                mnuScoreNoteStartPositionAt0, mnuScoreNoteStartPositionAt1, mnuScoreNoteStartPositionAt2,
                mnuScoreNoteStartPositionAt3, mnuScoreNoteStartPositionAt4, mnuScoreNoteStartPositionAt5
            };

            foreach (var it in atMenuItems) {
                var commandSource = CommandHelper.FindCommandSource(it);

                Debug.Assert(commandSource != null, nameof(commandSource) + " != null");
                Debug.Assert(commandSource.CommandParameter != null, "commandSource.CommandParameter != null");

                var pos = (NotePosition)commandSource.CommandParameter;

                it.Checked = pos == startPosition;
            }

            tsbScoreNoteStartPosition.Text = DescribedEnumConverter.GetEnumDescription(startPosition);
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

            Debug.Assert(note.Params != null, "note.Params != null");

            note.Params.NewBpm = newBpm;
            note.Basic.IndexInGrid = newRowIndex;

            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();

            ctxScoreNoteInsertSpecial.DeleteCommandParameter();
            ctxScoreNoteModifySpecial.DeleteCommandParameter();
            ctxScoreNoteDeleteSpecial.DeleteCommandParameter();
        }

        private void CmdScoreNoteModifySpecial_Executed(object sender, ExecutedEventArgs e) {
            var hit = (ScoreEditorHitTestResult)e.Parameter;

            if (hit == null || !hit.HitAnyNote) {
                return;
            }

            var note = hit.Note;

            Debug.Assert(note.Params != null, "note.Params != null");

            var originalBpm = note.Params.NewBpm;
            var (r, newBpm) = FSpecialNote.VariantBpmRequestInput(this, hit.Bar.Basic.Index, hit.Row, originalBpm);

            if (r == DialogResult.Cancel) {
                return;
            }
            note.Params.NewBpm = newBpm;
            InformProjectModified();
            visualizer.Editor.UpdateBarStartTimeText();

            ctxScoreNoteInsertSpecial.DeleteCommandParameter();
            ctxScoreNoteModifySpecial.DeleteCommandParameter();
            ctxScoreNoteDeleteSpecial.DeleteCommandParameter();
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

            ctxScoreNoteInsertSpecial.DeleteCommandParameter();
            ctxScoreNoteModifySpecial.DeleteCommandParameter();
            ctxScoreNoteDeleteSpecial.DeleteCommandParameter();
        }

        internal readonly CommandBinding CmdScoreNoteStartPositionSetAt = CommandHelper.CreateUIBinding();
        internal readonly CommandBinding CmdScoreNoteStartPositionMoveLeft = CommandHelper.CreateUIBinding("Alt+A");
        internal readonly CommandBinding CmdScoreNoteStartPositionMoveRight = CommandHelper.CreateUIBinding("Alt+D");
        internal readonly CommandBinding CmdScoreNoteStartPositionSetTo = CommandHelper.CreateUIBinding();
        internal readonly CommandBinding CmdScoreNoteResetToTap = CommandHelper.CreateUIBinding();
        internal readonly CommandBinding CmdScoreNoteDelete = CommandHelper.CreateUIBinding("Delete");
        internal readonly CommandBinding CmdScoreNoteInsertSpecial = CommandHelper.CreateUIBinding();
        internal readonly CommandBinding CmdScoreNoteModifySpecial = CommandHelper.CreateUIBinding();
        internal readonly CommandBinding CmdScoreNoteDeleteSpecial = CommandHelper.CreateUIBinding();

    }
}
