using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Input;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.UI {
    internal static class CommandHelper {

        [NotNull]
        public static CommandBinding CreateUIBinding() {
            var command = new RoutedUICommand();

            return new CommandBinding(command);
        }

        [NotNull]
        public static CommandBinding CreateUIBinding([NotNull] string shortcutKeys) {
            var command = new RoutedUICommand(shortcutKeys);

            return new CommandBinding(command);
        }

        [CanBeNull]
        public static ICommandSource FindCommandSource([NotNull] Component component) {
            CommandSources.TryGetValue(component, out var source);

            return source;
        }

        [NotNull]
        public static ICommandSource BindCommand([NotNull] this ButtonBase control, [NotNull] CommandBinding binding, [CanBeNull] object commandParameter = null) {
            var source = control.CreateCommandSourceBuilder().WithCommandBinding(binding).WithCommandParameter(commandParameter).Build();

            CommandSources[control] = source;

            return source;
        }

        [NotNull]
        public static ICommandSource BindCommand([NotNull] this MenuItem control, [NotNull] CommandBinding binding, [CanBeNull] object commandParameter = null) {
            var source = control.CreateCommandSourceBuilder().WithCommandBinding(binding).WithCommandParameter(commandParameter).Build();

            CommandSources[control] = source;

            return source;
        }

        [NotNull]
        public static ICommandSource BindCommand([NotNull] this ToolStripButton control, [NotNull] CommandBinding binding, [CanBeNull] object commandParameter = null) {
            var source = control.CreateCommandSourceBuilder().WithCommandBinding(binding).WithCommandParameter(commandParameter).Build();

            CommandSources[control] = source;

            return source;
        }

        [NotNull]
        public static ICommandSource BindCommand([NotNull] this ToolStripSplitButton control, [NotNull] CommandBinding binding, [CanBeNull] object commandParameter = null) {
            var source = control.CreateCommandSourceBuilder().WithCommandBinding(binding).WithCommandParameter(commandParameter).Build();

            CommandSources[control] = source;

            return source;
        }

        [NotNull]
        public static ICommandSource BindCommand([NotNull] this ToolStripOverflowButton control, [NotNull] CommandBinding binding, [CanBeNull] object commandParameter = null) {
            var source = control.CreateCommandSourceBuilder().WithCommandBinding(binding).WithCommandParameter(commandParameter).Build();

            CommandSources[control] = source;

            return source;
        }

        [NotNull]
        public static ICommandSource BindCommand([NotNull] this ToolStripMenuItem control, [NotNull] CommandBinding binding, [CanBeNull] object commandParameter = null) {
            var source = control.CreateCommandSourceBuilder().WithCommandBinding(binding).WithCommandParameter(commandParameter).Build();

            CommandSources[control] = source;

            return source;
        }

        internal static void UpdateAllSourceText() {
            foreach (var kv in CommandSources) {
                var source = kv.Value;
                var command = source.Command;

                // Force rebinding. Then the tip text will also be updated.
                source.Command = null;
                source.Command = command;
            }
        }

        // For backward compatibility only
        internal static void SetCommandParameter([NotNull] this ToolStripMenuItem control, [CanBeNull] object parameter) {
            var source = FindCommandSource(control);

            if (source == null) {
                return;
            }

            source.CommandParameter = parameter;
        }

        // For backward compatibility only
        internal static void DeleteCommandParameter([NotNull] this ToolStripMenuItem control) {
            SetCommandParameter(control, null);
        }

        // For backward compatibility only
        [CanBeNull]
        internal static object GetCommandParameter([NotNull] this ToolStripMenuItem control) {
            return FindCommandSource(control)?.CommandParameter;
        }

        private static readonly Dictionary<Component, ICommandSource> CommandSources = new Dictionary<Component, ICommandSource>();

    }
}
