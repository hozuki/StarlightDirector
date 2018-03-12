using System.Windows.Forms;
using JetBrains.Annotations;

namespace OpenCGSS.Director.UI.Controls.Extensions {
    internal static class ToolStripItemExtensions {

        internal static bool SafeGetChecked([CanBeNull] this ToolStripItem item) {
            if (item == null) {
                return false;
            }

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
