using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using StarlightDirector.Beatmap;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        [DebuggerStepThrough]
        public IEnumerable<Note> GetSelectedNotes() {
            var score = Project?.GetScore(Difficulty);
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
        public IEnumerable<Bar> GetSelectedBars() {
            var score = Project?.GetScore(Difficulty);
            if (score == null) {
                return Enumerable.Empty<Bar>();
            }
            return score.Bars.Where(bar => bar.IsSelected);
        }

        public bool HasSelectedBars {
            [DebuggerStepThrough]
            get { return GetSelectedBars().Any(); }
        }

        [DebuggerStepThrough]
        public void ClearSelectedNotes() {
            if (!HasSelectedNotes) {
                return;
            }
            var score = Project.GetScore(Difficulty);
            foreach (var bar in score.Bars) {
                foreach (var note in bar.Notes) {
                    note.Editor.IsSelected = false;
                }
            }
        }

        [DebuggerStepThrough]
        public void ClearSelectedNotesExcept(Note note) {
            if (!HasSelectedNotes) {
                return;
            }
            var score = Project.GetScore(Difficulty);
            foreach (var bar in score.Bars) {
                foreach (var n in bar.Notes) {
                    if (n != note) {
                        n.Editor.IsSelected = false;
                    }
                }
            }
        }

        [DebuggerStepThrough]
        public void ClearSelectedBars() {
            if (!HasSelectedBars) {
                return;
            }
            var score = Project.GetScore(Difficulty);
            foreach (var bar in score.Bars) {
                bar.IsSelected = false;
            }
        }

        [DebuggerStepThrough]
        public void ClearSelectedBarsExcept(Bar bar) {
            if (!HasSelectedBars) {
                return;
            }
            var score = Project.GetScore(Difficulty);
            foreach (var b in score.Bars) {
                if (b != bar) {
                    b.IsSelected = false;
                }
            }
        }

    }
}
