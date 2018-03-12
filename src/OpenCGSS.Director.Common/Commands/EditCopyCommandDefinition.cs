using System.ComponentModel.Composition;
using System.Windows.Input;
using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Common.Commands {
    [CommandDefinition]
    public sealed class EditCopyCommandDefinition : CommandDefinition {

        public override string Name => "Edit.Copy";

        public override string Text => "_Copy";

        public override string ToolTip => "Copy selected contents.";

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<EditCopyCommandDefinition>(new KeyGesture(Key.C, ModifierKeys.Control));

    }
}
