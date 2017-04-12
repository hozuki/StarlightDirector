using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace StarlightDirector.Commanding {
    public static class CommandExtensions {

        public static void SetParameter(this ButtonBase button, object parameter) {
            SetParameter(button as Component, parameter);
        }

        public static void DeleteParameter(this ButtonBase button) {
            DeleteParameter(button as Component);
        }

        public static object GetParameter(this ButtonBase button) {
            return GetParameter(button as Component);
        }

        public static bool DefinedParameter(this ButtonBase button) {
            return DefinedParameter(button as Component);
        }

        public static void Attach(this ButtonBase button, Command command) {
            Attach(button as Component, command);
        }

        public static void SetParameter(this MenuItem menuItem, object parameter) {
            SetParameter(menuItem as Component, parameter);
        }

        public static void DeleteParameter(this MenuItem menuItem) {
            DeleteParameter(menuItem as Component);
        }

        public static object GetParameter(this MenuItem menuItem) {
            return GetParameter(menuItem as Component);
        }

        public static bool DefinedParameter(this MenuItem menuItem) {
            return DefinedParameter(menuItem as Component);
        }

        public static void Attach(this MenuItem menuItem, Command command) {
            Attach(menuItem as Component, command);
        }

        public static void SetParameter(this ToolStripItem toolStripItem, object parameter) {
            SetParameter(toolStripItem as Component, parameter);
        }

        public static void DeleteParameter(this ToolStripItem toolStripItem) {
            DeleteParameter(toolStripItem as Component);
        }

        public static object GetParameter(this ToolStripItem toolStripItem) {
            return GetParameter(toolStripItem as Component);
        }

        public static bool DefinedParameter(this ToolStripItem toolStripItem) {
            return DefinedParameter(toolStripItem as Component);
        }

        public static void Attach(this ToolStripItem toolStripItem, Command command) {
            Attach(toolStripItem as Component, command);
        }

        internal static void SetParameter(this Component component, object parameter) {
            if (component == null) {
                return;
            }
            CommandParameters[component] = parameter;
            component.Disposed += ComponentOnDispose;
        }

        internal static void DeleteParameter(this Component component) {
            if (component == null) {
                return;
            }
            if (CommandParameters.ContainsKey(component)) {
                CommandParameters.Remove(component);
            }
            component.Disposed -= ComponentOnDispose;
        }

        internal static object GetParameter(this Component component) {
            if (component == null) {
                return null;
            }
            return CommandParameters.ContainsKey(component) ? CommandParameters[component] : null;
        }

        internal static bool DefinedParameter(this Component component) {
            if (component == null) {
                return false;
            }
            return CommandParameters.ContainsKey(component);
        }

        internal static void Attach(this Component component, Command command) {
            var reg = CommandManager.RegisteredCommandPairs;
            if (reg.ContainsKey(component)) {
                reg[component].UnsubscribeControl(component);
            }
            if (command != null) {
                command.SubscribeControl(component);
                reg[component] = command;
            } else {
                if (reg.ContainsKey(component)) {
                    reg.Remove(component);
                }
            }
        }

        private static void ComponentOnDispose(object sender, EventArgs e) {
            var comp = sender as Component;
            if (comp == null) {
                return;
            }
            DeleteParameter(comp);
        }

        private static readonly Dictionary<Component, object> CommandParameters = new Dictionary<Component, object>();

    }
}
