using System;
using System.Windows.Forms;
using OpenCGSS.StarlightDirector.Interop;

namespace OpenCGSS.StarlightDirector.UI.Controls {
    internal static class SystemStyleHelper {

        internal static int SetAdvancedTheme(Control control) {
            try {
                var r = NativeMethods.SetWindowTheme(control.Handle, "Explorer", null);
                return r;
            } catch (EntryPointNotFoundException) {
                return -1;
            }
        }

    }
}
