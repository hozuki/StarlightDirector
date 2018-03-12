using System.ComponentModel.Composition;
using Gemini.Framework.Menus;
using OpenCGSS.Director.Modules.SldProject.Commands;
using MainMenuDefinitions = Gemini.Modules.MainMenu.MenuDefinitions;
using ShellMenuDefinitions = Gemini.Modules.Shell.MenuDefinitions;
using ToolboxMenuDefinitions = Gemini.Modules.Toolbox.MenuDefinitions;
using CommonMenuDefinitions = OpenCGSS.Director.Common.MenuDefinitions;

namespace OpenCGSS.Director.Modules.SldProject {
    public static class MenuDefinitions {

        [Export]
        public static readonly MenuItemDefinition ViewCgssBeatmapToolsMenuItem = new CommandMenuItemDefinition<ViewCgssBeatmapToolsCommandDefinition>(MainMenuDefinitions.ViewToolsMenuGroup, 3);

        [Export]
        public static readonly ExcludeMenuItemDefinition ExcludeViewToolbox = new ExcludeMenuItemDefinition(ToolboxMenuDefinitions.ViewToolboxMenuItem);

        [Export]
        public static readonly MenuItemDefinition HelpAboutSldProjPluginMenuItem = new CommandMenuItemDefinition<AboutSldProjPluginCommandDefinition>(CommonMenuDefinitions.HelpAboutPluginsGroup, 0);

    }
}
