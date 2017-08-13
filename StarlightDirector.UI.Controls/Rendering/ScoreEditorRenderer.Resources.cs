using System.Diagnostics;
using System.Drawing;
using StarlightDirector.UI.Controls.Editing;
using StarlightDirector.UI.Rendering.Direct2D;
using FontStyle = StarlightDirector.UI.Rendering.FontStyle;

namespace StarlightDirector.UI.Controls.Rendering {
    partial class ScoreEditorRenderer {

        internal void CreateResources(D2DRenderContext context, Font controlFont) {
            _barSelectedOutlinePen = new D2DPen(context, Color.White, 2);
            _barGridOutlinePen = new D2DPen(context, Color.Gray, 1);
            _barNormalGridPen = new D2DPen(context, Color.Gray, 1);
            _barGridStartBeatPen = new D2DPen(context, Color.Red, 1);
            _barPrimaryBeatPen = new D2DPen(context, Color.Yellow, 1);
            _barSecondaryBeatPen = new D2DPen(context, Color.Violet, 1);

            _gridNumberBrush = new D2DSolidBrush(context, Color.White);

            _gridNumberFont = new D2DFont(context.DirectWriteFactory, controlFont.Name, controlFont.SizeInPoints, FontStyle.Regular, 10);
            _gridNumberBoldFont = new D2DFont(context.DirectWriteFactory, _gridNumberFont.FamilyName, _gridNumberFont.Size, FontStyle.Bold, _gridNumberFont.Weight);
            _barInfoFont = new D2DFont(context.DirectWriteFactory, controlFont.Name, controlFont.SizeInPoints * 1.5f, FontStyle.Regular, 10);
            _barInfoBoldFont = new D2DFont(context.DirectWriteFactory, _barInfoFont.FamilyName, _barInfoFont.Size, FontStyle.Bold, _barInfoFont.Weight);

            _noteCommonStroke = new D2DPen(context, Color.FromArgb(0x22, 0x22, 0x22), Definitions.NoteShapeStrokeWidth);
            _noteSelectedStroke = new D2DPen(context, Color.FromArgb(0x7F, 0xFF, 0x7F), Definitions.NoteShapeStrokeWidth * 3);
            _tapNoteShapeStroke = new D2DPen(context, Color.FromArgb(0xFF, 0x33, 0x66), Definitions.NoteShapeStrokeWidth);
            _holdNoteShapeStroke = new D2DPen(context, Color.FromArgb(0xFF, 0xBB, 0x22), Definitions.NoteShapeStrokeWidth);
            _flickNoteShapeStroke = new D2DPen(context, Color.FromArgb(0x22, 0x55, 0xBB), Definitions.NoteShapeStrokeWidth);

            _noteCommonFill = new D2DSolidBrush(context, Color.White);
            _holdNoteShapeFillInner = new D2DSolidBrush(context, Color.White);
            _flickNoteShapeFillInner = new D2DSolidBrush(context, Color.White);
            _slideNoteShapeFillInner = new D2DSolidBrush(context, Color.White);

            _syncLineStroke = new D2DPen(context, Color.FromArgb(0xA0, 0xA0, 0xA0), 4);
            _holdLineStroke = new D2DPen(context, Color.FromArgb(0xA0, 0xA0, 0xA0), 10);
            _flickLineStroke = new D2DPen(context, Color.FromArgb(0xA0, 0xA0, 0xA0), 14.1f);
            _slideLineStroke = new D2DPen(context, Color.FromArgb(0xA0, 0xA0, 0xA0), 14.1f);

            _syncIndicatorBrush = new D2DSolidBrush(context, Color.MediumSeaGreen);
            _holdIndicatorBrush = new D2DSolidBrush(context, Color.FromArgb(0xFF, 0xBB, 0x22));
            _flickIndicatorBrush = new D2DSolidBrush(context, Color.FromArgb(0x88, 0xBB, 0xFF));
            _slideIndicatorBrush = new D2DSolidBrush(context, Color.FromArgb(0xE1, 0xA8, 0xFB));

            _noteStartPositionFont = new D2DFont(context.DirectWriteFactory, controlFont.Name, Definitions.StartPositionFontSize, FontStyle.Regular, 10);

            _specialNoteStroke = new D2DPen(context, Color.FromArgb(0xAA, 0xAA, 0xAA), 2);
            _specialNoteFill = new D2DSolidBrush(context, Color.FromArgb(0x7F, 0xAA, 0xAA, 0xAA));
            _specialNoteTextBrush = new D2DSolidBrush(context, Color.White);
            _specialNoteDescriptionFont = new D2DFont(context.DirectWriteFactory, controlFont.Name, controlFont.SizeInPoints, FontStyle.Regular, 10);

            _selectionRectStroke = new D2DPen(context, Color.White, 5);
        }

        internal void DisposeResources(D2DRenderContext context) {
            _barSelectedOutlinePen?.Dispose();
            _barNormalGridPen?.Dispose();
            _barPrimaryBeatPen?.Dispose();
            _barSecondaryBeatPen?.Dispose();
            _barGridStartBeatPen?.Dispose();
            _barGridOutlinePen?.Dispose();

            _gridNumberBrush?.Dispose();

            _gridNumberFont?.Dispose();
            _gridNumberBoldFont?.Dispose();
            _barInfoFont?.Dispose();
            _barInfoBoldFont?.Dispose();

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

            _noteStartPositionFont?.Dispose();

            _specialNoteStroke?.Dispose();
            _specialNoteFill?.Dispose();
            _specialNoteTextBrush?.Dispose();
            _specialNoteDescriptionFont?.Dispose();

            _selectionRectStroke?.Dispose();
        }

        [DebuggerStepThrough]
        private static D2DLinearGradientBrush GetFillBrush(D2DRenderContext context, float x, float y, float r, Color[] colors) {
            return new D2DLinearGradientBrush(context, new PointF(x, y - r), new PointF(x, y + r), colors);
        }

        // Bar resources

        private D2DPen _barSelectedOutlinePen;
        private D2DPen _barGridOutlinePen;
        private D2DPen _barGridStartBeatPen;
        private D2DPen _barPrimaryBeatPen;
        private D2DPen _barSecondaryBeatPen;
        private D2DPen _barNormalGridPen;

        private D2DBrush _gridNumberBrush;

        private D2DFont _gridNumberFont;
        private D2DFont _gridNumberBoldFont;
        private D2DFont _barInfoFont;
        private D2DFont _barInfoBoldFont;

        // Note resources

        private D2DPen _noteCommonStroke;
        private D2DPen _noteSelectedStroke;
        private D2DPen _tapNoteShapeStroke;
        private D2DPen _holdNoteShapeStroke;
        private D2DPen _flickNoteShapeStroke;

        private D2DPen _syncLineStroke;
        private D2DPen _holdLineStroke;
        private D2DPen _flickLineStroke;
        private D2DPen _slideLineStroke;

        private D2DBrush _noteCommonFill;
        private D2DBrush _holdNoteShapeFillInner;
        private D2DBrush _flickNoteShapeFillInner;
        private D2DBrush _slideNoteShapeFillInner;

        private D2DBrush _syncIndicatorBrush;
        private D2DBrush _holdIndicatorBrush;
        private D2DBrush _flickIndicatorBrush;
        private D2DBrush _slideIndicatorBrush;

        private D2DFont _noteStartPositionFont;

        private D2DPen _specialNoteStroke;
        private D2DBrush _specialNoteFill;
        private D2DBrush _specialNoteTextBrush;
        private D2DFont _specialNoteDescriptionFont;

        private D2DPen _selectionRectStroke;

        private static readonly Color[] TapNoteShapeFillColors = { Color.FromArgb(0xFF, 0x99, 0xBB), Color.FromArgb(0xFF, 0x33, 0x66) };
        private static readonly Color[] HoldNoteShapeFillOuterColors = { Color.FromArgb(0xFF, 0xDD, 0x66), Color.FromArgb(0xFF, 0xBB, 0x22) };
        private static readonly Color[] FlickNoteShapeFillOuterColors = { Color.FromArgb(0x88, 0xBB, 0xFF), Color.FromArgb(0x22, 0x55, 0xBB) };
        private static readonly Color[] SlideNoteShapeFillOuterColors = { Color.FromArgb(0xE1, 0xA8, 0xFB), Color.FromArgb(0xA5, 0x46, 0xDA) };
        private static readonly Color[] SlideNoteShapeFillOuterTranslucentColors = { Color.FromArgb(0x80, 0xE1, 0xA8, 0xFB), Color.FromArgb(0x80, 0xA5, 0x46, 0xDA) };

    }
}
