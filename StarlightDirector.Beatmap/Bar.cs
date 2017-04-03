using System;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap {
    public sealed class Bar : IStarlightObject {

        public Bar() {
            StarlightID = Guid.NewGuid();
        }

        public InternalList<Note> Notes { get; } = new InternalList<Note>();

        public Guid StarlightID { get; internal set; }

    }
}
