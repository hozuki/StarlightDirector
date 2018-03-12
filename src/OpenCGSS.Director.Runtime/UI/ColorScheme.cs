using System.Drawing;

namespace OpenCGSS.Director.UI {
    public sealed class ColorScheme {

        public string Name { get; internal set; } = "Microsoft Word 2016";

        public Color ActiveWindowTitle { get; internal set; } = Color.FromArgb(249, 249, 249);

        public Color InactiveWindowTitle { get; internal set; } = Color.FromArgb(164, 185, 210);

        public Color CaptionBackground { get; internal set; } = Color.FromArgb(42, 87, 154);

        public Color Workspace { get; internal set; } = Color.FromArgb(230, 230, 230);

        public Color WindowBorder { get; internal set; } = Color.FromArgb(131, 131, 131);

        public Color ControlText { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color WindowNormalStatusText { get; internal set; } = Color.FromArgb(116, 116, 116);

        public Color WindowHoveringStatusText { get; internal set; } = Color.FromArgb(116, 116, 116);

        public Color WindowPressedStatusText { get; internal set; } = Color.FromArgb(116, 116, 116);

        public Color WindowStatusSeparator { get; internal set; } = Color.FromArgb(213, 213, 213);

        public Color WindowNormalStatusBackground { get; internal set; } = Color.FromArgb(241, 241, 241);

        public Color WindowHoveringStatusBackground { get; internal set; } = Color.FromArgb(197, 197, 197);

        public Color WindowPressedStatusBackground { get; internal set; } = Color.FromArgb(174, 174, 174);

        public Color SysMinNormalBackground { get; internal set; } = Color.FromArgb(42, 87, 154);

        public Color SysMinHoveringBackground { get; internal set; } = Color.FromArgb(62, 109, 182);

        public Color SysMinPressedBackground { get; internal set; } = Color.FromArgb(18, 64, 120);

        public Color SysMinNormalIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color SysMinHoveringIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color SysMinPressedIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color SysMaxNormalBackground { get; internal set; } = Color.FromArgb(42, 87, 154);

        public Color SysMaxHoveringBackground { get; internal set; } = Color.FromArgb(62, 109, 182);

        public Color SysMaxPressedBackground { get; internal set; } = Color.FromArgb(18, 64, 120);

        public Color SysMaxNormalIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color SysMaxHoveringIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color SysMaxPressedIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color SysCloseNormalBackground { get; internal set; } = Color.FromArgb(42, 87, 154);

        public Color SysCloseHoveringBackground { get; internal set; } = Color.FromArgb(232, 17, 35);

        public Color SysClosePressedBackground { get; internal set; } = Color.FromArgb(241, 112, 122);

        public Color SysCloseNormalIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color SysCloseHoveringIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color SysClosePressedIcon { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color MenuBarBackground { get; internal set; } = Color.FromArgb(42, 87, 154);

        public Color MenuBarHoveringBackground { get; internal set; } = Color.FromArgb(62, 109, 182);

        public Color MenuBarPressedBackground { get; internal set; } = Color.FromArgb(241, 241, 241);

        public Color MenuBarText { get; internal set; } = Color.FromArgb(249, 249, 249);

        public Color MenuBarHoveringText { get; internal set; } = Color.FromArgb(249, 249, 249);

        public Color MenuBarPressedText { get; internal set; } = Color.FromArgb(42, 87, 154);

        public Color MenuItemBackground { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color MenuItemHoveringBackground { get; internal set; } = Color.FromArgb(197, 197, 197);

        public Color MenuItemPressedBackground { get; internal set; } = Color.FromArgb(174, 174, 174);

        public Color MenuItemDisabledBackground { get; internal set; } = Color.FromArgb(255, 255, 255);

        public Color MenuItemText { get; internal set; } = Color.FromArgb(68, 68, 68);

        public Color MenuItemHoveringText { get; internal set; } = Color.FromArgb(68, 68, 68);

        public Color MenuItemPressedText { get; internal set; } = Color.FromArgb(68, 68, 68);

        public Color MenuItemDisabledText { get; internal set; } = Color.FromArgb(152, 152, 152);

        public Color MenuItemCheckBoxBackground { get; internal set; } = Color.FromArgb(230, 238, 250);

        public Color MenuItemCheckBoxBorder { get; internal set; } = Color.FromArgb(163, 189, 227);

        public Color ToolbarItemBackground { get; internal set; } = Color.FromArgb(241, 241, 241);

        public Color ToolbarItemHoveringBackground { get; internal set; } = Color.FromArgb(197, 197, 197);

        public Color ToolbarItemPressedBackground { get; internal set; } = Color.FromArgb(174, 174, 174);

        public Color ToolbarItemDisabledBackground { get; internal set; } = Color.FromArgb(241, 241, 241);

        public Color ToolbarItemText { get; internal set; } = Color.FromArgb(68, 68, 68);

        public Color ToolbarItemHoveringText { get; internal set; } = Color.FromArgb(68, 68, 68);

        public Color ToolbarItemPressedText { get; internal set; } = Color.FromArgb(68, 68, 68);

        public Color ToolbarItemDisabledText { get; internal set; } = Color.FromArgb(152, 152, 152);

        public Color ToolbarBackground { get; internal set; } = Color.FromArgb(241, 241, 241);

        public Color ToolStripBorder { get; internal set; } = Color.FromArgb(213, 213, 213);

        public Color Separator { get; internal set; } = Color.FromArgb(213, 213, 213);

        public static ColorScheme Default { get; } = new ColorScheme();

        public static ColorScheme Current { get; set; } = Default;

    }
}
