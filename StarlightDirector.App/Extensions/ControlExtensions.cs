using System.Windows.Forms;

namespace StarlightDirector.App.Extensions {
    internal static class ControlExtensions {

        public static Control FindFocusedControl(this Control baseControl) {
            var container = baseControl as IContainerControl;
            while (container != null) {
                baseControl = container.ActiveControl;
                container = baseControl as IContainerControl;
            }
            return baseControl;
        }

    }
}
