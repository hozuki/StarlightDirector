using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JetBrains.Annotations;
using OpenCGSS.Director.Core;

namespace OpenCGSS.Director.Input {
    public static class CommandManager {

        public static void UpdateCommandListStatus() {
            foreach (var command in CreatedCommands) {
                command.UpdateCanExecute();
                command.UpdateCanRevert();
                command.UpdateRecordToHistory();
            }
        }

        [NotNull]
        public static Command CreateCommand() {
            return CreateCommand(Keys.None);
        }

        [NotNull]
        public static Command CreateCommand(Keys shortcut) {
            var command = new Command {
                ShortcutKeys = shortcut
            };

            CreatedCommands.Add(command);

            return command;
        }

        [NotNull]
        public static Command CreateCommand(Shortcut shortcut) {
            return CreateCommand(ShortcutMapper.Map(shortcut));
        }

        [NotNull]
        public static Command CreateCommand([NotNull] string shortcut) {
            var k = ShortcutMapper.ParseShortcutKeys(shortcut);

            return CreateCommand(k);
        }

        public static Stack<Command> CommandStack { get; } = new Stack<Command>();

        public static void HookForm([NotNull] Form form) {
            Guard.ArgumentNotNull(form, nameof(form));

            form.Activated += OnUpdateCommandListStatus;
            form.Deactivate += OnUpdateCommandListStatus;
        }

        public static void UnhookForm([NotNull] Form form) {
            Guard.ArgumentNotNull(form, nameof(form));

            form.Activated -= OnUpdateCommandListStatus;
            form.Deactivate -= OnUpdateCommandListStatus;
        }

        private static void OnUpdateCommandListStatus([NotNull] object sender, [NotNull] EventArgs eventArgs) {
            UpdateCommandListStatus();
        }

        internal static readonly Dictionary<Component, Command> RegisteredCommandPairs = new Dictionary<Component, Command>();

        private static readonly List<Command> CreatedCommands = new List<Command>();

    }
}
