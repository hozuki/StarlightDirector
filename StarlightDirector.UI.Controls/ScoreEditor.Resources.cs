﻿using System.Drawing;
using StarlightDirector.UI.Rendering.Direct2D;
using FontStyle = StarlightDirector.UI.Rendering.FontStyle;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        protected override void OnCreateResources(D2DRenderContext context) {
            _barSelectedOutlinePen = new D2DPen(context, Color.White, 2);
            _barGridOutlinePen = new D2DPen(context, Color.Gray, 1);
            _barNormalGridPen = new D2DPen(context, Color.Gray, 1);
            _barGridStartBeatPen = new D2DPen(context, Color.Red, 1);
            _barPrimaryBeatPen = new D2DPen(context, Color.Yellow, 1);
            _barSecondaryBeatPen = new D2DPen(context, Color.Violet, 1);

            _gridNumberBrush = new D2DSolidBrush(context, Color.White);

            _scoreBarFont = new D2DFont(context.DirectWriteFactory, Font.Name, Font.SizeInPoints, FontStyle.Regular, 10);
            _scoreBarBoldFont = new D2DFont(context.DirectWriteFactory, _scoreBarFont.FamilyName, _scoreBarFont.Size, FontStyle.Bold, _scoreBarFont.Weight);

            _noteCommonStroke = new D2DPen(context, Color.FromArgb(0x22, 0x22, 0x22), NoteShapeStrokeWidth);
            _noteSelectedStroke = new D2DPen(context, Color.FromArgb(0x7F, 0xFF, 0x7F), NoteShapeStrokeWidth * 3);
            _tapNoteShapeStroke = new D2DPen(context, Color.FromArgb(0xFF, 0x33, 0x66), NoteShapeStrokeWidth);
            _holdNoteShapeStroke = new D2DPen(context, Color.FromArgb(0xFF, 0xBB, 0x22), NoteShapeStrokeWidth);
            _flickNoteShapeStroke = new D2DPen(context, Color.FromArgb(0x22, 0x55, 0xBB), NoteShapeStrokeWidth);

            _noteCommonFill = new D2DSolidBrush(context, Color.White);
            _holdNoteShapeFillInner = new D2DSolidBrush(context, Color.White);
            _flickNoteShapeFillInner = new D2DSolidBrush(context, Color.White);
            _slideNoteShapeFillInner = new D2DSolidBrush(context, Color.White);

            _syncLineStroke = new D2DPen(context, Color.FromArgb(0xA0, 0xA0, 0xA0), 4);
            _holdLineStroke = new D2DPen(context, Color.FromArgb(0xA0, 0xA0, 0xA0), 10);
            _flickLineStroke = new D2DPen(context, Color.FromArgb(0xA0, 0xA0, 0xA0), 14.1f);
            _slideLineStroke = new D2DPen(context, Color.FromArgb(0xA0, 0xA0, 0xA0), 14.1f);

            _noteStartPositionFont = new D2DFont(context.DirectWriteFactory, Font.Name, StartPositionFontSize, FontStyle.Regular, 10);
        }

        protected override void OnDisposeResources(D2DRenderContext context) {
            _barSelectedOutlinePen?.Dispose();
            _barNormalGridPen?.Dispose();
            _barPrimaryBeatPen?.Dispose();
            _barSecondaryBeatPen?.Dispose();
            _barGridStartBeatPen?.Dispose();
            _barGridOutlinePen?.Dispose();

            _gridNumberBrush?.Dispose();

            _scoreBarFont?.Dispose();
            _scoreBarBoldFont?.Dispose();

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

            _noteStartPositionFont?.Dispose();
        }

        // Bar resources

        private D2DPen _barSelectedOutlinePen;
        private D2DPen _barGridOutlinePen;
        private D2DPen _barGridStartBeatPen;
        private D2DPen _barPrimaryBeatPen;
        private D2DPen _barSecondaryBeatPen;
        private D2DPen _barNormalGridPen;

        private D2DBrush _gridNumberBrush;

        private D2DFont _scoreBarFont;
        private D2DFont _scoreBarBoldFont;

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

        private D2DFont _noteStartPositionFont;
        private static readonly float StartPositionFontSize = 10;

        private static readonly Color[] TapNoteShapeFillColors = { Color.FromArgb(0xFF, 0x99, 0xBB), Color.FromArgb(0xFF, 0x33, 0x66) };
        private static readonly Color[] HoldNoteShapeFillOuterColors = { Color.FromArgb(0xFF, 0xDD, 0x66), Color.FromArgb(0xFF, 0xBB, 0x22) };
        private static readonly Color[] FlickNoteShapeFillOuterColors = { Color.FromArgb(0x88, 0xBB, 0xFF), Color.FromArgb(0x22, 0x55, 0xBB) };
        private static readonly Color[] SlideNoteShapeFillOuterColors = { Color.FromArgb(0xA5, 0x46, 0xDA), Color.FromArgb(0xE1, 0xA8, 0xFB) };
        private static readonly Color[] SlideNoteShapeFillOuterTranslucentColors = { Color.FromArgb(0x80, 0xA5, 0x46, 0xDA), Color.FromArgb(0x80, 0xE1, 0xA8, 0xFB) };

    }
}
