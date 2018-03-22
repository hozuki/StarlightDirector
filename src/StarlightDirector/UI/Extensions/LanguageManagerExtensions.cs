using System.Windows.Forms;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.Globalization;

namespace OpenCGSS.StarlightDirector.UI.Extensions {
    public static class LanguageManagerExtensions {

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] Form form, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            form.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] Button button, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            button.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] Label label, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            label.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] CheckBox checkBox, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            checkBox.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] RadioButton radioButton, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            radioButton.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] ToolStripMenuItem menuItem, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            menuItem.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] ToolStripLabel label, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            label.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] ToolStripButton button, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            button.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] ToolStripSplitButton button, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            button.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] ToolStripDropDownButton button, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            button.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] GroupBox groupBox, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            groupBox.Text = value;
        }

        public static void ApplyText([NotNull] this LanguageManager manager, [NotNull] TabPage tab, [NotNull] string key) {
            var value = manager.GetString(key, null);
            if (value == null) {
                return;
            }
            tab.Text = value;
        }

    }
}
