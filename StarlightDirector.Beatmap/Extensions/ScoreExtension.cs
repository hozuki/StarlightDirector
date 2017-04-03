using System;
using System.Linq;

namespace StarlightDirector.Beatmap.Extensions {
    public static class ScoreExtension {

        public static CompiledScore Compile(this Score score) {
            throw new NotImplementedException();
        }

        public static Note FindNoteByID(this Score score, Guid id) {
            return score.Bars.SelectMany(bar => bar.Notes).FirstOrDefault(note => note.StarlightID == id);
        }

    }
}
