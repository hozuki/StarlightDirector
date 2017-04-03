using System.Windows.Forms;
using StarlightDirector.UI.Controls;

namespace StarlightDirector.App.UI.Forms {
    public partial class FMain : Form {

        public FMain() {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Opaque, false);
            InitializeComponent();
            RegisterEventHandlers();
            RegisterCommands();
            EditingFileName = "ABC.sldproj";
        }

        ~FMain() {
            UnregisterCommands();
            UnregisterEventHandlers();
        }

        public string StatusText {
            get { return _statusText; }
            set {
                if (value == null) {
                    value = string.Empty;
                }
                var b = value != _statusText;
                if (b) {
                    _statusText = value;
                    Invalidate();
                }
            }
        }

        public string EditingFileName {
            get { return _editingFileName; }
            set {
                if (value == null) {
                    value = string.Empty;
                }
                _editingFileName = value;
                var applicationTitle = ApplicationHelper.GetTitle();
                Text = !string.IsNullOrEmpty(value) ? $"{value} - {applicationTitle}" : applicationTitle;
            }
        }

        public void ApplyColorScheme(ColorScheme scheme) {
            BackColor = scheme.Workspace;

            sysMinimize.BackColor = scheme.SysMinNormalBackground;
            sysMinimize.IconColor = scheme.SysMinNormalIcon;
            sysMinimize.HoveringBackColor = scheme.SysMinHoveringBackground;
            sysMinimize.HoveringIconColor = scheme.SysMinHoveringIcon;
            sysMinimize.PressedBackColor = scheme.SysMinPressedBackground;
            sysMinimize.PressedIconColor = scheme.SysMinPressedIcon;

            sysMaximizeRestore.BackColor = scheme.SysMaxNormalBackground;
            sysMaximizeRestore.IconColor = scheme.SysMaxNormalIcon;
            sysMaximizeRestore.HoveringBackColor = scheme.SysMaxHoveringBackground;
            sysMaximizeRestore.HoveringIconColor = scheme.SysMaxHoveringIcon;
            sysMaximizeRestore.PressedBackColor = scheme.SysMaxPressedBackground;
            sysMaximizeRestore.PressedIconColor = scheme.SysMaxPressedIcon;

            sysClose.BackColor = scheme.SysCloseNormalBackground;
            sysClose.IconColor = scheme.SysCloseNormalIcon;
            sysClose.HoveringBackColor = scheme.SysCloseHoveringBackground;
            sysClose.HoveringIconColor = scheme.SysCloseHoveringIcon;
            sysClose.PressedBackColor = scheme.SysClosePressedBackground;
            sysClose.PressedIconColor = scheme.SysClosePressedIcon;

            mainMenuStrip.BackColor = scheme.MenuBarBackground;
            mainMenuStrip.ForeColor = scheme.ControlText;
            lblCaption.BackColor = scheme.CaptionBackground;
            picIcon.BackColor = scheme.CaptionBackground;
            panel2.BackColor = scheme.ToolbarBackground;
            visualizer.BackColor = scheme.Workspace;

            var toolStripRenderer = new StarlightToolStripRenderer(scheme);
            mainMenuStrip.Renderer = toolStripRenderer;
            context.Renderer = toolStripRenderer;
            tlbNote.Renderer = toolStripRenderer;
            tlbEdit.Renderer = toolStripRenderer;
            tlbPostprocessing.Renderer = toolStripRenderer;
            tlbMeasure.Renderer = toolStripRenderer;
        }

        private static readonly int CaptionMargin = 65;
        private static readonly int ToolbarMargin = 250;

        private string _statusText = string.Empty;
        private string _editingFileName = string.Empty;

    }
}
