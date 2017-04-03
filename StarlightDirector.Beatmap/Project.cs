using System;
using System.Collections.Generic;
using System.IO;

namespace StarlightDirector.Beatmap {
    public sealed class Project {

        public Project() {
            var scores = Scores = new Dictionary<Difficulty, Score>();
            for (var i = Difficulty.Debut; i < Difficulty.MasterPlus; ++i) {
                scores.Add(i, new Score(this, i));
            }
            Settings = new ScoreSettings();
        }

        public Dictionary<Difficulty, Score> Scores { get; }

        public ScoreSettings Settings { get; }

        public string MusicFileName { get; set; } = string.Empty;

        public bool HasMusic => !string.IsNullOrEmpty(MusicFileName);

        public string SaveFileName { get; set; } = string.Empty;

        public int Version { get; internal set; }

        public HashSet<Guid> UsedNoteIDs { get; } = new HashSet<Guid>();

        internal bool IsChanged { get; set; }

        internal bool IsSaved => !string.IsNullOrEmpty(SaveFileName) && File.Exists(SaveFileName);

    }
}
