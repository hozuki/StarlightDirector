using JetBrains.Annotations;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Overlay;

namespace OpenCGSS.StarlightDirector.UI.Controls {
    internal sealed class GraphicsBatch : DisposableBase {

        public void Update([NotNull] GraphicsDevice graphicsDevice) {
            Guard.ArgumentNotNull(graphicsDevice, nameof(graphicsDevice));

            var viewport = graphicsDevice.Viewport;

            if (graphicsDevice == _lastGraphicsDevice && viewport.Equals(_lastViewport)) {
                return;
            }

            DisposeResources();

            _spriteBatch = new SpriteBatch(graphicsDevice);
            _graphics = new Graphics(graphicsDevice);

            _lastGraphicsDevice = graphicsDevice;
            _lastViewport = viewport;
        }

        [CanBeNull]
        public SpriteBatch SpriteBatch => _spriteBatch;

        [CanBeNull]
        public Graphics Graphics => _graphics;

        protected override void Dispose(bool disposing) {
            DisposeResources();
        }

        private void DisposeResources() {
            _spriteBatch?.Dispose();
            _graphics?.Dispose();

            _spriteBatch = null;
            _graphics = null;
        }

        [CanBeNull]
        private GraphicsDevice _lastGraphicsDevice;

        private Viewport _lastViewport;

        [CanBeNull]
        private SpriteBatch _spriteBatch;
        [CanBeNull]
        private Graphics _graphics;

    }
}
