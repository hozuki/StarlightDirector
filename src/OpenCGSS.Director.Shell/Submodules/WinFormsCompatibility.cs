using System.ComponentModel.Composition;
using System.Windows.Forms;
using System.Windows.Interop;
using Gemini.Framework;

namespace OpenCGSS.Director.Shell.Submodules {
    [Export(typeof(IModule))]
    public sealed class WinFormsCompatibility : ModuleBase {

        public override void Initialize() {
            // https://stackoverflow.com/questions/2344398/application-idle-event-not-firing-in-wpf-application
            ComponentDispatcher.ThreadIdle += (s, e) => { Application.RaiseIdle(e); };
        }

    }
}
