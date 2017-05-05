using System.Diagnostics;

namespace StarlightDirector.Core {
    public static class MathUtils {

        [DebuggerStepThrough]
        public static float Clamp(this float v, float min, float max) {
            return v < min ? min : (v > max ? max : v);
        }

        [DebuggerStepThrough]
        public static int Clamp(this int v, int min, int max) {
            return v < min ? min : (v > max ? max : v);
        }

        [DebuggerStepThrough]
        public static double BpmToInterval(double bpm) {
            return 60 / bpm;
        }

    }
}
