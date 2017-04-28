using System;
using System.Diagnostics;
using System.Linq;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap.Extensions {
    public static class BarExtensions {

        public static Note AddNote(this Bar bar, int row, NotePosition column) {
            return AddNote(bar, Guid.NewGuid(), row, column);
        }

        public static Note AddNote(this Bar bar, int id, int row, NotePosition column) {
            var guid = StarlightID.GetGuidFromInt32(id);
            return AddNote(bar, guid, row, column);
        }

        public static Note AddNote(this Bar bar, Guid id, int row, NotePosition column) {
            if (row < 0 || row >= bar.GetNumberOfGrids()) {
                throw new ArgumentOutOfRangeException(nameof(row), row, null);
            }
            if (column == NotePosition.Default) {
                throw new ArgumentOutOfRangeException(nameof(column), column, null);
            }
            if (bar.Notes.Any(n => n.Basic.IndexInGrid == row && n.Basic.FinishPosition == column)) {
                throw new InvalidOperationException($"A note exists at row {row}, column {column}.");
            }

            var note = new Note(bar, id);
            bar.Notes.Add(note);
            bar.Score.Project.UsedNoteIDs.Add(id);
            // IndexInGrid must be set before calling FixSyncWhenAdded.
            note.Basic.IndexInGrid = row;
            note.Basic.StartPosition = note.Basic.FinishPosition = column;
            note.FixSyncWhenAdded();
            bar.Notes.Sort(Note.TimingThenPositionComparison);
            return note;
        }

        public static Note AddSpecialNote(this Bar bar, NoteType specialNoteType) {
            var guid = Guid.NewGuid();
            return AddSpecialNote(bar, guid, specialNoteType);
        }

        public static Note AddSpecialNote(this Bar bar, int id, NoteType specialNoteType) {
            var guid = StarlightID.GetGuidFromInt32(id);
            return AddSpecialNote(bar, guid, specialNoteType);
        }

        public static Note AddSpecialNote(this Bar bar, Guid id, NoteType specialNoteType) {
            var note = new Note(bar, id);
            note.Params = new NoteExtraParams();
            note.SetSpecialType(specialNoteType);
            bar.Notes.Add(note);
            bar.Score.Project.UsedNoteIDs.Add(id);
            return note;
        }

        public static Note RemoveNote(this Bar bar, Guid id) {
            var note = FindNoteByID(bar, id);
            return note == null ? null : RemoveNote(bar, note);
        }

        public static Note RemoveNote(this Bar bar, Note note) {
            if (note == null) {
                return null;
            }
            if (!bar.Notes.Contains(note)) {
                throw new ArgumentException("Note is not found in bar.");
            }

            // Relations
            note.ResetAsTap();

            bar.Notes.Remove(note);
            bar.Score.Project.UsedNoteIDs.Remove(note.StarlightID);
            return note;
        }

        [DebuggerStepThrough]
        public static Note FindNoteByID(this Bar bar, Guid id) {
            return bar.Notes.FirstOrDefault(n => n.StarlightID == id);
        }

        [DebuggerStepThrough]
        public static int GetSignature(this Bar bar) {
            return bar.Params?.UserDefinedSignature ?? bar.Score.Project.Settings.Signature;
        }

        [DebuggerStepThrough]
        public static int GetGridPerSignature(this Bar bar) {
            return bar.Params?.UserDefinedGridPerSignature ?? bar.Score.Project.Settings.GridPerSignature;
        }

        [DebuggerStepThrough]
        public static int GetNumberOfGrids(this Bar bar) {
            return GetGridPerSignature(bar) * GetSignature(bar);
        }

        [DebuggerStepThrough]
        public static void EditorToggleSelected(this Bar bar) {
            bar.IsSelected = !bar.IsSelected;
        }

        [DebuggerStepThrough]
        public static void EditorSelect(this Bar bar) {
            bar.IsSelected = true;
        }

        [DebuggerStepThrough]
        public static void EditorUnselect(this Bar bar) {
            bar.IsSelected = false;
        }

        internal static Note RemoveSpecialNoteForVariantBpmFix(this Bar bar, Note note) {
            if (note == null) {
                return null;
            }
            bar.Notes.Remove(note);
            bar.Score.Project.UsedNoteIDs.Remove(note.StarlightID);
            return note;
        }

        internal static Note AddNoteDirect(this Bar bar, Note note) {
            if (note == null) {
                return null;
            }
            bar.Notes.Add(note);
            return note;
        }

    }
}
