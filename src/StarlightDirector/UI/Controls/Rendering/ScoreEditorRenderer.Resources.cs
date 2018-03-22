using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using MonoGame.Extended.Overlay.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls.Editing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    partial class ScoreEditorRenderer {

        internal void CreateResources([NotNull] Font controlFont) {
            _barSelectedOutlinePen = new Pen(Color.White, 2);
            _barGridOutlinePen = new Pen(Color.Gray, 1);
            _barNormalGridPen = new Pen(Color.Gray, 1);
            _barGridStartBeatPen = new Pen(Color.Red, 1);
            _barPrimaryBeatPen = new Pen(Color.Yellow, 1);
            _barSecondaryBeatPen = new Pen(Color.Violet, 1);

            _gridNumberBrush = new SolidBrush(Color.White);

            _gridNumberFont = controlFont.CreateFontVariance(FontStyle.Regular);
            _gridNumberBoldFont = controlFont.CreateFontVariance(FontStyle.Bold);
            _barInfoFont = controlFont.CreateFontVariance(controlFont.Size * 1.2f, FontStyle.Bold);
            _barInfoBoldFont = controlFont.CreateFontVariance(FontStyle.Bold);

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

            _noteStartPositionFont = controlFont.CreateFontVariance(Definitions.StartPositionFontSize);

            _specialNoteStroke = new Pen(Color.FromNonPremultiplied(0xAA, 0xAA, 0xAA, 0xFF), 2);
            _specialNoteFill = new SolidBrush(Color.FromNonPremultiplied(0xAA, 0xAA, 0xAA, 0x7F));
            _specialNoteTextBrush = new SolidBrush(Color.White);
            _specialNoteDescriptionFont = controlFont.Clone();

            _selectionRectStroke = new Pen(Color.White, 5);

            _timeLineStroke = new Pen(Color.LightCyan, 4);
            _timeInfoBrush = new SolidBrush(Color.White);
            _timeInfoFont = controlFont.CreateFontVariance(Definitions.TimeInfoFontSize);
        }

        internal void DisposeResources() {
            _barSelectedOutlinePen?.Dispose();
            _barNormalGridPen?.Dispose();
            _barPrimaryBeatPen?.Dispose();
            _barSecondaryBeatPen?.Dispose();
            _barGridStartBeatPen?.Dispose();
            _barGridOutlinePen?.Dispose();

            _gridNumberBrush?.Dispose();

            //_gridNumberFont?.Dispose();
            //_gridNumberBoldFont?.Dispose();
            //_barInfoFont?.Dispose();
            //_barInfoBoldFont?.Dispose();

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

            //_noteStartPositionFont?.Dispose();

            _specialNoteStroke?.Dispose();
            _specialNoteFill?.Dispose();
            _specialNoteTextBrush?.Dispose();
            //_specialNoteDescriptionFont?.Dispose();

            _selectionRectStroke?.Dispose();

            _timeLineStroke?.Dispose();
            _timeInfoBrush?.Dispose();
            //_timeInfoFont?.Dispose();
        }

        [DebuggerStepThrough]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static LinearGradientBrush GetFillBrush(float x, float y, float r, [NotNull] Color[] colors) {
            return new LinearGradientBrush(new Vector2(x, y - r), new Vector2(x, y + r), colors);
        }

        // Bar resources

        private Pen _barSelectedOutlinePen;
        private Pen _barGridOutlinePen;
        private Pen _barGridStartBeatPen;
        private Pen _barPrimaryBeatPen;
        private Pen _barSecondaryBeatPen;
        private Pen _barNormalGridPen;

        private Brush _gridNumberBrush;

        private Font _gridNumberFont;
        private Font _gridNumberBoldFont;
        private Font _barInfoFont;
        private Font _barInfoBoldFont;

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

        private Font _noteStartPositionFont;

        private Pen _specialNoteStroke;
        private Brush _specialNoteFill;
        private Brush _specialNoteTextBrush;
        private Font _specialNoteDescriptionFont;

        // Selection

        private Pen _selectionRectStroke;

        // Previewing

        private Pen _timeLineStroke;
        private Brush _timeInfoBrush;
        private Font _timeInfoFont;

        private static readonly Color[] TapNoteShapeFillColors = { Color.FromNonPremultiplied(0xFF, 0x99, 0xBB, 0xFF), Color.FromNonPremultiplied(0xFF, 0x33, 0x66, 0xFF) };
        private static readonly Color[] HoldNoteShapeFillOuterColors = { Color.FromNonPremultiplied(0xFF, 0xDD, 0x66, 0xFF), Color.FromNonPremultiplied(0xFF, 0xBB, 0x22, 0xFF) };
        private static readonly Color[] FlickNoteShapeFillOuterColors = { Color.FromNonPremultiplied(0x88, 0xBB, 0xFF, 0xFF), Color.FromNonPremultiplied(0x22, 0x55, 0xBB, 0xFF) };
        private static readonly Color[] SlideNoteShapeFillOuterColors = { Color.FromNonPremultiplied(0xE1, 0xA8, 0xFB, 0xFF), Color.FromNonPremultiplied(0xA5, 0x46, 0xDA, 0xFF) };
        private static readonly Color[] SlideNoteShapeFillOuterTranslucentColors = { Color.FromNonPremultiplied(0xE1, 0xA8, 0xFB, 0x80), Color.FromNonPremultiplied(0xA5, 0x46, 0xDA, 0x80) };


    }
}
