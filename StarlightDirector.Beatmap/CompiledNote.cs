namespace StarlightDirector.Beatmap {
    public sealed class CompiledNote {

        public int ID { get; set; }

        public NoteType Type { get; set; }

        public double HitTiming { get; set; }

        public NotePosition StartPosition { get; set; }

        public NotePosition FinishPosition { get; set; }

        public NoteFlickType FlickType { get; set; }

        public bool IsSync { get; set; }

        public int GroupID { get; set; }

    }
}
