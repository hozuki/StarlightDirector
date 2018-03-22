using System.Windows.Forms;

namespace OpenCGSS.StarlightDirector.UI.Extensions {
    internal static class ToolStripItemExtensions {

        public static bool SafeGetChecked(this ToolStripItem item) {
            switch (item) {
                case ToolStripButton button:
                    return button.Checked;
                case ToolStripMenuItem menuItem:
                    return menuItem.Checked;
                default:
                    return false;
            }
        }

    }
}
