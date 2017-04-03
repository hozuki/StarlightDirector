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
            var command = new Command();
            CreatedCommands.Add(command);
            return command;
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
