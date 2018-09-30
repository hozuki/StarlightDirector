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
            if (_noteCountChanged || _allNotesCache == null) {
                _allNotesCache = Bars.SelectMany(measure => measure.Notes).ToArray();
                _noteCountChanged = false;
            }

            return _allNotesCache;
        }

        public bool HasAnyNote => Bars.Count != 0 && Bars.Any(bar => bar.Notes.Count > 0);

        public bool HasAnyBar => Bars.Count != 0;

        internal void InformNoteCountChanged() {
            _noteCountChanged = true;
        }

        private bool _noteCountChanged = true;
        [CanBeNull]
        private Note[] _allNotesCache;

    }
}
