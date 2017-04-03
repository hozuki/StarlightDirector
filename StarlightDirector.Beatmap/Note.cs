using System;
using System.Diagnostics;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap {
    public sealed class Note : IStarlightObject {

        public Note() {
            Basic = new NoteBasicProperties();
            Helper = new NoteHelperProperties(this);
            Editor = new NoteEditorProperties();
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

        public sealed class NoteBasicProperties {

            internal NoteBasicProperties() {
            }

            public Guid ID { get; internal set; }

            public NoteType Type { get; set; }

            public double HitTiming { get; set; }

            public NotePosition StartPosition { get; set; }

            public NotePosition FinishPosition { get; set; }

            public NoteFlickType FlickType { get; set; }

        }

        public sealed class NoteHelperProperties {

            internal NoteHelperProperties(Note note) {
                _note = note;
            }

            public bool IsTap {
                [DebuggerStepThrough]
                get { return _note.Basic.Type == NoteType.TapOrFlick && _note.Basic.FlickType == NoteFlickType.None; }
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
                get { return HasHold && IsTap; }
            }

            public bool IsHold {
                [DebuggerStepThrough]
                get { return IsHoldStart || IsHoldEnd; }
            }

            public bool HasHold {
                [DebuggerStepThrough]
                get { return _note.Editor.HoldPair != null; }
            }

            public bool IsSlide {
                [DebuggerStepThrough]
                get { return _note.Basic.Type == NoteType.Slide && _note.Basic.FlickType == NoteFlickType.None; }
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

            public int BarIndex { get; set; }

            public bool IsSelected { get; set; }

            public Note SyncPair { get; internal set; }

            public Note NextFlick { get; internal set; }

            public Note PrevFlick { get; internal set; }

            public Note NextSlide { get; internal set; }

            public Note PrevSlide { get; internal set; }

            public Note HoldPair { get; internal set; }

        }

    }
}
