using System.ComponentModel.Composition;
using Gemini.Framework.Menus;
using OpenCGSS.Director.Common.Commands;
using MainMenuDefinitions = Gemini.Modules.MainMenu.MenuDefinitions;
using ShellMenuDefinitions = Gemini.Modules.Shell.MenuDefinitions;

namespace OpenCGSS.Director.Common {
    public static class MenuDefinitions {

        [Export]
        public static readonly MenuItemGroupDefinition EditExtendedDocumentOperationGroup = new MenuItemGroupDefinition(MainMenuDefinitions.EditMenu, 1);

        [Export]
        public static readonly MenuItemDefinition EditCutMenuItem = new CommandMenuItemDefinition<EditCutCommandDefinition>(EditExtendedDocumentOperationGroup, 0);

        [Export]
        public static readonly MenuItemDefinition EditCopyMenuItem = new CommandMenuItemDefinition<EditCopyCommandDefinition>(EditExtendedDocumentOperationGroup, 1);

        [Export]
        public static readonly MenuItemDefinition EditPasteMenuItem = new CommandMenuItemDefinition<EditPasteCommandDefinition>(EditExtendedDocumentOperationGroup, 2);

        [Export]
        public static readonly MenuItemDefinition EditSelectAllMenuItem = new CommandMenuItemDefinition<EditSelectAllCommandDefinition>(EditExtendedDocumentOperationGroup, 3);

        [Export]
        public static readonly MenuItemGroupDefinition EditExtendedDocumentOperationGroup2 = new MenuItemGroupDefinition(MainMenuDefinitions.EditMenu, 2);

        [Export]
        public static readonly MenuItemDefinition EditViewPropertiesMenuItem = new CommandMenuItemDefinition<EditViewPropertiesCommandDefinition>(EditExtendedDocumentOperationGroup2, 0);

        [Export]
        public static readonly MenuItemGroupDefinition ViewOperationsGroup = new MenuItemGroupDefinition(MainMenuDefinitions.ViewMenu, -1);

        [Export]
        public static readonly MenuItemDefinition ViewZoomInMenuItem = new CommandMenuItemDefinition<ViewZoomInCommandDefinition>(ViewOperationsGroup, 0);

        [Export]
        public static readonly MenuItemDefinition ViewZoomOutMenuItem = new CommandMenuItemDefinition<ViewZoomOutCommandDefinition>(ViewOperationsGroup, 1);

        [Export]
        public static readonly MenuItemGroupDefinition HelpItemsGroup = new MenuItemGroupDefinition(MainMenuDefinitions.HelpMenu, 0);

        [Export]
        public static readonly MenuItemDefinition HelpAboutPluginsMenuItem = new TextMenuItemDefinition(HelpItemsGroup, 0, "About Plugins");

        [Export]
        public static readonly MenuItemGroupDefinition HelpAboutPluginsGroup = new MenuItemGroupDefinition(HelpAboutPluginsMenuItem, 0);

    }
}
