using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace OpenCGSS.StarlightDirector.Models {
    public static class BeatmapMathHelper {

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double BpmToInterval(double bpm) {
            return 60 / bpm;
        }

    }
}
