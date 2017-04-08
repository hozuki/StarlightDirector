using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;
using StarlightDirector.UI.Rendering.Direct2D;
using FontStyle = StarlightDirector.UI.Rendering.FontStyle;

namespace StarlightDirector.UI.Controls {
    public sealed partial class ScoreRenderer : Direct2DCanvas {

        [Browsable(false)]
        public ScrollBar ScrollBar { get; internal set; }

        [Browsable(false)]
        public Project Project {
            get => _project;
            set {
                var b = value != _project;
                if (b) {
                    _project = value;
                    RecalcLayout();
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public Difficulty Difficulty {
            get => _difficulty;
            set {
                var b = value != _difficulty;
                if (b) {
                    _difficulty = value;
                    RecalcLayout();
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public int ScrollOffsetX { get; set; }

        [Browsable(false)]
        public int ScrollOffsetY {
            get => _scrollOffsetY;
            set {
                var b = value != _scrollOffsetY;
                if (b) {
                    _scrollOffsetY = value;
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public float BarLineSpaceUnit {
            get => _barLineSpaceUnit;
            set {
                var b = !value.Equals(_barLineSpaceUnit);
                if (b) {
                    _barLineSpaceUnit = value;
                    RecalcLayout();
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public PrimaryBeatMode PrimaryBeatMode { get; set; } = PrimaryBeatMode.EveryFourBeats;

        [Browsable(false)]
        public ScoreRendererConfig Config { get; } = new ScoreRendererConfig();

        public float GetFullHeight() {
            var score = Project?.GetScore(Difficulty);
            if (score == null) {
                return 0;
            }
            var height = score.Bars.Sum(bar => BarLineSpaceUnit * bar.GetNumberOfGrids());
            return height;
        }

        public void ZoomIn() {
            var max = 2.1f * Config.NoteRadius;
            var min = max / MaxNumberOfGrids;
            var unit = BarLineSpaceUnit;
            unit *= ZoomScale;
            unit = unit.Clamp(min, max);
            BarLineSpaceUnit = unit;
        }

        public void ZoomOut() {
            var max = 2.1f * Config.NoteRadius;
            var min = max / MaxNumberOfGrids;
            var unit = BarLineSpaceUnit;
            unit /= ZoomScale;
            unit = unit.Clamp(min, max);
            BarLineSpaceUnit = unit;
        }

        internal ScoreRenderer() {
        }

        internal void RecalcLayout() {
            var scrollBar = ScrollBar;
            var clientSize = ClientSize;
            if (scrollBar != null) {
                var expectedHeight = GetFullHeight();
                scrollBar.Minimum = clientSize.Height / 2;
                scrollBar.Maximum = clientSize.Height / 2 + (int)Math.Round(expectedHeight);
            }
        }

        protected override void OnClientSizeChanged(EventArgs e) {
            base.OnClientSizeChanged(e);
            RecalcLayout();
        }

        protected override void OnRender(D2DRenderContext context) {
            var score = Project?.GetScore(Difficulty);
            var hasAnyBar = score?.HasAnyBar ?? false;
            if (hasAnyBar) {
                RenderBars(context, score);
            }
            var hasAnyNote = score?.HasAnyNote ?? false;
            if (hasAnyNote) {
                RenderNotes(context, score);
            }
        }

        protected override void OnCreateResources(D2DRenderContext context) {
            _barGridOutlinePen = new D2DPen(context, Color.White, 1);
            _barNormalGridPen = new D2DPen(context, Color.White, 1);
            _barGridStartBeatPen = new D2DPen(context, Color.Red, 1);
            _barPrimaryBeatPen = new D2DPen(context, Color.Yellow, 1);
            _barSecondaryBeatPen = new D2DPen(context, Color.Violet, 1);

            _gridNumberBrush = new D2DSolidBrush(context, Color.White);

            _scoreBarFont = new D2DFont(context.DirectWriteFactory, Font.Name, Font.SizeInPoints, FontStyle.Regular, 10);
            _scoreBarBoldFont = new D2DFont(context.DirectWriteFactory, _scoreBarFont.FamilyName, _scoreBarFont.Size, FontStyle.Bold, _scoreBarFont.Weight);

            _noteCommonStroke = new D2DPen(context, Color.FromArgb(0x22, 0x22, 0x22), NoteShapeStrokeWidth);
            _tapNoteShapeStroke = new D2DPen(context, Color.FromArgb(0xFF, 0x33, 0x66), NoteShapeStrokeWidth);
            _holdNoteShapeStroke = new D2DPen(context, Color.FromArgb(0xFF, 0xBB, 0x22), NoteShapeStrokeWidth);
            _flickNoteShapeStroke = new D2DPen(context, Color.FromArgb(0x22, 0x55, 0xBB), NoteShapeStrokeWidth);

            _noteCommonFill = new D2DSolidBrush(context, Color.White);
            _holdNoteShapeFillInner = new D2DSolidBrush(context, Color.White);
            _flickNoteShapeFillInner = new D2DSolidBrush(context, Color.White);
            _slideNoteShapeFillInner = new D2DSolidBrush(context, Color.White);

            _syncLineStroke = new D2DPen(context, Color.White, 4);
            _holdLineStroke = new D2DPen(context, Color.White, 10);
            _flickLineStroke = new D2DPen(context, Color.White, 14.1f);
            _slideLineStroke = new D2DPen(context, Color.White, 14.1f);
        }

        protected override void OnDisposeResources(D2DRenderContext context) {
            _barNormalGridPen?.Dispose();
            _barPrimaryBeatPen?.Dispose();
            _barSecondaryBeatPen?.Dispose();
            _barGridStartBeatPen?.Dispose();
            _barGridOutlinePen?.Dispose();

            _gridNumberBrush?.Dispose();

            _scoreBarFont?.Dispose();
            _scoreBarBoldFont?.Dispose();

            _noteCommonStroke?.Dispose();
            _tapNoteShapeStroke?.Dispose();
            _holdNoteShapeStroke?.Dispose();
            _flickNoteShapeStroke?.Dispose();

            _noteCommonFill?.Dispose();
            _holdNoteShapeFillInner?.Dispose();
            _flickNoteShapeFillInner?.Dispose();
            _slideNoteShapeFillInner?.Dispose();

            _syncLineStroke?.Dispose();
            _holdLineStroke?.Dispose();
            _flickLineStroke?.Dispose();
            _slideLineStroke?.Dispose();
        }

        private int _scrollOffsetY;
        private Project _project;
        private Difficulty _difficulty = Difficulty.Debut;
        private float _barLineSpaceUnit = 5;

        private static readonly float ZoomScale = 1.2f;
        // This is used for scaling. It can be different with signature*gps.
        private static readonly int MaxNumberOfGrids = 96;

    }
}
