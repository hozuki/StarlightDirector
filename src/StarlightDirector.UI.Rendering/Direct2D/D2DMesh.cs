using SharpDX.Direct2D1;
using StarlightDirector.Core;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public sealed class D2DMesh : DisposableBase {

        public D2DMesh(D2DRenderContext context) {
            _mesh = new Mesh(context.RenderTarget);
        }

        public D2DMesh(D2DRenderContext context, D2DTriangle[] triangles) {
            var t = new Triangle[triangles.Length];
            for (var i = 0; i < triangles.Length; ++i) {
                t[i] = triangles[i].ToNative();
            }
            _mesh = new Mesh(context.RenderTarget, t);
        }

        public Mesh Native => _mesh;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _mesh?.Dispose();
                _mesh = null;
            }
        }

        private Mesh _mesh;

    }
}
