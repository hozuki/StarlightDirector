using System.Diagnostics;
using StarlightDirector.Beatmap;

namespace StarlightDirector.UI.Controls {
    public sealed class ScoreEditorHitTestResult {

        [DebuggerStepThrough]
        internal ScoreEditorHitTestResult(Bar bar, Note note, int row, NotePosition column) {
            Bar = bar;
            Note = note;
            Row = row;
            Column = column;
        }

        [DebuggerStepThrough]
        internal ScoreEditorHitTestResult(Bar bar, Note note, int row, int track)
            : this(bar, note, row, (NotePosition)track) {
        }

        public Bar Bar { get; }

        public Note Note { get; }

        public int Row { get; }

        public NotePosition Column { get; }

        public bool HitAnyBar => Bar != null;

        public bool HitBarGridIntersection => Bar != null && Row >= 0 && Column != NotePosition.Nowhere;

        public bool HitAnyNote => Note != null;

        public static readonly ScoreEditorHitTestResult Invalid = new ScoreEditorHitTestResult(null, null, -1, NotePosition.Nowhere);

    }
}
