using System;
using System.Diagnostics;
using System.Linq;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap.Extensions {
    public static class ScoreExtension {

        public static CompiledScore Compile(this Score score) {
            throw new NotImplementedException();
        }

        [DebuggerStepThrough]
        public static Bar AppendBar(this Score score) {
            return AppendBar(score, Guid.NewGuid());
        }

        [DebuggerStepThrough]
        public static Bar AppendBar(this Score score, Guid id) {
            var bar = new Bar(score, score.Bars.Count, id);
            score.Bars.Add(bar);
            return bar;
        }

        [DebuggerStepThrough]
        public static Bar AppendBar(this Score score, int id) {
            var guid = StarlightID.GetGuidFromInt32(id);
            return AppendBar(score, guid);
        }

        [DebuggerStepThrough]
        public static Note FindNoteByID(this Score score, Guid id) {
            return score.Bars.SelectMany(bar => bar.Notes).FirstOrDefault(note => note.StarlightID == id);
        }

    }
}
