using System.ComponentModel.Composition;
using System.Windows.Input;
using Gemini.Framework.Commands;

namespace OpenCGSS.Director.Shell.Commands {
    [CommandDefinition]
    public sealed class SwitchBetweenTabsCommandDefinition : CommandDefinition {

        public override string Name => "Window.SwitchBetweenTabs";

        public override string Text => "Switch Between Tabs";

        public override string ToolTip => "Swtich between two lastly used tabs.";

        [Export]
        public static CommandKeyboardShortcut KeyGesture = new CommandKeyboardShortcut<SwitchBetweenTabsCommandDefinition>(new KeyGesture(Key.Tab, ModifierKeys.Control));

    }
}
