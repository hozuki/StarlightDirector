using JetBrains.Annotations;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap;

namespace OpenCGSS.Director.Modules.SldProject.IO {
    public abstract class ProjectWriter {

        /// <summary>
        /// Writes a <see cref="Project"/> to a persistence file.
        /// </summary>
        /// <param name="project">The project to save.</param>
        /// <param name="fileName">Path to the save file.</param>
        public abstract void WriteProject([NotNull] Project project, [NotNull] string fileName);

        [NotNull]
        public static ProjectWriter CreateLatest() {
            return new SldprojV4Writer();
        }

    }
}
