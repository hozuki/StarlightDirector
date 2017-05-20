﻿using System.Drawing;
using StarlightDirector.UI.Rendering.Direct2D;
using FontStyle = StarlightDirector.UI.Rendering.FontStyle;

namespace StarlightDirector.UI.Controls.Previewing {
    partial class ScorePreviewer {

        protected override void OnCreateResources(D2DRenderContext context) {
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

            _syncIndicatorBrush = new D2DSolidBrush(context, Color.MediumSeaGreen);
            _holdIndicatorBrush = new D2DSolidBrush(context, Color.FromArgb(0xFF, 0xBB, 0x22));
            _flickIndicatorBrush = new D2DSolidBrush(context, Color.FromArgb(0x88, 0xBB, 0xFF));
            _slideIndicatorBrush = new D2DSolidBrush(context, Color.FromArgb(0xE1, 0xA8, 0xFB));

            _avatarFill = new D2DSolidBrush(context, Color.Black);
            _avatarBorderStroke = new D2DPen(context, Color.White, 4);
        }

        protected override void OnDisposeResources(D2DRenderContext context) {
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
        }

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

        private D2DBrush _avatarFill;
        private D2DPen _avatarBorderStroke;

        private static readonly Color[] TapNoteShapeFillColors = { Color.FromArgb(0xFF, 0x99, 0xBB), Color.FromArgb(0xFF, 0x33, 0x66) };
        private static readonly Color[] HoldNoteShapeFillOuterColors = { Color.FromArgb(0xFF, 0xDD, 0x66), Color.FromArgb(0xFF, 0xBB, 0x22) };
        private static readonly Color[] FlickNoteShapeFillOuterColors = { Color.FromArgb(0x88, 0xBB, 0xFF), Color.FromArgb(0x22, 0x55, 0xBB) };
        private static readonly Color[] SlideNoteShapeFillOuterColors = { Color.FromArgb(0xE1, 0xA8, 0xFB), Color.FromArgb(0xA5, 0x46, 0xDA) };
        private static readonly Color[] SlideNoteShapeFillOuterTranslucentColors = { Color.FromArgb(0x80, 0xE1, 0xA8, 0xFB), Color.FromArgb(0x80, 0xA5, 0x46, 0xDA) };


    }
}
