using System;
using System.Diagnostics;
using StarlightDirector.UI.Rendering.Direct2D;

namespace StarlightDirector.UI.Rendering.Extensions {
    public static class DrawingComponentExtensions {

        [DebuggerStepThrough]
        public static D2DBrush AsD2DBrush(this Brush brush) {
            var r = brush as D2DBrush;
            if (r == null) {
                throw new InvalidCastException("Expects D2DBrush.");
            }
            return r;
        }

        [DebuggerStepThrough]
        public static D2DPen AsD2DPen(this Pen pen) {
            var r = pen as D2DPen;
            if (r == null) {
                throw new InvalidCastException("Expects D2DPen.");
            }
            return r;
        }

        [DebuggerStepThrough]
        public static D2DImage AsD2DImage(this Image image) {
            var r = image as D2DImage;
            if (r == null) {
                throw new InvalidCastException("Expects D2DImage.");
            }
            return r;
        }

        [DebuggerStepThrough]
        public static D2DFont AsD2DFont(this Font font) {
            var r = font as D2DFont;
            if (r == null) {
                throw new InvalidCastException("Expects D2DFont.");
            }
            return r;
        }

        [DebuggerStepThrough]
        public static D2DRenderContext AsD2DRenderContext(this RenderContext context) {
            var r = context as D2DRenderContext;
            if (r == null) {
                throw new InvalidCastException("Expects D2DRenderContext.");
            }
            return r;
        }

    }
}
