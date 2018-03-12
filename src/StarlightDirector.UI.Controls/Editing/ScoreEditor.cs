using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using JetBrains.Annotations;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;
using StarlightDirector.UI.Controls.Direct2D;
using StarlightDirector.UI.Controls.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Controls.Editing {
    public sealed partial class ScoreEditor : Direct2DCanvas {

        public ScoreEditor() {
            _editorRenderer = new ScoreEditorRenderer();
            Look.BarLineSpaceUnitChanged += Look_BarLineSpaceUnitChanged;
        }

        ~ScoreEditor() {
            Look.BarLineSpaceUnitChanged -= Look_BarLineSpaceUnitChanged;
        }

        public event EventHandler<EventArgs> EditModeChanged;

        [Browsable(false)]
        public ScrollBar ScrollBar { get; internal set; }

        public int ScrollOffsetX { get; set; }

        public int ScrollOffsetY {
            [DebuggerStepThrough]
            get => _scrollOffsetY;
            internal set {
                var scrollBar = ScrollBar;
                if (scrollBar != null) {
                    value = value.Clamp(scrollBar.Minimum, scrollBar.Maximum);
                }
                var b = value != _scrollOffsetY;
                if (b) {
                    _scrollOffsetY = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public Project Project {
            [DebuggerStepThrough]
            get => _project;
            set {
                var b = value != _project;
                if (b) {
                    _project = value;
                    RecalcLayout();
                    UpdateBarStartTimeText();
                }
            }
        }

        [Browsable(false)]
        public Difficulty Difficulty {
            [DebuggerStepThrough]
            get => _difficulty;
            set {
                var b = value != _difficulty;
                if (b) {
                    _difficulty = value;
                    RecalcLayout();
                    UpdateBarStartTimeText();
                }
            }
        }

        [Browsable(false)]
        [CanBeNull]
        public Score CurrentScore {
            [DebuggerStepThrough]
            get => Project?.GetScore(Difficulty);
        }

        [Browsable(false)]
        public ScoreEditMode EditMode {
            get => _editMode;
            set {
                var b = value != _editMode;
                if (b) {
                    _editMode = value;
                    OnEditModeChanged(EventArgs.Empty);
                }
            }
        }

        [Browsable(false)]
        public NotePosition NoteStartPosition { get; set; }

        [Browsable(false)]
        public ScoreEditorConfig Config { get; } = new ScoreEditorConfig();

        [Browsable(false)]
        public ScoreEditorLook Look { get; } = new ScoreEditorLook();

        public float GetFullHeight() {
            var score = CurrentScore;
            if (score == null) {
                return 0;
            }
            var look = Look;
            var height = score.Bars.Sum(bar => look.BarLineSpaceUnit * bar.GetNumberOfGrids());
            return height;
        }

        protected override void OnClientSizeChanged(EventArgs e) {
            base.OnClientSizeChanged(e);
            RecalcLayout();
        }

        protected override void OnRender(D2DRenderContext context) {
            _editorRenderer.Render(context, CurrentScore, Config, Look, ScrollOffsetY, SelectionRectangle);
        }

        protected override void OnCreateResources(D2DRenderContext context) {
            _editorRenderer.CreateResources(context, Font);
        }

        protected override void OnDisposeResources(D2DRenderContext context) {
            _editorRenderer.DisposeResources();
        }

        private void Look_BarLineSpaceUnitChanged(object sender, EventArgs eventArgs) {
            RecalcLayout();
            Invalidate();
        }

        private void OnEditModeChanged(EventArgs e) {
            EditModeChanged?.Invoke(this, e);
        }

        // This is used for scaling. It can be different with signature*gps.
        private static readonly int MaxNumberOfGrids = 96;


        private Project _project;
        private Difficulty _difficulty = Difficulty.Debut;
        private ScoreEditMode _editMode;
        private int _scrollOffsetY;
        private readonly ScoreEditorRenderer _editorRenderer;

    }
}
