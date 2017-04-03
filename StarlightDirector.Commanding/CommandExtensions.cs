using System.ComponentModel;
using System.Windows.Forms;

namespace StarlightDirector.Commanding {
    public static class CommandExtensions {

        public static void Attach(this ButtonBase button, Command command) {
            Attach(button as Component, command);
        }

        public static void Attach(this MenuItem menuItem, Command command) {
            Attach(menuItem as Component, command);
        }

        public static void Attach(this ToolStripItem toolStripItem, Command command) {
            Attach(toolStripItem as Component, command);
        }

        private static void Attach(Component component, Command command) {
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

    }
}
