using System.Collections.Generic;
using System.Linq;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap {
    public sealed class Score {

        public Difficulty Difficulty { get; set; }

        public InternalList<Bar> Bars { get; } = new InternalList<Bar>();

        public IReadOnlyList<Note> GetAllNotes() {
            return Bars.SelectMany(measure => measure.Notes).ToArray();
        }

        public bool HasAnyNote {
            get {
                return Bars.Count != 0 && Bars.Any(bar => bar.Notes.Count > 0);
            }
        }

        public bool HasAnyBar => Bars.Count != 0;

        public ScoreSettings Settings { get; } = ScoreSettings.CreateDefault();

    }
}
