using StarlightDirector.Core;

namespace StarlightDirector.UI.Rendering {
    public abstract class Brush : DisposableBase {

        protected Brush(RenderContext context) {
            RenderContext = context;
        }

        public RenderContext RenderContext { get; }

    }
}
