using System;
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
        public static double RoundDigits(this double v, int digits) {
            if (digits < 0) {
                return v;
            }
            var pow = Math.Pow(10, digits);
            var newValue = (int)(v * pow);
            return newValue / pow;
        }

        [DebuggerStepThrough]
        public static double RoundDigits(this float v, int digits) {
            if (digits < 0) {
                return v;
            }
            var pow = Math.Pow(10, digits);
            var newValue = (int)(v * pow);
            return newValue / pow;
        }

    }
}
