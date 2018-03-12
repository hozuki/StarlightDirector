using System.Collections.Generic;
using System.Linq;

namespace StarlightDirector.Beatmap {
    public sealed class CompiledScore {

        public CompiledScore(IEnumerable<CompiledNote> notes) {
            Notes = notes.ToArray();
        }

        public IReadOnlyList<CompiledNote> Notes { get; }

    }
}
