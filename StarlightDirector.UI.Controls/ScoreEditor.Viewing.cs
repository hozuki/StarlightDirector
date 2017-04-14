using StarlightDirector.Core;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

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

        private static readonly float ZoomScale = 1.2f;

    }
}
