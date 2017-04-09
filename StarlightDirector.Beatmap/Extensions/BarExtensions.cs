using System;
using System.Diagnostics;
using System.Linq;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap.Extensions {
    public static class BarExtensions {

        public static Note AddNote(this Bar bar) {
            return AddNote(bar, Guid.NewGuid());
        }

        public static Note AddNote(this Bar bar, int id) {
            var guid = StarlightID.GetGuidFromInt32(id);
            return AddNote(bar, guid);
        }

        public static Note AddNote(this Bar bar, Guid id) {
            var note = new Note(bar, id);
            bar.Notes.Add(note);
            bar.Notes.Sort(Note.TimingThenPositionComparison);
            bar.Score.Project.UsedNoteIDs.Add(id);
            return note;
        }

        public static Note RemoveNote(this Bar bar, Guid id) {
            var note = FindNoteByID(bar, id);
            if (note == null) {
                return null;
            }
            return RemoveNote(bar, note);
        }

        public static Note RemoveNote(this Bar bar, Note note) {
            if (note == null) {
                return null;
            }
            if (!bar.Notes.Contains(note)) {
                throw new ArgumentException("Note is not found in bar.");
            }
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

    }
}
