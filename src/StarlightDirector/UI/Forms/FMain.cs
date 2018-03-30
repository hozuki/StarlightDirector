using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs;
using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.Models.Editor.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    public sealed partial class FMain : Form {

        public FMain() {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            InitializeComponent();
            RegisterEventHandlers();
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

        public void UpdateUIIndications() {
            UpdateUIIndications(_cachedTitleDifficulty);
        }

        public void UpdateUIIndications(Difficulty currentDifficulty) {
            var project = visualizer.Editor.Project;
            string editingFileName;
            if (project == null || !project.WasSaved()) {
                editingFileName = LanguageManager.TryGetString("misc.default_doc_name") ?? DefaultDocumentName;
            } else {
                var fileInfo = new FileInfo(project.SaveFilePath);
                editingFileName = fileInfo.Name;
            }
            var applicationTitle = AssemblyHelper.GetTitle();
            var difficultyDescription = DescribedEnumConverter.GetEnumDescription(currentDifficulty);

            string stateDescription;
            switch (visualizer.Display) {
                case VisualizerDisplay.Editor:
                    stateDescription = LanguageManager.Current.GetString("misc.state.editing");
                    break;
                case VisualizerDisplay.Previewer:
                    stateDescription = LanguageManager.Current.GetString("misc.state.previewing");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            string text;
            if (string.IsNullOrEmpty(editingFileName)) {
                text = applicationTitle;
            } else {
                var titleTemplate = LanguageManager.TryGetString("ui.fmain.text_template") ?? DefaultTitleTemplate;
                text = string.Format(titleTemplate, editingFileName, difficultyDescription, applicationTitle, stateDescription);
            }
            if (project != null) {
                if (project.IsModified) {
                    text = "* " + text;
                }
            }
            Text = text;
            tsbScoreDifficultySelection.Text = difficultyDescription;
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

            var toolStripRenderer = new StarlightToolStripRenderer(scheme);
            mainMenuStrip.Renderer = toolStripRenderer;
            ctxMain.Renderer = toolStripRenderer;
            tlbNote.Renderer = toolStripRenderer;
            tlbStandard.Renderer = toolStripRenderer;
            tlbPostprocessing.Renderer = toolStripRenderer;
            tlbMeasure.Renderer = toolStripRenderer;
            tlbEditAndView.Renderer = toolStripRenderer;
        }

        private static readonly Size FrameBorderSize = SystemInformation.FrameBorderSize;
        private static readonly int CaptionMargin = 65;
        private static readonly int ToolbarMargin = 250;
        private static readonly string DefaultDocumentName = "Untitled";
        private static readonly string DefaultTitleTemplate = "{0} [{1}] - {2}";

        private Difficulty _cachedTitleDifficulty = Difficulty.Debut;

        private static readonly string TapAudioFilePath = "Resources/SFX/Director/se_live_tap_perfect.wav";
        private static readonly string FlickAudioFilePath = "Resources/SFX/Director/se_live_flic_perfect.wav";
        private static readonly string SlideAudioFilePath = "Resources/SFX/Director/se_live_slide_node.wav";

        private string _statusText = string.Empty;

        [CanBeNull]
        private SDCommunication _communication;

    }
}
