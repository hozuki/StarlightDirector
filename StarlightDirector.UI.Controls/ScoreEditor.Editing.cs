using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        [DebuggerStepThrough]
        public Note AddNoteAt(Bar bar, int row, NotePosition column) {
            var score = CurrentScore;
            if (score == null) {
                return null;
            }
            if (!score.Bars.Contains(bar)) {
                throw new ArgumentException("Assigned bar is not in current score.", nameof(bar));
            }
            var note = bar.AddNote(row, column);
            if (NoteStartPosition != NotePosition.Default) {
                note.Basic.StartPosition = NoteStartPosition;
            }
            return note;
        }

        [DebuggerStepThrough]
        public Note RemoveNoteAt(Bar bar, int row, NotePosition column) {
            var score = CurrentScore;
            if (score == null) {
                return null;
            }
            if (!score.Bars.Contains(bar)) {
                throw new ArgumentException("Assigned bar is not in current score.", nameof(bar));
            }
            var note = bar.Notes.FirstOrDefault(n => n.Basic.IndexInGrid == row && n.Basic.FinishPosition == column);
            return note == null ? null : bar.RemoveNote(note);
        }

        [DebuggerStepThrough]
        public Bar AppendBar() {
            var score = CurrentScore;
            return score?.AppendBar();
        }

        [DebuggerStepThrough]
        public IReadOnlyList<Note> RemoveSelectedNotes() {
            var selectedNotes = GetSelectedNotes().ToArray();
            foreach (var selectedNote in selectedNotes) {
                selectedNote.Basic.Bar.RemoveNote(selectedNote);
            }
            return selectedNotes;
        }

        [DebuggerStepThrough]
        public IReadOnlyList<Bar> RemoveSelectedBars() {
            var selectedBars = GetSelectedBars().ToArray();
            var score = CurrentScore;
            score?.RemoveBars(selectedBars);
            return selectedBars;
        }

        [DebuggerStepThrough]
        public IEnumerable<Note> GetSelectedNotes() {
            var score = CurrentScore;
            if (score == null) {
                return Enumerable.Empty<Note>();
            }
            return score.Bars.SelectMany(bar => bar.Notes.Where(note => note.Editor.IsSelected));
        }

        public bool HasSelectedNotes {
            [DebuggerStepThrough]
            get { return GetSelectedNotes().Any(); }
        }

        [DebuggerStepThrough]
        public int GetSelectedNoteCount() {
            return GetSelectedNotes().Count();
        }

        /// <summary>
        /// Get the only selected note. If there is no selected notes, or there are more than one selected notes,
        /// this method returns null.
        /// </summary>
        /// <returns>The only selected note, or null.</returns>
        [DebuggerStepThrough]
        public Note GetSelectedNote() {
            return GetSelectedNotes().SingleOrDefault();
        }

        public bool HasOneSelectedNote {
            [DebuggerStepThrough]
            get { return GetSelectedNotes().SingleOrDefault() != null; }
        }

        [DebuggerStepThrough]
        public IEnumerable<Bar> GetSelectedBars() {
            var score = CurrentScore;
            if (score == null) {
                return Enumerable.Empty<Bar>();
            }
            return score.Bars.Where(bar => bar.Editor.IsSelected);
        }

        public bool HasSelectedBars {
            [DebuggerStepThrough]
            get { return GetSelectedBars().Any(); }
        }

        [DebuggerStepThrough]
        public int GetSelectedBarCount() {
            return GetSelectedBars().Count();
        }

        [DebuggerStepThrough]
        public Bar GetSelectedBar() {
            return GetSelectedBars().SingleOrDefault();
        }

        public bool HasOneSelectedBar {
            [DebuggerStepThrough]
            get { return GetSelectedBars().SingleOrDefault() != null; }
        }

        [DebuggerStepThrough]
        public void ClearSelectedNotes() {
            if (!HasSelectedNotes) {
                return;
            }
            var score = CurrentScore;
            foreach (var bar in score.Bars) {
                foreach (var note in bar.Notes) {
                    note.EditorUnselect();
                }
            }
        }

        [DebuggerStepThrough]
        public void ClearSelectedNotesExcept(Note note) {
            if (!HasSelectedNotes) {
                return;
            }
            var score = CurrentScore;
            foreach (var bar in score.Bars) {
                foreach (var n in bar.Notes) {
                    if (n != note) {
                        n.EditorUnselect();
                    }
                }
            }
        }

        [DebuggerStepThrough]
        public void ClearSelectedBars() {
            if (!HasSelectedBars) {
                return;
            }
            var score = CurrentScore;
            foreach (var bar in score.Bars) {
                bar.EditorUnselect();
            }
        }

        //[DebuggerStepThrough]
        public void ClearSelectedBarsExcept(Bar bar) {
            if (!HasSelectedBars) {
                return;
            }
            var score = CurrentScore;
            foreach (var b in score.Bars) {
                if (b != bar) {
                    b.EditorUnselect();
                }
            }
        }

        [DebuggerStepThrough]
        public void SelectAllNotes() {
            var score = CurrentScore;
            if (score == null) {
                return;
            }
            foreach (var bar in score.Bars) {
                foreach (var note in bar.Notes) {
                    note.EditorSelect();
                }
            }
        }

        [DebuggerStepThrough]
        public void SelectAllBars() {
            var score = CurrentScore;
            if (score == null) {
                return;
            }
            foreach (var bar in score.Bars) {
                bar.EditorSelect();
            }
        }

    }
}
