using System;
using System.Windows.Forms;
using StarlightDirector.Core.Interop;

namespace StarlightDirector.App.UI {
    internal static class UIUtilities {

        public static int SetAdvancedTheme(Control control) {
            try {
                var r = NativeMethods.SetWindowTheme(control.Handle, "Explorer", null);
                return r;
            } catch (EntryPointNotFoundException) {
                return -1;
            }
        }

    }
}
