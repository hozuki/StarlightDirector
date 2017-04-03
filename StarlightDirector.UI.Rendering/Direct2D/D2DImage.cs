using SharpDX.Direct2D1;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public abstract class D2DImage : Image {

        protected D2DImage(Bitmap bitmap) {
            NativeImage = bitmap;
        }

        public Bitmap NativeImage { get; }

        public override float Width => NativeImage.Size.Width;

        public override float Height => NativeImage.Size.Height;

    }
}
