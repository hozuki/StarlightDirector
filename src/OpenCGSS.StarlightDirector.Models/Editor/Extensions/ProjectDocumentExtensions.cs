using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Models.Editor.Extensions {
    public static class ProjectDocumentExtensions {

        public static void MarkAsDirty([NotNull] this ProjectDocument document) {
            Guard.ArgumentNotNull(document, nameof(document));

            document.IsModified = true;
        }

        public static void MarkAsClean([NotNull] this ProjectDocument document) {
            Guard.ArgumentNotNull(document, nameof(document));

            document.IsModified = false;
        }

        public static bool WasSaved([NotNull] this ProjectDocument document) {
            Guard.ArgumentNotNull(document, nameof(document));

            return !string.IsNullOrWhiteSpace(document.SaveFilePath);
        }

    }
}
