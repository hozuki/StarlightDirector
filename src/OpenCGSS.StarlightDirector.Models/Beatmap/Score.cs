using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.Models.Editor;

namespace OpenCGSS.StarlightDirector.Models.Beatmap {
    public sealed class Score {

        public Score([NotNull] Project project, Difficulty difficulty) {
            Project = project;
            Difficulty = difficulty;
        }

        public Difficulty Difficulty { get; internal set; }

        [NotNull]
        public Project Project { get; }

        [NotNull, ItemNotNull]
        public List<Bar> Bars { get; } = new List<Bar>();

        [NotNull, ItemNotNull]
        public IReadOnlyList<Note> GetAllNotes() {
            return Bars.SelectMany(measure => measure.Notes).ToArray();
        }

        public bool HasAnyNote {
            get {
                return Bars.Count != 0 && Bars.Any(bar => bar.Notes.Count > 0);
            }
        }

        public bool HasAnyBar => Bars.Count != 0;

    }
}
