using Gemini.Framework;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Shell.Submodules {
    public interface ITabsRecorder {

        [CanBeNull]
        IDocument LastDocument { get; }

    }
}
