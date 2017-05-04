using System.Windows.Forms;

namespace StarlightDirector.App.Extensions {
    internal static class ControlExtensions {

        public static Control FindFocusedControl(this Control baseControl) {
            var container = baseControl as IContainerControl;
            if (container == null) {
                return baseControl;
            }

            do {
                var active = container.ActiveControl;
                if (active == null) {
                    break;
                }
                baseControl = active;
                container = active as IContainerControl;
            } while (container != null);

            return baseControl;
        }

    }
}
