using System.Diagnostics;

namespace OpenCGSS.Director.Modules.SldProject.Core {
    public static class SldMathHelper {

        [DebuggerStepThrough]
        public static double BpmToInterval(double bpm) {
            return 60 / bpm;
        }

    }
}
