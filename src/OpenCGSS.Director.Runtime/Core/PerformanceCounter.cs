using System;
using System.ComponentModel;
using OpenCGSS.Director.Core.Interop;

namespace OpenCGSS.Director.Core {
    public static class PerformanceCounter {

        public static long GetCurrent() {
            var r = NativeMethods.QueryPerformanceCounter(out var result);

            if (!r) {
                throw new Win32Exception("QueryPerformanceCounter is not supported.");
            }

            return result;
        }

        // Result is in milliseconds.
        public static TimeSpan GetDuration(long start, long stop) {
            if (_frequency == 0) {
                var r = NativeMethods.QueryPerformanceFrequency(out _frequency);

                if (!r) {
                    throw new Win32Exception("QueryPerformanceFrequency is not supported.");
                }
            }

            var duration = stop - start;
            var milliSec = duration * Multiplier / _frequency;

            return TimeSpan.FromMilliseconds(milliSec);
        }

        private static long _frequency = 0;
        private const double Multiplier = 1e3;

    }
}
