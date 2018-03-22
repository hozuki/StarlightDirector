using System.Runtime.InteropServices;
using OpenCGSS.StarlightDirector.Interop;

namespace OpenCGSS.StarlightDirector.UI {
    internal static class ScreenHelper {

        public static int GetCurrentRefreshRate() {
            var devmode = new DEVMODE();

            devmode.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));

            var succeeded = NativeMethods.EnumDisplaySettings(null, NativeConstants.ENUM_CURRENT_SETTINGS, ref devmode);

            if (!succeeded) {
                return 0;
            }

            return devmode.dmDisplayFrequency;
        }

    }
}
