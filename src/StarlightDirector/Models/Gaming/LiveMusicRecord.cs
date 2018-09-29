namespace OpenCGSS.StarlightDirector.Models.Gaming {
    internal sealed class LiveMusicRecord {

        public int LiveID { get; set; }
        public int MusicID { get; set; }
        public string MusicName { get; set; }
        public bool[] DifficultyExists { get; internal set; }
        public MusicColor Attribute { get; set; }

    }
}
