using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Models.Editor {
    public sealed class ProjectDocument {

        public ProjectDocument() {
            Project = new Project();
        }

        public ProjectDocument([NotNull] Project project, [CanBeNull] string filePath) {
            Guard.ArgumentNotNull(project, nameof(project));

            Project = project;
            SaveFilePath = filePath;
        }

        [NotNull]
        public Project Project { get; }

        public bool IsModified { get; internal set; }

        [CanBeNull]
        public string SaveFilePath { get; set; }

    }
}

