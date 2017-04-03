using StarlightDirector.Core;

namespace StarlightDirector.UI.Rendering {
    public abstract class Pen : DisposableBase {

        protected Pen(Brush brush, float strokeWidth) {
            Brush = brush;
            StrokeWidth = strokeWidth;
        }

        public Brush Brush { get; internal set; }

        public float StrokeWidth { get; }

    }
}
