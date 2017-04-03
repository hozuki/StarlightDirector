using System.Collections.Generic;
using System.Linq;
using StarlightDirector.Core;

namespace StarlightDirector.Beatmap {
    public sealed class Score {

        public Score(Project project, Difficulty difficulty) {
            Project = project;
            Difficulty = difficulty;
        }

        public Difficulty Difficulty { get; }

        public Project Project { get; }

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
