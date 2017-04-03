﻿using System;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap {
    public sealed class Bar : IStarlightObject {

        public Bar(Score score, int index)
            : this(score, index, Guid.NewGuid()) {
        }

        public Bar(Score score, int index, Guid id) {
            StarlightID = id;
            Index = index;
            Score = score;
        }

        public Score Score { get; }

        public int Index { get; internal set; }

        public BarParams Params { get; internal set; }

        public InternalList<Note> Notes { get; } = new InternalList<Note>();

        public Guid StarlightID { get; internal set; }

    }
}
