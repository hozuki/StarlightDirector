using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.Input {
    public static class CommandExtensions {

        public static void SetParameter([CanBeNull] this ButtonBase button, [CanBeNull] object parameter) {
            SetParameter(button as Component, parameter);
        }

        public static void DeleteParameter([CanBeNull] this ButtonBase button) {
            DeleteParameter(button as Component);
        }

        [CanBeNull]
        public static object GetParameter([CanBeNull] this ButtonBase button) {
            return GetParameter(button as Component);
        }

        public static bool DefinedParameter([CanBeNull] this ButtonBase button) {
            return DefinedParameter(button as Component);
        }

        public static void Bind([CanBeNull] this ButtonBase button, [CanBeNull] Command command) {
            Bind(button as Component, command, false);
        }

        public static void SetParameter([CanBeNull] this MenuItem menuItem, [CanBeNull] object parameter) {
            SetParameter(menuItem as Component, parameter);
        }

        public static void DeleteParameter([CanBeNull] this MenuItem menuItem) {
            DeleteParameter(menuItem as Component);
        }

        [CanBeNull]
        public static object GetParameter([CanBeNull] this MenuItem menuItem) {
            return GetParameter(menuItem as Component);
        }

        public static bool DefinedParameter([CanBeNull] this MenuItem menuItem) {
            return DefinedParameter(menuItem as Component);
        }

        public static void Bind([CanBeNull] this MenuItem menuItem, [CanBeNull] Command command, bool setShortcut = true) {
            Bind(menuItem as Component, command, setShortcut);
        }

        public static void SetParameter([CanBeNull] this ToolStripButton button, [CanBeNull] object parameter) {
            SetParameter(button as Component, parameter);
        }

        public static void DeleteParameter([CanBeNull] this ToolStripButton button) {
            DeleteParameter(button as Component);
        }

        [CanBeNull]
        public static object GetParameter([CanBeNull] this ToolStripButton button) {
            return GetParameter(button as Component);
        }

        public static bool DefinedParameter([CanBeNull] this ToolStripButton button) {
            return DefinedParameter(button as Component);
        }

        public static void Bind([CanBeNull] this ToolStripButton button, [CanBeNull] Command command, bool setShortcut = true) {
            Bind(button as Component, command, setShortcut);
        }

        public static void SetParameter([CanBeNull] this ToolStripSplitButton button, [CanBeNull] object parameter) {
            SetParameter(button as Component, parameter);
        }

        public static void DeleteParameter([CanBeNull] this ToolStripSplitButton button) {
            DeleteParameter(button as Component);
        }

        [CanBeNull]
        public static object GetParameter([CanBeNull] this ToolStripSplitButton button) {
            return GetParameter(button as Component);
        }

        public static bool DefinedParameter([CanBeNull] this ToolStripSplitButton button) {
            return DefinedParameter(button as Component);
        }

        public static void Bind([CanBeNull] this ToolStripSplitButton button, [CanBeNull] Command command, bool setShortcut = true) {
            Bind(button as Component, command, setShortcut);
        }

        public static void SetParameter([CanBeNull] this ToolStripOverflowButton button, [CanBeNull] object parameter) {
            SetParameter(button as Component, parameter);
        }

        public static void DeleteParameter([CanBeNull] this ToolStripOverflowButton button) {
            DeleteParameter(button as Component);
        }

        [CanBeNull]
        public static object GetParameter([CanBeNull] this ToolStripOverflowButton button) {
            return GetParameter(button as Component);
        }

        public static bool DefinedParameter([CanBeNull] this ToolStripOverflowButton button) {
            return DefinedParameter(button as Component);
        }

        public static void Bind([CanBeNull] this ToolStripOverflowButton button, [CanBeNull] Command command, bool setShortcut = true) {
            Bind(button as Component, command, setShortcut);
        }

        public static void SetParameter([CanBeNull] this ToolStripMenuItem menuItem, [CanBeNull] object parameter) {
            SetParameter(menuItem as Component, parameter);
        }

        public static void DeleteParameter([CanBeNull] this ToolStripMenuItem menuItem) {
            DeleteParameter(menuItem as Component);
        }

        [CanBeNull]
        public static object GetParameter([CanBeNull] this ToolStripMenuItem menuItem) {
            return GetParameter(menuItem as Component);
        }

        public static bool DefinedParameter([CanBeNull] this ToolStripMenuItem menuItem) {
            return DefinedParameter(menuItem as Component);
        }

        public static void Bind([CanBeNull] this ToolStripMenuItem menuItem, [CanBeNull] Command command, bool setShortcut = true) {
            Bind(menuItem as Component, command, setShortcut);
        }

        internal static void SetParameter([CanBeNull] this Component component, [CanBeNull] object parameter) {
            if (component == null) {
                return;
            }
            CommandParameters[component] = parameter;
            component.Disposed += ComponentOnDispose;
        }

        internal static void DeleteParameter([CanBeNull] this Component component) {
            if (component == null) {
                return;
            }

            CommandParameters.Remove(component);
            component.Disposed -= ComponentOnDispose;
        }

        [CanBeNull]
        internal static object GetParameter([CanBeNull] this Component component) {
            if (component == null) {
                return null;
            }

            return CommandParameters.ContainsKey(component) ? CommandParameters[component] : null;
        }

        internal static bool DefinedParameter([CanBeNull] this Component component) {
            if (component == null) {
                return false;
            }

            return CommandParameters.ContainsKey(component);
        }

        internal static void Bind([CanBeNull] this Component component, [CanBeNull] Command command, bool setShortcut) {
            if (component == null) {
                return;
            }

            var reg = CommandManager.RegisteredCommandPairs;

            if (reg.ContainsKey(component)) {
                reg[component].UnsubscribeControl(component);
            }

            if (command != null) {
                command.SubscribeControl(component, setShortcut);
                reg[component] = command;
            } else {
                if (reg.ContainsKey(component)) {
                    reg.Remove(component);
                }
            }
        }

        private static void ComponentOnDispose([NotNull] object sender, [NotNull] EventArgs e) {
            if (!(sender is Component comp)) {
                return;
            }

            DeleteParameter(comp);
        }

        [NotNull]
        private static readonly Dictionary<Component, object> CommandParameters = new Dictionary<Component, object>();

    }
}
