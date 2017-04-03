using System;
using System.Collections.Generic;
using System.IO;

namespace StarlightDirector.Beatmap.IO {
    public sealed class Project {

        public Project() {
            var scores = Scores = new Dictionary<Difficulty, Score>();
            for (var i = Difficulty.Debut; i < Difficulty.MasterPlus; ++i) {
                scores.Add(i, new Score());
            }
            Settings = new ScoreSettings();
        }

        public Dictionary<Difficulty, Score> Scores { get; }

        public ScoreSettings Settings { get; }

        public HashSet<Guid> ExistingIDs { get; } = new HashSet<Guid>();

        public string MusicFileName { get; set; } = string.Empty;

        public bool HasMusic => !string.IsNullOrEmpty(MusicFileName);

        public string SaveFileName { get; set; } = string.Empty;

        internal bool IsChanged { get; set; }

        internal bool IsSaved => !string.IsNullOrEmpty(SaveFileName) && File.Exists(SaveFileName);

        public int Version { get; internal set; } = CurrentDefaultVersion;

        public static int CurrentDefaultVersion => 301;

    }
}
