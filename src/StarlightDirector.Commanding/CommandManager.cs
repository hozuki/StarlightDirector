using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace StarlightDirector.Commanding {
    public static class CommandManager {

        public static void UpdateCommandListStatus() {
            foreach (var command in CreatedCommands) {
                command.UpdateCanExecute();
                command.UpdateCanRevert();
                command.UpdateRecordToHistory();
            }
        }

        public static Command CreateCommand() {
            return CreateCommand(Keys.None);
        }

        public static Command CreateCommand(Keys shortcut) {
            var command = new Command {
                ShortcutKeys = shortcut
            };
            CreatedCommands.Add(command);
            return command;
        }

        public static Command CreateCommand(Shortcut shortcut) {
            return CreateCommand(ShortcutMapper.Map(shortcut));
        }

        public static Command CreateCommand(string shortcut) {
            var k = ShortcutMapper.ParseShortcutKeys(shortcut);
            return CreateCommand(k);
        }

        public static Stack<Command> CommandStack { get; } = new Stack<Command>();

        public static void HookForm(Form form) {
            form.Activated += OnUpdateCommandListStatus;
            form.Deactivate += OnUpdateCommandListStatus;
        }

        public static void UnhookForm(Form form) {
            form.Activated -= OnUpdateCommandListStatus;
            form.Deactivate -= OnUpdateCommandListStatus;
        }

        private static void OnUpdateCommandListStatus(object sender, EventArgs eventArgs) {
            UpdateCommandListStatus();
        }

        internal static readonly Dictionary<Component, Command> RegisteredCommandPairs = new Dictionary<Component, Command>();

        private static readonly List<Command> CreatedCommands = new List<Command>();

    }
}
