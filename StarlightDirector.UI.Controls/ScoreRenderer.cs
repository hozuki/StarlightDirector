using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering.Direct2D;
using FontStyle = StarlightDirector.UI.Rendering.FontStyle;

namespace StarlightDirector.UI.Controls {
    public sealed partial class ScoreRenderer : Direct2DCanvas {

        internal ScoreRenderer(ScoreVisualizer visualizer) {
            Visualizer = visualizer;
        }

        public ScoreVisualizer Visualizer { get; }

        public Project Project { get; set; }

        public Difficulty Difficulty { get; set; }

        public float ScrollOffsetX { get; set; }

        public float ScrollOffsetY { get; set; }

        // 3/4
        public int PrimaryBeat { get; set; } = 4;

        public ScoreRendererConfig Config { get; } = new ScoreRendererConfig();

        protected override void OnRender(D2DRenderContext context) {
            var score = Project?.Scores?[Difficulty];
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
            _barGridOutlinePen = new D2DPen(Color.White, 4, context);
            _barNormalGridPen = new D2DPen(Color.White, 4, context);
            _barGridStartBeatPen = new D2DPen(Color.Red, 4, context);
            _barPrimaryBeatPen = new D2DPen(Color.Yellow, 4, context);
            _barSecondaryBeatPen = new D2DPen(Color.Violet, 4, context);
            _scoreBarFont = new D2DFont(context.DirectWriteFactory, DefaultFont.Name, 10, FontStyle.None, 10);
        }

        protected override void OnDisposeResources(D2DRenderContext context) {
            _barNormalGridPen?.Dispose();
            _barPrimaryBeatPen?.Dispose();
            _barSecondaryBeatPen?.Dispose();
            _barGridStartBeatPen?.Dispose();
            _barGridOutlinePen?.Dispose();
            _scoreBarFont?.Dispose();
        }

    }
}
