using System;
using System.Diagnostics;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap {
    public sealed class Note : IStarlightObject {

        internal Note(Bar bar)
            : this(bar, Guid.NewGuid()) {
        }

        internal Note(Bar bar, Guid id) {
            Basic = new NoteBasicProperties(bar);
            Helper = new NoteHelperProperties(this);
            Editor = new NoteEditorProperties();
            StarlightID = id;
        }

        public Guid StarlightID {
            [DebuggerStepThrough]
            get { return Basic.ID; }
            [DebuggerStepThrough]
            internal set { Basic.ID = value; }
        }

        public NoteBasicProperties Basic { get; }

        public NoteHelperProperties Helper { get; }

        public NoteEditorProperties Editor { get; }

        public NoteExtraParams Params { get; internal set; }

        public sealed class NoteBasicProperties {

            internal NoteBasicProperties(Bar bar) {
                Bar = bar;
            }

            public Guid ID { get; internal set; }

            public Bar Bar { get; }

            public NoteType Type { get; set; } = NoteType.TapOrFlick;

            public int IndexInGrid { get; set; }

            public NotePosition StartPosition { get; set; } = NotePosition.Default;

            public NotePosition FinishPosition { get; set; } = NotePosition.Default;

            public NoteFlickType FlickType { get; set; } = NoteFlickType.None;

        }

        public sealed class NoteHelperProperties {

            internal NoteHelperProperties(Note note) {
                _note = note;
            }

            public bool IsGaming {
                [DebuggerStepThrough]
                get { return IsTypeGaming(_note.Basic.Type); }
            }

            public bool IsSpecial {
                [DebuggerStepThrough]
                get { return IsTypeSpecial(_note.Basic.Type); }
            }

            public bool IsCgssInternal {
                [DebuggerStepThrough]
                get { return IsTypeCgssInternal(_note.Basic.Type); }
            }

            public bool IsTap {
                [DebuggerStepThrough]
                get { return _note.Basic.Type == NoteType.TapOrFlick && _note.Basic.FlickType == NoteFlickType.None; }
            }

            public bool IsSync {
                [DebuggerStepThrough]
                get { return HasPrevSync || HasNextSync; }
            }

            public bool HasPrevSync {
                [DebuggerStepThrough]
                get { return _note.Editor.PrevSync != null; }
            }

            public bool HasNextSync {
                [DebuggerStepThrough]
                get { return _note.Editor.NextSync != null; }
            }

            public bool IsFlick {
                [DebuggerStepThrough]
                get { return _note.Basic.FlickType != NoteFlickType.None; }
            }

            public bool IsFlickStart {
                [DebuggerStepThrough]
                get { return IsFlick && HasNextFlick && !HasPrevFlick; }
            }

            public bool IsFlickEnd {
                [DebuggerStepThrough]
                get { return IsFlick && HasPrevFlick && !HasNextFlick; }
            }

            public bool IsFlickMidway {
                [DebuggerStepThrough]
                get { return IsFlick && HasPrevFlick && HasNextFlick; }
            }

            public bool HasPrevFlick {
                [DebuggerStepThrough]
                get { return _note.Editor.PrevFlick != null; }
            }

            public bool HasNextFlick {
                [DebuggerStepThrough]
                get { return _note.Editor.NextFlick != null; }
            }

            public bool IsHoldStart {
                [DebuggerStepThrough]
                get { return _note.Basic.Type == NoteType.Hold; }
            }

            public bool IsHoldEnd {
                [DebuggerStepThrough]
                get { return HasHoldPair && _note.Basic.Type != NoteType.Hold; }
            }

            public bool IsHold {
                [DebuggerStepThrough]
                get { return IsHoldStart || IsHoldEnd; }
            }

            public bool HasHoldPair {
                [DebuggerStepThrough]
                get { return _note.Editor.HoldPair != null; }
            }

            public bool IsSlide {
                [DebuggerStepThrough]
                get { return _note.Basic.Type == NoteType.Slide; }
            }

            public bool IsSlideStart {
                [DebuggerStepThrough]
                get { return IsSlide && HasNextSlide && !HasPrevSlide; }
            }

            public bool IsSlideEnd {
                [DebuggerStepThrough]
                get { return IsSlide && HasPrevSlide && !HasNextSlide; }
            }

            public bool IsSlideMidway {
                [DebuggerStepThrough]
                get { return IsSlide && HasPrevSlide && HasNextSlide; }
            }

            public bool HasNextSlide {
                [DebuggerStepThrough]
                get { return _note.Editor.NextSlide != null; }
            }

            public bool HasPrevSlide {
                [DebuggerStepThrough]
                get { return _note.Editor.PrevSlide != null; }
            }

            private readonly Note _note;

        }

        public sealed class NoteEditorProperties {

            internal NoteEditorProperties() {
            }

            public bool IsSelected { get; internal set; }

            public Note NextSync { get; internal set; }

            public Note PrevSync { get; internal set; }

            public Note NextFlick { get; internal set; }

            public Note PrevFlick { get; internal set; }

            public Note NextSlide { get; internal set; }

            public Note PrevSlide { get; internal set; }

            public Note HoldPair { get; internal set; }

        }

        public static readonly Comparison<Note> TimingThenPositionComparison = (x, y) => {
            var r = TimingComparison(x, y);
            return r == 0 ? TrackPositionComparison(x, y) : r;
        };

        public static readonly Comparison<Note> TimingComparison = (x, y) => {
            if (x == null) {
                throw new ArgumentNullException(nameof(x));
            }
            if (y == null) {
                throw new ArgumentNullException(nameof(y));
            }
            if (x.Equals(y)) {
                return 0;
            }
            if (x.Basic.Bar.StarlightID != y.Basic.Bar.StarlightID) {
                return x.Basic.Bar.Basic.Index.CompareTo(y.Basic.Bar.Basic.Index);
            }
            var r = x.Basic.IndexInGrid.CompareTo(y.Basic.IndexInGrid);
            if (r == 0 && x.Basic.Type != y.Basic.Type && (x.Basic.Type == NoteType.VariantBpm || y.Basic.Type == NoteType.VariantBpm)) {
                // The Variant BPM note is always placed at the end on the same grid line.
                return x.Basic.Type == NoteType.VariantBpm ? 1 : -1;
            } else {
                return r;
            }
        };

        public static readonly Comparison<Note> TrackPositionComparison = (n1, n2) => ((int)n1.Basic.FinishPosition).CompareTo((int)n2.Basic.FinishPosition);

        public static bool IsTypeGaming(NoteType type) {
            switch (type) {
                case NoteType.TapOrFlick:
                case NoteType.Hold:
                case NoteType.Slide:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsTypeSpecial(NoteType type) {
            switch (type) {
                case NoteType.Avatar:
                case NoteType.VariantBpm:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsTypeCgssInternal(NoteType type) {
            switch (type) {
                case NoteType.FeverStart:
                case NoteType.FeverEnd:
                case NoteType.MusicStart:
                case NoteType.MusicEnd:
                case NoteType.NoteCount:
                    return true;
                default:
                    return false;
            }
        }

        public static bool operator >(Note left, Note right) {
            var r = TimingComparison(left, right);
            return r > 0;
        }

        public static bool operator <(Note left, Note right) {
            var r = TimingComparison(left, right);
            return r < 0;
        }

        public override string ToString() {
            return $"Note (ID={StarlightID}, Type={Basic.Type}, Flick={Basic.FlickType}, Row={Basic.IndexInGrid}, Col={Basic.FinishPosition})";
        }

        public sealed class TemporaryProperties {

            internal Guid PrevFlickNoteID { get; set; }

            internal Guid NextFlickNoteID { get; set; }

            internal Guid HoldTargetID { get; set; }

            internal Guid PrevSlideNoteID { get; set; }

            internal Guid NextSlideNoteID { get; set; }

            public TimeSpan HitTiming { get; internal set; }

            internal bool EditorVisible { get; set; }

        }

        public TemporaryProperties Temporary { get; } = new TemporaryProperties();

    }
}
