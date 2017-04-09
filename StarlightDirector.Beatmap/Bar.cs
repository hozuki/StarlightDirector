using System;
using System.Diagnostics;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap {
    public sealed class Bar : IStarlightObject {

        internal Bar(Score score, int index)
            : this(score, index, Guid.NewGuid()) {
        }

        internal Bar(Score score, int index, Guid id) {
            StarlightID = id;
            Index = index;
            Score = score;
        }

        public Score Score { get; }

        public int Index { get; internal set; }

        public BarParams Params { get; internal set; }

        public bool IsSelected { get; set; }

        public InternalList<Note> Notes { get; } = new InternalList<Note>();

        public bool HasAnyNote {
            [DebuggerStepThrough]
            get { return Notes.Count > 0; }
        }

        public Guid StarlightID { get; internal set; }

    }
}
