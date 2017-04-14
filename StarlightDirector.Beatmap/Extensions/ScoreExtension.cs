using System;
using System.Collections.Generic;
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
        public static Bar RemoveBar(this Score score, Bar bar) {
            if (!score.Bars.Contains(bar)) {
                throw new ArgumentException("Assigned bar is not in the score.", nameof(bar));
            }
            var barIndex = bar.Index;
            foreach (var b in score.Bars) {
                if (b.Index > barIndex) {
                    --b.Index;
                }
            }
            score.Bars.Remove(bar);
            return bar;
        }

        [DebuggerStepThrough]
        public static void RemoveBars(this Score score, IEnumerable<Bar> bars) {
            var barArr = bars.ToArray();
            foreach (var bar in barArr) {
                if (!score.Bars.Contains(bar)) {
                    throw new ArgumentException("Assigned bar is not in the score.", nameof(bar));
                }
            }
            var barIndices = barArr.Select(b => b.Index).ToArray();
            foreach (var bar in barArr) {
                score.Bars.Remove(bar);
            }
            foreach (var bar in score.Bars) {
                var greaterNum = barIndices.Count(index => bar.Index > index);
                bar.Index -= greaterNum;
            }
        }

        [DebuggerStepThrough]
        public static Note FindNoteByID(this Score score, Guid id) {
            return score.Bars.SelectMany(bar => bar.Notes).FirstOrDefault(note => note.StarlightID == id);
        }

    }
}
