using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;
using OpenCGSS.StarlightDirector.UI.Controls.Previewing;
using Point = System.Drawing.Point;

namespace OpenCGSS.StarlightDirector.UI.Controls {
    public partial class ScoreVisualizer : UserControl {

        public ScoreVisualizer() {
            InitializeComponent();
            RegisterEventHandlers();

            editor.ScrollBar = vScroll;
            _scoreEditorSelectionHandler = new ScoreEditorGestureHandler(this);

            SetStyle(ControlStyles.Selectable, true);
        }

        ~ScoreVisualizer() {
            UnregisterEventHandlers();
        }

        public event EventHandler<ContextMenuRequestedEventArgs> ContextMenuRequested;
        public event EventHandler<EventArgs> ProjectModified;
        public event EventHandler<EventArgs> DisplayChanged;

        [Browsable(false)]
        public ScoreEditor Editor => editor;

        [Browsable(false)]
        public VScrollBar ScrollBar => vScroll;

        [Browsable(false)]
        public ScorePreviewer Previewer => previewer;

        [Browsable(false)]
        [DefaultValue(true)]
        public bool InvertedScrolling { get; set; } = true;

        [Browsable(false)]
        public int ScrollingSpeed { get; set; } = 5;

        public VisualizerDisplay Display {
            get => _display;
            set {
                var b = value != _display;
                if (b) {
                    _display = value;
                    OnDisplayChanged(EventArgs.Empty);
                }
            }
        }

        public void RecalcLayout() {
            editor.RecalcLayout();
        }

        public void ScrollUp() {
            ScrollInternal(true, false);
        }

        public void ScrollDown() {
            ScrollInternal(false, false);
        }

        public void ScrollUpLarge() {
            ScrollInternal(true, true);
        }

        public void ScrollDownLarge() {
            ScrollInternal(false, true);
        }

        public void ScrollToStart() {
            vScroll.Value = vScroll.Minimum;
        }

        public void ScrollToEnd() {
            vScroll.Value = vScroll.Maximum;
        }

        [DebuggerStepThrough]
        internal void InformProjectModified() {
            ProjectModified?.Invoke(this, EventArgs.Empty);
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            OnDisplayChanged(EventArgs.Empty);
        }

        protected override void OnLayout(LayoutEventArgs e) {
            // Anchor doesn't seem to work.
            base.OnLayout(e);

            var clientSize = ClientSize;

            var scrollBarLeft = clientSize.Width - vScroll.Width - ConstMargin * 2;
            var scrollBarTop = ConstMargin;
            var scrollBarHeight = clientSize.Height - ConstMargin * 2;
            vScroll.Location = new Point(scrollBarLeft, scrollBarTop);
            vScroll.Height = scrollBarHeight;

            var editorLeft = ConstMargin;
            var editorTop = ConstMargin;
            var editorWidth = scrollBarLeft - ConstMargin;
            var editorHeight = clientSize.Height - ConstMargin * 2;
            editor.Location = new Point(editorLeft, editorTop);
            editor.Size = new Size(editorWidth, editorHeight);

            var previewerLeft = ConstMargin;
            var previewerTop = ConstMargin;
            var previewerWidth = clientSize.Width - ConstMargin * 2;
            var previewerHeight = clientSize.Height - ConstMargin * 2;
            previewer.Location = new Point(previewerLeft, previewerTop);
            previewer.Size = new Size(previewerWidth, previewerHeight);
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);

            var modifiers = ModifierKeys;
            if (modifiers == Keys.Control) {
                if (e.Delta > 0) {
                    editor.ZoomIn();
                } else {
                    editor.ZoomOut();
                }
                return;
            }

            ScrollInternal(e.Delta > 0, modifiers == Keys.Shift);
        }

        protected virtual void OnDisplayChanged(EventArgs e) {
            var d = Display;
            switch (d) {
                case VisualizerDisplay.Editor:
                    editor.Visible = true;
                    vScroll.Visible = true;
                    previewer.Visible = false;
                    break;
                case VisualizerDisplay.Previewer:
                    previewer.Visible = true;
                    editor.Visible = false;
                    vScroll.Visible = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            DisplayChanged?.Invoke(this, e);
        }

        private void ScrollInternal(bool isUp, bool isLarge) {
            var newScrollValue = vScroll.Value;
            var deltaScrollValue = 0;
            if (isUp) {
                // Up
                deltaScrollValue = isLarge ? -vScroll.LargeChange : -vScroll.SmallChange;
            } else {
                // Down
                deltaScrollValue = isLarge ? vScroll.LargeChange : vScroll.SmallChange;
            }
            var invertFactor = InvertedScrolling ? 1 : -1;
            deltaScrollValue *= invertFactor;
            var speed = ScrollingSpeed;
            deltaScrollValue = (int)(deltaScrollValue * speed / (10f / 2));
            newScrollValue += deltaScrollValue;
            newScrollValue = MathHelper.Clamp(newScrollValue, vScroll.Minimum, vScroll.Maximum);
            vScroll.Value = newScrollValue;
        }

        private void UnregisterEventHandlers() {
            vScroll.ValueChanged -= VScroll_ValueChanged;
            editor.MouseDown -= Editor_MouseDown;
            editor.MouseUp -= Editor_MouseUp;
            editor.MouseMove -= Editor_MouseMove;
            editor.EditModeChanged -= Editor_EditModeChanged;
        }

        private void RegisterEventHandlers() {
            vScroll.ValueChanged += VScroll_ValueChanged;
            editor.MouseDown += Editor_MouseDown;
            editor.MouseUp += Editor_MouseUp;
            editor.MouseMove += Editor_MouseMove;
            editor.EditModeChanged += Editor_EditModeChanged;
        }

        internal void RequestContextMenu(ContextMenuRequestedEventArgs e) {
            ContextMenuRequested?.Invoke(this, e);
        }

        private void Editor_MouseDown(object sender, MouseEventArgs e) {
            _scoreEditorSelectionHandler.OnMouseDown(e);
        }

        private void Editor_MouseUp(object sender, MouseEventArgs e) {
            _scoreEditorSelectionHandler.OnMouseUp(e);
        }

        private void Editor_MouseMove(object sender, MouseEventArgs e) {
            _scoreEditorSelectionHandler.OnMouseMove(e);
        }

        private void Editor_EditModeChanged(object sender, EventArgs e) {
            _scoreEditorSelectionHandler.OnEditModeChanged();
        }

        private void VScroll_ValueChanged(object sender, EventArgs eventArgs) {
            editor.ScrollOffsetY = vScroll.Value;
        }

        private static readonly int ConstMargin = 3;

        private readonly ScoreEditorGestureHandler _scoreEditorSelectionHandler;
        private VisualizerDisplay _display = VisualizerDisplay.Editor;

    }
}
