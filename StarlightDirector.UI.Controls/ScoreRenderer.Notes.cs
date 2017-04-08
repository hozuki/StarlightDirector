using System;
using System.Collections;
using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;
using StarlightDirector.UI.Rendering.Extensions;
using Brush = StarlightDirector.UI.Rendering.Brush;
using System.Collections.Generic;

namespace StarlightDirector.UI.Controls {
    partial class ScoreRenderer {

        private void RenderNotes(D2DRenderContext context, Score score) {
            var config = Config;
            var clientSize = context.ClientSize;
            var barArea = new RectangleF((clientSize.Width - config.EditingAreaWidth) / 2 + (config.LeftAreaWidth + config.GridNumberAreaWidth), 0, config.BarAreaWidth, clientSize.Height);
            var radius = config.NoteRadius;
            var noteStartY = (float)ScrollOffsetY;

            DrawNoteConnections(context, score, barArea, noteStartY, radius);
            DrawNotes(context, score, barArea, noteStartY, radius);
        }

        private void DrawNotes(D2DRenderContext context, Score score, RectangleF barArea, float noteStartY, float radius) {
            if (!score.HasAnyNote) {
                return;
            }

            var unit = BarLineSpaceUnit;
            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        if (!IsNoteVisible(note, barArea, noteStartY, unit, radius)) {
                            continue;
                        }

                        var x = GetNotePositionX(note, barArea);
                        var y = GetNotePositionY(note, unit, noteStartY);
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
                noteStartY -= numberOfGrids * unit;
            }
        }

        private void DrawNoteConnections(RenderContext context, Score score, RectangleF barArea, float noteStartY, float radius) {
            if (!score.HasAnyNote) {
                return;
            }

            var unit = BarLineSpaceUnit;
            var scrollOffsetY = ScrollOffsetY;
            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        var x1 = GetNotePositionX(note, barArea);
                        var y1 = GetNotePositionY(note, unit, noteStartY);
                        var isThisNoteVisible = IsNoteVisible(note, barArea, noteStartY, unit, radius);
                        Note n2;
                        if (note.Helper.HasNextSync && isThisNoteVisible) {
                            n2 = note.Editor.NextSync;
                            var x2 = GetNotePositionX(n2, barArea);
                            // Draw sync line
                            context.DrawLine(_syncLineStroke, x1, y1, x2, y1);
                        }
                        n2 = note.Editor.HoldPair;
                        if (n2 != null)
                            if ((isThisNoteVisible || (n2 > note && IsNoteVisible(n2, barArea, noteStartY, unit, radius)))) {
                                var x2 = GetNotePositionX(n2, barArea);
                                var y2 = GetNotePositionY(score.Bars, n2.Basic.Bar.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                                // Draw hold line
                                context.DrawLine(_holdLineStroke, x1, y1, x2, y2);
                            }
                        n2 = note.Editor.NextFlick;
                        if (n2 != null && (isThisNoteVisible || IsNoteVisible(n2, barArea, noteStartY, unit, radius))) {
                            var x2 = GetNotePositionX(n2, barArea);
                            var y2 = GetNotePositionY(score.Bars, n2.Basic.Bar.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                            // Draw flick line
                            context.DrawLine(_flickLineStroke, x1, y1, x2, y2);
                        }
                        n2 = note.Editor.NextSlide;
                        if (n2 != null && (isThisNoteVisible || IsNoteVisible(n2, barArea, noteStartY, unit, radius))) {
                            var x2 = GetNotePositionX(n2, barArea);
                            var y2 = GetNotePositionY(score.Bars, n2.Basic.Bar.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                            // Draw slide line
                            context.DrawLine(_slideLineStroke, x1, y1, x2, y2);
                        }
                    }
                }
                noteStartY -= numberOfGrids * unit;
            }
        }

        private void DrawCommonNoteOutline(RenderContext context, float x, float y, float r) {
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

        private static bool IsNoteVisible(Note note, RectangleF barArea, float noteStartY, float u, float r) {
            if (note == null) {
                return false;
            }
            var y = GetNotePositionY(note, u, noteStartY);
            return barArea.Top - r <= y && y <= barArea.Bottom + r;
        }

        private static D2DLinearGradientBrush GetFillBrush(D2DRenderContext context, float x, float y, float r, Color[] colors) {
            return new D2DLinearGradientBrush(context, new PointF(x, y - r), new PointF(x, y + r), colors);
        }

        private static float GetNotePositionX(Note note, RectangleF barArea) {
            return ((int)note.Basic.FinishPosition - 1) * barArea.Width / (5 - 1) + barArea.Left;
        }

        private static float GetNotePositionY(Note note, float unit, float noteStartY) {
            return noteStartY - unit * note.Basic.IndexInGrid;
        }

        private static float GetNotePositionY(IEnumerable<Bar> bars, int barIndex, int indexInGrid, float unit, float scrollOffsetY) {
            var noteStartY = scrollOffsetY;
            if (barIndex == 0) {
                return noteStartY - unit * indexInGrid;
            }
            var n = 0;
            foreach (var bar in bars) {
                if (n >= barIndex) {
                    break;
                }
                noteStartY -= bar.GetNumberOfGrids() * unit;
                ++n;
            }
            return noteStartY - unit * indexInGrid;
        }

        private D2DPen _noteCommonStroke;
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

        private static readonly Color[] TapNoteShapeFillColors = { Color.FromArgb(0xFF, 0x99, 0xBB), Color.FromArgb(0xFF, 0x33, 0x66) };
        private static readonly Color[] HoldNoteShapeFillOuterColors = { Color.FromArgb(0xFF, 0xDD, 0x66), Color.FromArgb(0xFF, 0xBB, 0x22) };
        private static readonly Color[] FlickNoteShapeFillOuterColors = { Color.FromArgb(0x88, 0xBB, 0xFF), Color.FromArgb(0x22, 0x55, 0xBB) };
        private static readonly Color[] SlideNoteShapeFillOuterColors = { Color.FromArgb(0xA5, 0x46, 0xDA), Color.FromArgb(0xE1, 0xA8, 0xFB) };
        private static readonly Color[] SlideNoteShapeFillOuterTranslucentColors = { Color.FromArgb(0x80, 0xA5, 0x46, 0xDA), Color.FromArgb(0x80, 0xE1, 0xA8, 0xFB) };

        private static readonly float NoteShapeStrokeWidth = 1;

        private static readonly float ScaleFactor1 = 0.8f;
        private static readonly float ScaleFactor2 = 0.5f;
        private static readonly float ScaleFactor3 = 1 / 3f;
        private static readonly float SlideNoteStrikeHeightFactor = (float)4 / 30;

        private static readonly float Sqrt3 = (float)Math.Sqrt(3);

    }
}
