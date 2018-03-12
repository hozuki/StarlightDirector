using System.ComponentModel.Composition;
using System.Windows.Input;
using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Common.Commands {
    [CommandDefinition]
    public sealed class EditCutCommandDefinition : CommandDefinition {

        public override string Name => "Edit.Cut";

        public override string Text => "Cu_t";

        public override string ToolTip => "Cut selected contents.";

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<EditCutCommandDefinition>(new KeyGesture(Key.X, ModifierKeys.Control));

    }
}
