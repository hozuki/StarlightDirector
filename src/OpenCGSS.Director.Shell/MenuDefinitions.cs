using System.ComponentModel.Composition;
using Gemini.Framework.Menus;
using OpenCGSS.Director.Shell.Commands;
using MainMenuDefinitions = Gemini.Modules.MainMenu.MenuDefinitions;
using ShellMenuDefinitions = Gemini.Modules.Shell.MenuDefinitions;

namespace OpenCGSS.Director.Shell {
    public static class MenuDefinitions {

        [Export]
        public static readonly MenuItemGroupDefinition WindowCommandsGroup = new MenuItemGroupDefinition(MainMenuDefinitions.WindowMenu, 1);

        [Export]
        public static readonly MenuItemDefinition WindowSwitchBetweenTabs = new CommandMenuItemDefinition<SwitchBetweenTabsCommandDefinition>(WindowCommandsGroup, 0);

    }
}
