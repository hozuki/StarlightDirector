using System;
using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Rendering.Direct2D;
using StarlightDirector.UI.Rendering.Extensions;
using Brush = StarlightDirector.UI.Rendering.Brush;
using Pen = StarlightDirector.UI.Rendering.Pen;

namespace StarlightDirector.UI.Controls {
    partial class ScoreRenderer {

        private void RenderNotes(D2DRenderContext context, Score score) {
            var config = Config;
            var clientSize = context.ClientSize;
            var barArea = new RectangleF((clientSize.Width - config.EditingAreaWidth) / 2 + (config.LeftAreaWidth + config.GridNumberAreaWidth), 0, config.BarAreaWidth, clientSize.Height);
            var radius = config.NoteRadius;
            var noteStartY = (float)ScrollOffsetY;

            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        var y = noteStartY - note.Basic.IndexInGrid * BarLineSpaceUnit;
                        if (!IsNoteVisible(barArea, y, radius)) {
                            continue;
                        }

                        var x = ((int)note.Basic.FinishPosition - 1) * barArea.Width / (5 - 1) + barArea.Left;
                        var h = note.Helper;
                        if (h.IsSlide) {
                            DrawSlideNote(context, x, y, radius, h.IsSlideMidway);
                        } else if (h.IsHoldStart) {
                            DrawHoldNote(context, x, y, radius);
                        } else {
                            if (note.Basic.FlickType != NoteFlickType.None) {
                                DrawFlickNote(context, x, y, radius, note.Basic.FlickType);
                            } else {
                                DrawTapNote(context, x, y, radius);
                            }
                        }
                    }
                }
                noteStartY -= numberOfGrids * BarLineSpaceUnit;
            }
        }

        private static bool IsNoteVisible(RectangleF barArea, float y, float r) {
            return barArea.Top - r <= y && y <= barArea.Bottom + r;
        }

        private void DrawCommonNoteOutline(D2DRenderContext context, float x, float y, float r) {
            context.FillCircle(_noteCommonFill, x, y, r);
            context.DrawCircle(_noteCommonStroke, x, y, r);
        }

        private void DrawTapNote(D2DRenderContext context, float x, float y, float r) {
            DrawCommonNoteOutline(context, x, y, r);

            var r1 = r * ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, TapNoteShapeFillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_tapNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
        }

        private void DrawFlickNote(D2DRenderContext context, float x, float y, float r, NoteFlickType flickType) {
            DrawCommonNoteOutline(context, x, y, r);

            var r1 = r * ScaleFactor1;
            // Triangle
            var polygon = new PointF[3];
            if (flickType == NoteFlickType.Left) {
                polygon[0] = new PointF(x - r1, y);
                polygon[1] = new PointF(x + r1 / 2, y + r1 / 2 * Sqrt3);
                polygon[2] = new PointF(x + r1 / 2, y - r1 / 2 * Sqrt3);

            } else if (flickType == NoteFlickType.Right) {
                polygon[0] = new PointF(x + r1, y);
                polygon[1] = new PointF(x - r1 / 2, y - r1 / 2 * Sqrt3);
                polygon[2] = new PointF(x - r1 / 2, y + r1 / 2 * Sqrt3);
            }
            using (var fill = GetFillBrush(context, x, y, r, FlickNoteShapeFillOuterColors)) {
                context.FillPolygon(fill, polygon);
            }
            context.DrawPolygon(_flickNoteShapeStroke, polygon);
        }

        private void DrawHoldNote(D2DRenderContext context, float x, float y, float r) {
            DrawCommonNoteOutline(context, x, y, r);

            var r1 = r * ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, HoldNoteShapeFillOuterColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_holdNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
            var r2 = r * ScaleFactor3;
            context.FillEllipse(_holdNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
        }

        private void DrawSlideNote(D2DRenderContext context, float x, float y, float r, bool isMidway) {
            DrawCommonNoteOutline(context, x, y, r);

            var fillColors = isMidway ? SlideNoteShapeFillOuterTranslucentColors : SlideNoteShapeFillOuterColors;
            var r1 = r * ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, fillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            var r2 = r * ScaleFactor3;
            context.FillEllipse(_slideNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
            var l = r * SlideNoteStrikeHeightFactor;
            context.FillRectangle(_slideNoteShapeFillInner, x - r1 - 1, y - l, r1 * 2 + 2, l * 2);
        }

        private static D2DLinearGradientBrush GetFillBrush(D2DRenderContext context, float x, float y, float r, Color[] colors) {
            return new D2DLinearGradientBrush(context, new PointF(x, y - r), new PointF(x, y + r), colors);
        }

        private D2DPen _noteCommonStroke;
        private Brush _noteCommonFill;
        private D2DPen _tapNoteShapeStroke;
        private static readonly Color[] TapNoteShapeFillColors = { Color.FromArgb(0xFF, 0x99, 0xBB), Color.FromArgb(0xFF, 0x33, 0x66) };
        private D2DPen _holdNoteShapeStroke;
        private static readonly Color[] HoldNoteShapeFillOuterColors = { Color.FromArgb(0xFF, 0xDD, 0x66), Color.FromArgb(0xFF, 0xBB, 0x22) };
        private D2DBrush _holdNoteShapeFillInner;
        private D2DPen _flickNoteShapeStroke;
        private static readonly Color[] FlickNoteShapeFillOuterColors = { Color.FromArgb(0x88, 0xBB, 0xFF), Color.FromArgb(0x22, 0x55, 0xBB) };
        private D2DBrush _flickNoteShapeFillInner;
        private static readonly Color[] SlideNoteShapeFillOuterColors = { Color.FromArgb(0xA5, 0x46, 0xDA), Color.FromArgb(0xE1, 0xA8, 0xFB) };
        private static readonly Color[] SlideNoteShapeFillOuterTranslucentColors = { Color.FromArgb(0x80, 0xA5, 0x46, 0xDA), Color.FromArgb(0x80, 0xE1, 0xA8, 0xFB) };
        private D2DBrush _slideNoteShapeFillInner;

        private static readonly float NoteShapeStrokeWidth = 1;

        private static readonly float ScaleFactor1 = 0.8f;
        private static readonly float ScaleFactor2 = 0.5f;
        private static readonly float ScaleFactor3 = 1 / 3f;
        private static readonly float SlideNoteStrikeHeightFactor = (float)4 / 30;

        private static readonly float Sqrt3 = (float)Math.Sqrt(3);

    }
}
