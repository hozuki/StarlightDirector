using Gemini.Framework;

namespace OpenCGSS.Director.Common.ViewModels {
    public interface IBeatmapDocument : IPersistedDocument {

        void Cut();

        void Copy();

        void Paste();

        void SelectAll();

        void ViewProperties();

        void ZoomIn();

        void ZoomOut();

        bool CanPerformCut { get; }

        bool CanPerformCopy { get; }

        bool CanPerformPaste { get; }

        bool CanPerformSelectAll { get; }

        bool CanPerformViewProperties { get; }

        bool CanPerformZoomIn { get; }

        bool CanPerformZoomOut { get; }

    }
}
