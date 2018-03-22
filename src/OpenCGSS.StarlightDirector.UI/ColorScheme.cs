using System.Drawing;

namespace OpenCGSS.StarlightDirector.UI {
    public sealed class ColorScheme {

        public string Name { get; set; } = "Microsoft Word 2016";

        public Color ActiveWindowTitle { get; } = Color.FromArgb(249, 249, 249);

        public Color InactiveWindowTitle { get; } = Color.FromArgb(164, 185, 210);

        public Color CaptionBackground { get; } = Color.FromArgb(42, 87, 154);

        public Color Workspace { get; } = Color.FromArgb(230, 230, 230);

        public Color WindowBorder { get; } = Color.FromArgb(131, 131, 131);

        public Color ControlText { get; } = Color.FromArgb(255, 255, 255);

        public Color WindowNormalStatusText { get; } = Color.FromArgb(116, 116, 116);

        public Color WindowHoveringStatusText { get; } = Color.FromArgb(116, 116, 116);

        public Color WindowPressedStatusText { get; } = Color.FromArgb(116, 116, 116);

        public Color WindowStatusSeparator { get; } = Color.FromArgb(213, 213, 213);

        public Color WindowNormalStatusBackground { get; } = Color.FromArgb(241, 241, 241);

        public Color WindowHoveringStatusBackground { get; } = Color.FromArgb(197, 197, 197);

        public Color WindowPressedStatusBackground { get; } = Color.FromArgb(174, 174, 174);

        public Color SysMinNormalBackground { get; } = Color.FromArgb(42, 87, 154);

        public Color SysMinHoveringBackground { get; } = Color.FromArgb(62, 109, 182);

        public Color SysMinPressedBackground { get; } = Color.FromArgb(18, 64, 120);

        public Color SysMinNormalIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color SysMinHoveringIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color SysMinPressedIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color SysMaxNormalBackground { get; } = Color.FromArgb(42, 87, 154);

        public Color SysMaxHoveringBackground { get; } = Color.FromArgb(62, 109, 182);

        public Color SysMaxPressedBackground { get; } = Color.FromArgb(18, 64, 120);

        public Color SysMaxNormalIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color SysMaxHoveringIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color SysMaxPressedIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color SysCloseNormalBackground { get; } = Color.FromArgb(42, 87, 154);

        public Color SysCloseHoveringBackground { get; } = Color.FromArgb(232, 17, 35);

        public Color SysClosePressedBackground { get; } = Color.FromArgb(241, 112, 122);

        public Color SysCloseNormalIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color SysCloseHoveringIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color SysClosePressedIcon { get; } = Color.FromArgb(255, 255, 255);

        public Color MenuBarBackground { get; } = Color.FromArgb(42, 87, 154);

        public Color MenuBarHoveringBackground { get; } = Color.FromArgb(62, 109, 182);

        public Color MenuBarPressedBackground { get; } = Color.FromArgb(241, 241, 241);

        public Color MenuBarText { get; } = Color.FromArgb(249, 249, 249);

        public Color MenuBarHoveringText { get; } = Color.FromArgb(249, 249, 249);

        public Color MenuBarPressedText { get; } = Color.FromArgb(42, 87, 154);

        public Color MenuItemBackground { get; } = Color.FromArgb(255, 255, 255);

        public Color MenuItemHoveringBackground { get; } = Color.FromArgb(197, 197, 197);

        public Color MenuItemPressedBackground { get; } = Color.FromArgb(174, 174, 174);

        public Color MenuItemDisabledBackground { get; } = Color.FromArgb(255, 255, 255);

        public Color MenuItemText { get; } = Color.FromArgb(68, 68, 68);

        public Color MenuItemHoveringText { get; } = Color.FromArgb(68, 68, 68);

        public Color MenuItemPressedText { get; } = Color.FromArgb(68, 68, 68);

        public Color MenuItemDisabledText { get; } = Color.FromArgb(152, 152, 152);

        public Color MenuItemCheckBoxBackground { get; } = Color.FromArgb(230, 238, 250);

        public Color MenuItemCheckBoxBorder { get; } = Color.FromArgb(163, 189, 227);

        public Color ToolbarItemBackground { get; } = Color.FromArgb(241, 241, 241);

        public Color ToolbarItemHoveringBackground { get; } = Color.FromArgb(197, 197, 197);

        public Color ToolbarItemPressedBackground { get; } = Color.FromArgb(174, 174, 174);

        public Color ToolbarItemDisabledBackground { get; } = Color.FromArgb(241, 241, 241);

        public Color ToolbarItemText { get; } = Color.FromArgb(68, 68, 68);

        public Color ToolbarItemHoveringText { get; } = Color.FromArgb(68, 68, 68);

        public Color ToolbarItemPressedText { get; } = Color.FromArgb(68, 68, 68);

        public Color ToolbarItemDisabledText { get; } = Color.FromArgb(152, 152, 152);

        public Color ToolbarBackground { get; } = Color.FromArgb(241, 241, 241);

        public Color ToolStripBorder { get; } = Color.FromArgb(213, 213, 213);

        public Color Separator { get; } = Color.FromArgb(213, 213, 213);

        public static ColorScheme Default { get; } = new ColorScheme();

        public static ColorScheme Current { get; set; } = Default;

    }
}
