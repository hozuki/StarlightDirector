using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using OpenCGSS.Director.Modules.SldProject.Core;

namespace OpenCGSS.Director.Modules.SldProject.Models.Beatmap {
    public sealed class Score {

        public Score([NotNull] Project project, Difficulty difficulty) {
            Project = project;
            Difficulty = difficulty;
        }

        public Difficulty Difficulty { get; }

        [NotNull]
        public Project Project { get; }

        [NotNull, ItemNotNull]
        public InternalList<Bar> Bars { get; } = new InternalList<Bar>();

        [NotNull, ItemNotNull]
        public IReadOnlyList<Note> GetAllNotes() {
            return Bars.SelectMany(measure => measure.Notes).ToArray();
        }

        public bool HasAnyNote {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => HasAnyBar && Bars.Any(bar => bar.Notes.Count > 0);
        }

        public bool HasAnyBar {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Bars.Count != 0;
        }

    }
}
