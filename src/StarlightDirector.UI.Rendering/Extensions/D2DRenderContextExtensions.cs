using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Rendering.Extensions {
    public static class D2DRenderContextExtensions {

        public static D2DPathData CreatePathData(this D2DRenderContext context) {
            return new D2DPathData(context.RenderTarget.Factory);
        }

    }
}
