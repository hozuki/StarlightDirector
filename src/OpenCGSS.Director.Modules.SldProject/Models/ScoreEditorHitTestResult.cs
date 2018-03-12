using System.Diagnostics;
using JetBrains.Annotations;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap;

namespace OpenCGSS.Director.Modules.SldProject.Models {
    public sealed class ScoreEditorHitTestResult {

        [DebuggerStepThrough]
        internal ScoreEditorHitTestResult(System.Windows.Point location, ScoreEditorHitRegion hitRegion, [CanBeNull] Bar bar, [CanBeNull] Note note, int row, NotePosition column) {
            Location = location;
            HitRegion = hitRegion;
            Bar = bar;
            Note = note;
            Row = row;
            Column = column;
        }

        [DebuggerStepThrough]
        internal ScoreEditorHitTestResult(System.Windows.Point location, ScoreEditorHitRegion hitRegion, [CanBeNull] Bar bar, [CanBeNull] Note note, int row, int track)
            : this(location, hitRegion, bar, note, row, (NotePosition)track) {
        }

        public System.Windows.Point Location { get; }

        [CanBeNull]
        public Bar Bar { get; }

        [CanBeNull]
        public Note Note { get; }

        public int Row { get; }

        public NotePosition Column { get; }

        public ScoreEditorHitRegion HitRegion { get; }

        public bool HitAnyBar => Bar != null;

        public bool HitBarGridIntersection => Bar != null && Row >= 0 && Column != NotePosition.Default;

        public bool HitAnyNote => Note != null;

        internal static ScoreEditorHitTestResult GetInvalidResult(System.Windows.Point location) {
            return new ScoreEditorHitTestResult(location, ScoreEditorHitRegion.None, null, null, -1, NotePosition.Default);
        }

        internal static ScoreEditorHitTestResult GetInvalidResult(double x, double y) {
            return new ScoreEditorHitTestResult(new System.Windows.Point(x, y), ScoreEditorHitRegion.None, null, null, -1, NotePosition.Default);
        }

    }
}
