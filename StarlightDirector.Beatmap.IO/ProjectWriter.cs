using System.IO;

namespace StarlightDirector.Beatmap.IO {
    public abstract class ProjectWriter {

        public abstract void WriteProject(Project project, Stream outputStream);

    }
}
