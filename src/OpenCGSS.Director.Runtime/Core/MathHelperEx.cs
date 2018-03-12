using System.Diagnostics;

namespace OpenCGSS.Director.Core {
    public static class MathHelperEx {

        [DebuggerStepThrough]
        public static double BpmToInterval(double bpm) {
            return 60 / bpm;
        }

    }
}
