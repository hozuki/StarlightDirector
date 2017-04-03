using SharpDX.DirectWrite;

namespace StarlightDirector.UI.Rendering.Direct2D {
    public sealed class D2DFont : Font {

        public D2DFont(Factory factory, string familyName, float size, FontStyle style, float weight)
            : base(familyName, size, style, weight) {
            Factory = factory;

            var isBold = (style & FontStyle.Bold) != 0;
            var isItalic = (style & FontStyle.Italic) != 0;

            _nativeFont = new TextFormat(factory, familyName, isBold ? FontWeight.Bold : FontWeight.Normal, isItalic ? SharpDX.DirectWrite.FontStyle.Italic : SharpDX.DirectWrite.FontStyle.Normal, size);
        }

        public Factory Factory { get; }

        public TextFormat NativeFont => _nativeFont;

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _nativeFont?.Dispose();
                _nativeFont = null;
            }
        }

        private TextFormat _nativeFont;

    }
}
