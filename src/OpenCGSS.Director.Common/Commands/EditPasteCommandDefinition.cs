using System.ComponentModel.Composition;
using System.Windows.Input;
using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Common.Commands {
    [CommandDefinition]
    public sealed class EditPasteCommandDefinition : CommandDefinition {

        public override string Name => "Edit.Paste";

        public override string Text => "_Paste";

        public override string ToolTip => "Paste cut/copied contents.";

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<EditPasteCommandDefinition>(new KeyGesture(Key.V, ModifierKeys.Control));

    }
}
