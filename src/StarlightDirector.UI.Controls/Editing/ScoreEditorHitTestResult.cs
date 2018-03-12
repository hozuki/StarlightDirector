using System.Diagnostics;
using System.Drawing;
using StarlightDirector.Beatmap;

namespace StarlightDirector.UI.Controls.Editing {
    public sealed class ScoreEditorHitTestResult {

        [DebuggerStepThrough]
        internal ScoreEditorHitTestResult(Point location, ScoreEditorHitRegion hitRegion, Bar bar, Note note, int row, NotePosition column) {
            Location = location;
            HitRegion = hitRegion;
            Bar = bar;
            Note = note;
            Row = row;
            Column = column;
        }

        [DebuggerStepThrough]
        internal ScoreEditorHitTestResult(Point location, ScoreEditorHitRegion hitRegion, Bar bar, Note note, int row, int track)
            : this(location, hitRegion, bar, note, row, (NotePosition)track) {
        }

        public Point Location { get; }

        public Bar Bar { get; }

        public Note Note { get; }

        public int Row { get; }

        public NotePosition Column { get; }

        public ScoreEditorHitRegion HitRegion { get; }

        public bool HitAnyBar => Bar != null;

        public bool HitBarGridIntersection => Bar != null && Row >= 0 && Column != NotePosition.Default;

        public bool HitAnyNote => Note != null;

        internal static ScoreEditorHitTestResult GetInvalidResult(Point location) {
            return new ScoreEditorHitTestResult(location, ScoreEditorHitRegion.None, null, null, -1, NotePosition.Default);
        }

        internal static ScoreEditorHitTestResult GetInvalidResult(int x, int y) {
            return new ScoreEditorHitTestResult(new Point(x, y), ScoreEditorHitRegion.None, null, null, -1, NotePosition.Default);
        }

    }
}
