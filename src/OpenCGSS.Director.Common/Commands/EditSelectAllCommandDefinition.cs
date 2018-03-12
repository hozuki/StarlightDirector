using System.ComponentModel.Composition;
using System.Windows.Input;
using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Common.Commands {
    [CommandDefinition]
    public sealed class EditSelectAllCommandDefinition : CommandDefinition {

        public override string Name => "Edit.SelectAll";

        public override string Text => "Select _All";

        public override string ToolTip => "Select all contents.";

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<EditSelectAllCommandDefinition>(new KeyGesture(Key.A, ModifierKeys.Control));

    }
}
