using System;
using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;
using StarlightDirector.UI.Rendering.Extensions;
using System.Collections.Generic;
using System.Diagnostics;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditor {

        private void RenderNotes(D2DRenderContext context, Score score) {
            var config = Config;
            var gridArea = GetGridArea(context.ClientSize);
            var radius = config.NoteRadius;
            var noteStartY = (float)ScrollOffsetY;

            DrawNoteConnections(context, score, gridArea, noteStartY, radius);
            DrawNotes(context, score, gridArea, noteStartY, radius);
        }

        private void DrawNotes(D2DRenderContext context, Score score, RectangleF gridArea, float noteStartY, float radius) {
            if (!score.HasAnyNote) {
                return;
            }

            var unit = BarLineSpaceUnit;
            var startPositionFont = _noteStartPositionFont;
            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        if (!IsNoteVisible(note, gridArea, noteStartY, unit, radius)) {
                            continue;
                        }

                        var x = GetNotePositionX(note, gridArea);
                        var y = GetNotePositionY(note, unit, noteStartY);
                        var h = note.Helper;
                        if (h.IsSlide) {
                            DrawSlideNote(context, note, x, y, radius, h.IsSlideMidway);
                        } else if (h.IsHoldStart) {
                            DrawHoldNote(context, note, x, y, radius);
                        } else {
                            if (note.Basic.FlickType != NoteFlickType.None) {
                                DrawFlickNote(context, note, x, y, radius, note.Basic.FlickType);
                            } else {
                                DrawTapNote(context, note, x, y, radius);
                            }
                        }

                        // Indicators

                        // Start position
                        if (note.Basic.StartPosition != note.Basic.FinishPosition) {
                            var startPositionX = x - radius - StartPositionFontSize / 2;
                            var startPositionY = y - radius - StartPositionFontSize / 2;
                            var text = ((int)note.Basic.StartPosition).ToString();
                            context.DrawText(text, _noteCommonFill, startPositionFont, startPositionX, startPositionY);
                        }
                    }
                }
                noteStartY -= numberOfGrids * unit;
            }
        }

        private void DrawNoteConnections(RenderContext context, Score score, RectangleF gridArea, float noteStartY, float radius) {
            if (!score.HasAnyNote) {
                return;
            }

            var unit = BarLineSpaceUnit;
            var scrollOffsetY = ScrollOffsetY;
            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        var x1 = GetNotePositionX(note, gridArea);
                        var y1 = GetNotePositionY(note, unit, noteStartY);
                        var isThisNoteVisible = IsNoteVisible(note, gridArea, noteStartY, unit, radius);
                        Note n2;
                        if (note.Helper.HasNextSync && isThisNoteVisible) {
                            n2 = note.Editor.NextSync;
                            var x2 = GetNotePositionX(n2, gridArea);
                            // Draw sync line
                            context.DrawLine(_syncLineStroke, x1, y1, x2, y1);
                        }
                        n2 = note.Editor.HoldPair;
                        if (n2 != null)
                            if ((isThisNoteVisible || (n2 > note && IsNoteVisible(n2, gridArea, noteStartY, unit, radius)))) {
                                var x2 = GetNotePositionX(n2, gridArea);
                                var y2 = GetNotePositionY(score.Bars, n2.Basic.Bar.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                                // Draw hold line
                                context.DrawLine(_holdLineStroke, x1, y1, x2, y2);
                            }
                        n2 = note.Editor.NextFlick;
                        if (n2 != null && (isThisNoteVisible || IsNoteVisible(n2, gridArea, noteStartY, unit, radius))) {
                            var x2 = GetNotePositionX(n2, gridArea);
                            var y2 = GetNotePositionY(score.Bars, n2.Basic.Bar.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                            // Draw flick line
                            context.DrawLine(_flickLineStroke, x1, y1, x2, y2);
                        }
                        n2 = note.Editor.NextSlide;
                        if (n2 != null && (isThisNoteVisible || IsNoteVisible(n2, gridArea, noteStartY, unit, radius))) {
                            var x2 = GetNotePositionX(n2, gridArea);
                            var y2 = GetNotePositionY(score.Bars, n2.Basic.Bar.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                            // Draw slide line
                            context.DrawLine(_slideLineStroke, x1, y1, x2, y2);
                        }
                    }
                }
                noteStartY -= numberOfGrids * unit;
            }
        }

        private void DrawCommonNoteOutline(RenderContext context, Note note, float x, float y, float r) {
            context.FillCircle(_noteCommonFill, x, y, r);
            context.DrawCircle(_noteCommonStroke, x, y, r);
            if (note.Editor.IsSelected) {
                context.DrawCircle(_noteSelectedStroke, x, y, r * 1.15f);
            }
        }

        private void DrawTapNote(D2DRenderContext context, Note note, float x, float y, float r) {
            DrawCommonNoteOutline(context, note, x, y, r);

            var r1 = r * ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, TapNoteShapeFillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_tapNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
        }

        private void DrawFlickNote(D2DRenderContext context, Note note, float x, float y, float r, NoteFlickType flickType) {
            DrawCommonNoteOutline(context, note, x, y, r);

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

        private void DrawHoldNote(D2DRenderContext context, Note note, float x, float y, float r) {
            DrawCommonNoteOutline(context, note, x, y, r);

            var r1 = r * ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, HoldNoteShapeFillOuterColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_holdNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
            var r2 = r * ScaleFactor3;
            context.FillEllipse(_holdNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
        }

        private void DrawSlideNote(D2DRenderContext context, Note note, float x, float y, float r, bool isMidway) {
            DrawCommonNoteOutline(context, note, x, y, r);

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

        private static bool IsNoteVisible(Note note, RectangleF gridArea, float noteStartY, float u, float r) {
            if (note == null) {
                return false;
            }
            var y = GetNotePositionY(note, u, noteStartY);
            return gridArea.Top - r <= y && y <= gridArea.Bottom + r;
        }

        [DebuggerStepThrough]
        private static D2DLinearGradientBrush GetFillBrush(D2DRenderContext context, float x, float y, float r, Color[] colors) {
            return new D2DLinearGradientBrush(context, new PointF(x, y - r), new PointF(x, y + r), colors);
        }

        [DebuggerStepThrough]
        private static float GetNotePositionX(Note note, RectangleF gridArea) {
            return ((int)note.Basic.FinishPosition - 1) * gridArea.Width / (5 - 1) + gridArea.Left;
        }

        [DebuggerStepThrough]
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

        private static readonly float NoteShapeStrokeWidth = 1;

        private static readonly float ScaleFactor1 = 0.8f;
        private static readonly float ScaleFactor2 = 0.5f;
        private static readonly float ScaleFactor3 = 1 / 3f;
        private static readonly float SlideNoteStrikeHeightFactor = (float)4 / 30;

        private static readonly float Sqrt3 = (float)Math.Sqrt(3);

    }
}
