using System.Windows.Forms;
using StarlightDirector.Core;

namespace StarlightDirector.App.Extensions {
    public static class LanguageManagerExtensions {

        public static void ApplyText(this LanguageManager manager, ToolStripMenuItem menuItem, string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            menuItem.Text = value;
        }

        public static void ApplyText(this LanguageManager manager, ToolStripLabel label, string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            label.Text = value;
        }

        public static void ApplyText(this LanguageManager manager, ToolStripButton button, string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            button.Text = value;
        }

        public static void ApplyText(this LanguageManager manager, ToolStripSplitButton button, string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            button.Text = value;
        }

        public static void ApplyText(this LanguageManager manager, ToolStripDropDownButton button, string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            button.Text = value;
        }

        public static void ApplyText(this LanguageManager manager, GroupBox groupBox, string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            groupBox.Text = value;
        }

    }
}
