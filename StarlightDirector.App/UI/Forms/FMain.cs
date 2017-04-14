using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Controls;
using System.Drawing;

namespace StarlightDirector.App.UI.Forms {
    public sealed partial class FMain : Form {

        public FMain() {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            InitializeComponent();
            RegisterEventHandlers();
            RegisterCommands();
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

        public void UpdateUIIndications(Difficulty currentDifficulty) {
            UpdateUIIndications(_editingFileName, currentDifficulty);
        }

        public void UpdateUIIndications(string editingFileName) {
            UpdateUIIndications(editingFileName, _cachedTitleDifficulty);
        }

        public void UpdateUIIndications(string editingFileName, Difficulty currentDifficulty) {
            if (editingFileName == null) {
                editingFileName = string.Empty;
            }
            var applicationTitle = ApplicationHelper.GetTitle();
            var difficultyDescription = DescribedEnumConverter.GetEnumDescription(currentDifficulty, typeof(Difficulty));
            Text = string.IsNullOrEmpty(editingFileName) ? applicationTitle : $"{editingFileName} [{difficultyDescription}] - {applicationTitle}";
            btnDifficultySelection.Text = difficultyDescription;
            _editingFileName = editingFileName;
            _cachedTitleDifficulty = currentDifficulty;
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

            btnDifficultySelection.BackColor = scheme.WindowNormalStatusBackground;
            btnDifficultySelection.ForeColor = scheme.WindowNormalStatusText;
            btnDifficultySelection.HoveringBackColor = scheme.WindowHoveringStatusBackground;
            btnDifficultySelection.HoveringTextColor = scheme.WindowHoveringStatusText;
            btnDifficultySelection.PressedBackColor = scheme.WindowPressedStatusBackground;
            btnDifficultySelection.PressedTextColor = scheme.WindowPressedStatusText;

            var toolStripRenderer = new StarlightToolStripRenderer(scheme);
            mainMenuStrip.Renderer = toolStripRenderer;
            ctxMain.Renderer = toolStripRenderer;
            ctxDifficultySelection.Renderer = toolStripRenderer;
            tlbNote.Renderer = toolStripRenderer;
            tlbEdit.Renderer = toolStripRenderer;
            tlbPostprocessing.Renderer = toolStripRenderer;
            tlbMeasure.Renderer = toolStripRenderer;
        }

        private static readonly Size FrameBorderSize = SystemInformation.FrameBorderSize;
        private static readonly int CaptionMargin = 65;
        private static readonly int ToolbarMargin = 250;
        private static readonly string DefaultDocumentName = "Untitled";

        private string _editingFileName = string.Empty;
        private Difficulty _cachedTitleDifficulty = Difficulty.Debut;

        private string _statusText = string.Empty;

    }
}
