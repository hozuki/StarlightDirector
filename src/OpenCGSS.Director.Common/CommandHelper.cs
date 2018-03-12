using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Common {
    public static class CommandHelper {

        [NotNull]
        public static RoutedCommand RegisterCommand([NotNull] Type ownerType) {
            return RegisterCommand(null, ownerType);
        }

        [NotNull]
        public static RoutedCommand RegisterCommand([NotNull] Type ownerType, [NotNull, ItemNotNull] string[] gestures) {
            return RegisterCommand(null, ownerType, gestures);
        }

        [NotNull]
        public static RoutedCommand RegisterCommand([CanBeNull] string commandName, [NotNull] Type ownerType) {
            if (string.IsNullOrWhiteSpace(commandName)) {
                commandName = Guid.NewGuid().ToString();
            }

            var command = new RoutedCommand(commandName, ownerType);

            return command;
        }

        [NotNull]
        public static RoutedCommand RegisterCommand([CanBeNull] string commandName, [NotNull] Type ownerType, [NotNull, ItemNotNull] params string[] gestures) {
            var command = RegisterCommand(commandName, ownerType);

            if (gestures.Length <= 0) {
                return command;
            }

            foreach (var gesture in gestures) {
                Key key;
                var modifierKeys = ModifierKeys.None;
                var parts = gesture.Split('+');

                if (parts.Length > 1) {
                    foreach (var part in parts.Take(parts.Length - 1)) {
                        var lowerCasePart = part.ToLowerInvariant();
                        switch (lowerCasePart) {
                            case "ctrl":
                                modifierKeys |= ModifierKeys.Control;
                                break;
                            case "win":
                                modifierKeys |= ModifierKeys.Windows;
                                break;
                            default:
                                var mod = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), lowerCasePart, true);
                                modifierKeys |= mod;
                                break;
                        }
                    }
                }

                var lastPart = parts[parts.Length - 1];

                if (uint.TryParse(lastPart, out var dummy) && dummy <= 9) {
                    key = (Key)((int)Key.D0 + dummy);
                } else {
                    key = (Key)Enum.Parse(typeof(Key), lastPart, true);
                }

                command.InputGestures.Add(new KeyGesture(key, modifierKeys));
            }

            return command;
        }

        public static void InitializeCommandBindings([NotNull] FrameworkElement element) {
            var commandBindings = element.CommandBindings;

            var thisType = element.GetType();
            var icommandType = typeof(ICommand);
            var commandFields = thisType.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var commandField in commandFields) {
                if (commandField.FieldType != icommandType && !commandField.FieldType.IsSubclassOf(icommandType)) {
                    continue;
                }

                var command = (ICommand)commandField.GetValue(null);
                var name = commandField.Name;

                var executedHandlerInfo = thisType.GetMethod(name + "_Executed", BindingFlags.NonPublic | BindingFlags.Instance);
                ExecutedRoutedEventHandler executedHandler;

                if (executedHandlerInfo != null) {
                    executedHandler = (ExecutedRoutedEventHandler)Delegate.CreateDelegate(typeof(ExecutedRoutedEventHandler), element, executedHandlerInfo);
                } else {
                    Debug.Print("{0}_Executed() not found. Ignoring.", name);

                    executedHandler = null;
                }

                var canExecuteHandlerInfo = thisType.GetMethod(name + "_CanExecute", BindingFlags.NonPublic | BindingFlags.Instance);
                CanExecuteRoutedEventHandler canExecuteHandler;

                if (canExecuteHandlerInfo != null) {
                    canExecuteHandler = (CanExecuteRoutedEventHandler)Delegate.CreateDelegate(typeof(CanExecuteRoutedEventHandler), element, canExecuteHandlerInfo);
                } else {
                    Debug.Print("{0}_CanExecute() not found. Ignoring.", name);

                    canExecuteHandler = null;
                }

                commandBindings.Add(new CommandBinding(command, executedHandler, canExecuteHandler));
            }
        }

    }
}
