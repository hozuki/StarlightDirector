using System.IO;

namespace StarlightDirector.Beatmap.IO {
    public abstract class ScoreWriter {

        public abstract void WriteScore(Score score, Stream outputStream);

    }
}
