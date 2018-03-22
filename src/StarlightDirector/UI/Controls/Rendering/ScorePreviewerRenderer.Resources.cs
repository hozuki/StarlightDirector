using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.UI.Controls.Previewing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    partial class ScorePreviewerRenderer {

        internal void CreateResources(Rectangle clientRectangle) {
            _noteCommonStroke = new Pen(Color.FromNonPremultiplied(0x22, 0x22, 0x22, 0xFF), Definitions.NoteShapeStrokeWidth);
            _noteSelectedStroke = new Pen(Color.FromNonPremultiplied(0x7F, 0xFF, 0x7F, 0xFF), Definitions.NoteShapeStrokeWidth * 3);
            _tapNoteShapeStroke = new Pen(Color.FromNonPremultiplied(0xFF, 0x33, 0x66, 0xFF), Definitions.NoteShapeStrokeWidth);
            _holdNoteShapeStroke = new Pen(Color.FromNonPremultiplied(0xFF, 0xBB, 0x22, 0xFF), Definitions.NoteShapeStrokeWidth);
            _flickNoteShapeStroke = new Pen(Color.FromNonPremultiplied(0x22, 0x55, 0xBB, 0xFF), Definitions.NoteShapeStrokeWidth);

            _noteCommonFill = new SolidBrush(Color.White);
            _holdNoteShapeFillInner = new SolidBrush(Color.White);
            _flickNoteShapeFillInner = new SolidBrush(Color.White);
            _slideNoteShapeFillInner = new SolidBrush(Color.White);

            _syncLineStroke = new Pen(Color.FromNonPremultiplied(0xA0, 0xA0, 0xA0, 0xFF), 4);
            _holdLineStroke = new Pen(Color.FromNonPremultiplied(0xA0, 0xA0, 0xA0, 0xFF), 10);
            _flickLineStroke = new Pen(Color.FromNonPremultiplied(0xA0, 0xA0, 0xA0, 0xFF), 14.1f);
            _slideLineStroke = new Pen(Color.FromNonPremultiplied(0xA0, 0xA0, 0xA0, 0xFF), 14.1f);

            _syncIndicatorBrush = new SolidBrush(Color.MediumSeaGreen);
            _holdIndicatorBrush = new SolidBrush(Color.FromNonPremultiplied(0xFF, 0xBB, 0x22, 0xFF));
            _flickIndicatorBrush = new SolidBrush(Color.FromNonPremultiplied(0x88, 0xBB, 0xFF, 0xFF));
            _slideIndicatorBrush = new SolidBrush(Color.FromNonPremultiplied(0xE1, 0xA8, 0xFB, 0xFF));

            _avatarFill = new SolidBrush(Color.Black);
            _avatarBorderStroke = new Pen(Color.White, 4);

            _ribbonBrush = new LinearGradientBrush(new Vector2(clientRectangle.Height, 0), new Vector2(0, clientRectangle.Height), RibbonColors);
        }

        internal void DisposeResources() {
            _noteCommonStroke?.Dispose();
            _noteSelectedStroke?.Dispose();
            _tapNoteShapeStroke?.Dispose();
            _holdNoteShapeStroke?.Dispose();
            _flickNoteShapeStroke?.Dispose();

            _noteCommonFill?.Dispose();
            _holdNoteShapeFillInner?.Dispose();
            _flickNoteShapeFillInner?.Dispose();
            _slideNoteShapeFillInner?.Dispose();

            _syncLineStroke?.Dispose();
            _holdLineStroke?.Dispose();
            _flickLineStroke?.Dispose();
            _slideLineStroke?.Dispose();

            _syncIndicatorBrush?.Dispose();
            _holdIndicatorBrush?.Dispose();
            _flickIndicatorBrush?.Dispose();
            _slideIndicatorBrush?.Dispose();

            _avatarFill?.Dispose();
            _avatarBorderStroke?.Dispose();

            _ribbonBrush?.Dispose();
        }

        // Note resources

        private Pen _noteCommonStroke;
        private Pen _noteSelectedStroke;
        private Pen _tapNoteShapeStroke;
        private Pen _holdNoteShapeStroke;
        private Pen _flickNoteShapeStroke;

        private Pen _syncLineStroke;
        private Pen _holdLineStroke;
        private Pen _flickLineStroke;
        private Pen _slideLineStroke;

        private Brush _noteCommonFill;
        private Brush _holdNoteShapeFillInner;
        private Brush _flickNoteShapeFillInner;
        private Brush _slideNoteShapeFillInner;

        private Brush _syncIndicatorBrush;
        private Brush _holdIndicatorBrush;
        private Brush _flickIndicatorBrush;
        private Brush _slideIndicatorBrush;

        private Brush _avatarFill;
        private Pen _avatarBorderStroke;

        private LinearGradientBrush _ribbonBrush;

        private static readonly Color[] RibbonColors = {
            Color.FromNonPremultiplied(0xFF, 0xFF, 0xFF, 0x7F),
            Color.FromNonPremultiplied(0xFF, 0xFF, 0xFF, 0xFF)
        };

        private static readonly Color[] TapNoteShapeFillColors = { Color.FromNonPremultiplied(0xFF, 0x99, 0xBB, 0xFF), Color.FromNonPremultiplied(0xFF, 0x33, 0x66, 0xFF) };
        private static readonly Color[] HoldNoteShapeFillOuterColors = { Color.FromNonPremultiplied(0xFF, 0xDD, 0x66, 0xFF), Color.FromNonPremultiplied(0xFF, 0xBB, 0x22, 0xFF) };
        private static readonly Color[] FlickNoteShapeFillOuterColors = { Color.FromNonPremultiplied(0x88, 0xBB, 0xFF, 0xFF), Color.FromNonPremultiplied(0x22, 0x55, 0xBB, 0xFF) };
        private static readonly Color[] SlideNoteShapeFillOuterColors = { Color.FromNonPremultiplied(0xE1, 0xA8, 0xFB, 0xFF), Color.FromNonPremultiplied(0xA5, 0x46, 0xDA, 0xFF) };
        private static readonly Color[] SlideNoteShapeFillOuterTranslucentColors = { Color.FromNonPremultiplied(0xE1, 0xA8, 0xFB, 0x80), Color.FromNonPremultiplied(0xA5, 0x46, 0xDA, 0x80) };

    }
}
