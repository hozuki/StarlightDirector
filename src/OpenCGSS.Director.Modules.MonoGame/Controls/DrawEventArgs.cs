using Microsoft.Xna.Framework.Graphics;

namespace OpenCGSS.Director.Modules.MonoGame.Controls {
    public sealed class DrawEventArgs : GraphicsDeviceEventArgs {
        private readonly DrawingSurface _drawingSurface;

        public DrawEventArgs(DrawingSurface drawingSurface, GraphicsDevice graphicsDevice)
            : base(graphicsDevice) {
            _drawingSurface = drawingSurface;
        }

        public void InvalidateSurface() {
            _drawingSurface.Invalidate();
        }
    }
}
