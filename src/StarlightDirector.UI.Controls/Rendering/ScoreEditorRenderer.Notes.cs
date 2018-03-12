using System;
using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Editing;
using StarlightDirector.UI.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;
using StarlightDirector.UI.Rendering.Extensions;

namespace StarlightDirector.UI.Controls.Rendering {
    partial class ScoreEditorRenderer {

        private void RenderNotes(D2DRenderContext context, Score score, ScoreEditorConfig config, ScoreEditorLook look, float scrollOffsetY) {
            var gridArea = ScoreEditorLayout.GetGridArea(config, context.ClientSize);
            var radius = config.NoteRadius;
            var specialNoteArea = ScoreEditorLayout.GetSpecialNoteArea(config, context.ClientSize);

            DrawNoteConnections(context, score, gridArea, config, look, scrollOffsetY, scrollOffsetY, radius);
            DrawNotes(context, score, gridArea, config, look, specialNoteArea, scrollOffsetY, radius);
        }

        private void DrawNotes(D2DRenderContext context, Score score, RectangleF gridArea, ScoreEditorConfig config, ScoreEditorLook look, RectangleF specialNoteArea, float noteStartY, float radius) {
            if (!score.HasAnyNote) {
                return;
            }

            var unit = look.BarLineSpaceUnit;
            var startPositionFont = _noteStartPositionFont;
            var shouldDrawIndicators = look.IndicatorsVisible;
            var numColumns = config.NumberOfColumns;
            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.Helper.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        if (!ScoreEditorLayout.IsNoteVisible(note, gridArea, noteStartY, unit, radius)) {
                            continue;
                        }

                        if (note.Helper.IsGaming) {
                            var x = ScoreEditorLayout.GetNotePositionX(note, gridArea, numColumns);
                            var y = ScoreEditorLayout.GetNotePositionY(note, unit, noteStartY);
                            var h = note.Helper;
                            if (h.IsSlide) {
                                if (h.HasNextFlick) {
                                    DrawFlickNote(context, note, x, y, radius, note.Basic.FlickType);
                                } else {
                                    DrawSlideNote(context, note, x, y, radius, h.IsSlideMidway);
                                }
                            } else if (h.IsHoldStart) {
                                DrawHoldNote(context, note, x, y, radius);
                            } else {
                                if (note.Basic.FlickType != NoteFlickType.None) {
                                    DrawFlickNote(context, note, x, y, radius, note.Basic.FlickType);
                                } else if (h.IsHoldEnd) {
                                    DrawHoldNote(context, note, x, y, radius);
                                } else {
                                    DrawTapNote(context, note, x, y, radius);
                                }
                            }

                            // Indicators
                            if (shouldDrawIndicators) {
                                if (note.Helper.IsSync) {
                                    context.FillCircle(_syncIndicatorBrush, x + radius, y - radius, Definitions.IndicatorRadius);
                                }
                                if (note.Helper.IsHold) {
                                    context.FillCircle(_holdIndicatorBrush, x - radius, y + radius, Definitions.IndicatorRadius);
                                }
                                if (note.Helper.IsSlide) {
                                    context.FillCircle(_slideIndicatorBrush, x + radius, y + radius, Definitions.IndicatorRadius);
                                } else if (note.Helper.IsFlick) {
                                    context.FillCircle(_flickIndicatorBrush, x + radius, y + radius, Definitions.IndicatorRadius);
                                }
                            }

                            // Start position
                            if (note.Basic.StartPosition != note.Basic.FinishPosition) {
                                var startPositionX = x - radius - Definitions.StartPositionFontSize / 2;
                                var startPositionY = y - radius - Definitions.StartPositionFontSize / 2;
                                var text = ((int)note.Basic.StartPosition).ToString();
                                context.DrawText(text, _noteCommonFill, startPositionFont, startPositionX, startPositionY);
                            }
                        } else if (note.Helper.IsSpecial) {
                            // Draw a special note (a rectangle)
                            var left = specialNoteArea.Left + radius * 4;
                            var gridY = ScoreEditorLayout.GetNotePositionY(note, unit, noteStartY);
                            var top = gridY - radius * Definitions.SpecialNoteHeightFactor;
                            var width = specialNoteArea.Right - left;
                            var height = radius * 2 * Definitions.SpecialNoteHeightFactor;
                            context.DrawRectangle(_specialNoteStroke, left, top, width, height);
                            context.FillRectangle(_specialNoteFill, left, top, width, height);

                            string specialNoteText;
                            switch (note.Basic.Type) {
                                case NoteType.VariantBpm:
                                    specialNoteText = $"BPM: {note.Params.NewBpm:0.00}";
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException(nameof(note.Basic.Type), note.Basic.Type, null);
                            }
                            var rect = context.MeasureText(specialNoteText, _specialNoteDescriptionFont);
                            var textLeft = left + 2;
                            var textTop = gridY - rect.Height / 2;
                            context.DrawText(specialNoteText, _specialNoteTextBrush, _specialNoteDescriptionFont, textLeft, textTop);
                        }
                    }
                }
                noteStartY -= numberOfGrids * unit;
            }
        }

        private void DrawNoteConnections(RenderContext context, Score score, RectangleF gridArea, ScoreEditorConfig config, ScoreEditorLook look, float scrollOffsetY, float noteStartY, float radius) {
            if (!score.HasAnyNote) {
                return;
            }

            var unit = look.BarLineSpaceUnit;
            var numColumns = config.NumberOfColumns;
            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();
                if (bar.Helper.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        var thisStatus = ScoreEditorLayout.GetNoteOnStageStatus(note, gridArea, noteStartY, unit, radius);
                        var x1 = ScoreEditorLayout.GetNotePositionX(note, gridArea, numColumns);
                        var y1 = ScoreEditorLayout.GetNotePositionY(note, unit, noteStartY);
                        if (note.Helper.HasNextSync && thisStatus == OnStageStatus.OnStage) {
                            var n2 = note.Editor.NextSync;
                            var x2 = ScoreEditorLayout.GetNotePositionX(n2, gridArea, numColumns);
                            // Draw sync line
                            context.DrawLine(_syncLineStroke, x1, y1, x2, y1);
                        }
                        if (note.Helper.IsHoldStart) {
                            var n2 = note.Editor.HoldPair;
                            var s2 = noteStartY;
                            // We promise only calculate the latter note, so this method works.
                            if (note.Basic.Bar != n2.Basic.Bar) {
                                for (var i = note.Basic.Bar.Basic.Index; i < n2.Basic.Bar.Basic.Index; ++i) {
                                    s2 -= score.Bars[i].GetNumberOfGrids() * unit;
                                }
                            }
                            var thatStatus = ScoreEditorLayout.GetNoteOnStageStatus(n2, gridArea, s2, unit, radius);
                            if ((int)thisStatus * (int)thatStatus <= 0) {
                                var x2 = ScoreEditorLayout.GetNotePositionX(n2, gridArea, numColumns);
                                var y2 = ScoreEditorLayout.GetNotePositionY(score.Bars, n2.Basic.Bar.Basic.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                                // Draw hold line
                                context.DrawLine(_holdLineStroke, x1, y1, x2, y2);
                            }
                        }
                        if (note.Helper.HasNextFlick) {
                            var n2 = note.Editor.NextFlick;
                            var s2 = noteStartY;
                            if (note.Basic.Bar != n2.Basic.Bar) {
                                for (var i = note.Basic.Bar.Basic.Index; i < n2.Basic.Bar.Basic.Index; ++i) {
                                    s2 -= score.Bars[i].GetNumberOfGrids() * unit;
                                }
                            }
                            var thatStatus = ScoreEditorLayout.GetNoteOnStageStatus(n2, gridArea, s2, unit, radius);
                            if ((int)thisStatus * (int)thatStatus <= 0) {
                                var x2 = ScoreEditorLayout.GetNotePositionX(n2, gridArea, numColumns);
                                var y2 = ScoreEditorLayout.GetNotePositionY(score.Bars, n2.Basic.Bar.Basic.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                                // Draw flick line
                                context.DrawLine(_flickLineStroke, x1, y1, x2, y2);
                            }
                        }
                        if (note.Helper.HasNextSlide) {
                            var n2 = note.Editor.NextSlide;
                            var s2 = noteStartY;
                            if (note.Basic.Bar != n2.Basic.Bar) {
                                for (var i = note.Basic.Bar.Basic.Index; i < n2.Basic.Bar.Basic.Index; ++i) {
                                    s2 -= score.Bars[i].GetNumberOfGrids() * unit;
                                }
                            }
                            var thatStatus = ScoreEditorLayout.GetNoteOnStageStatus(n2, gridArea, s2, unit, radius);
                            if ((int)thisStatus * (int)thatStatus <= 0) {
                                var x2 = ScoreEditorLayout.GetNotePositionX(n2, gridArea, numColumns);
                                var y2 = ScoreEditorLayout.GetNotePositionY(score.Bars, n2.Basic.Bar.Basic.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);
                                // Draw slide line
                                context.DrawLine(_slideLineStroke, x1, y1, x2, y2);
                            }
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
                context.DrawCircle(_noteSelectedStroke, x, y, r * Definitions.ScaleFactor0);
            }
        }

        private void DrawTapNote(D2DRenderContext context, Note note, float x, float y, float r) {
            DrawCommonNoteOutline(context, note, x, y, r);

            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, TapNoteShapeFillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_tapNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
        }

        private void DrawFlickNote(D2DRenderContext context, Note note, float x, float y, float r, NoteFlickType flickType) {
            DrawCommonNoteOutline(context, note, x, y, r);

            switch (flickType) {
                case NoteFlickType.Left:
                case NoteFlickType.Right:
                    break;
                default:
                    if (note.Helper.IsSlide && note.Helper.HasNextFlick) {
                        flickType = note.Editor.NextFlick.Basic.FinishPosition > note.Basic.FinishPosition ? NoteFlickType.Right : NoteFlickType.Left;
                    } else {
                        throw new ArgumentOutOfRangeException(nameof(flickType), "Unknown flick type for flick note rendering.");
                    }
                    break;
            }

            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r1, FlickNoteShapeFillOuterColors)) {
                context.FillCircle(fill, x, y, r1);
            }
            context.DrawCircle(_flickNoteShapeStroke, x, y, r1);

            var r3 = r * Definitions.ScaleFactor2;
            // Triangle
            var polygon = new PointF[3];
            switch (flickType) {
                case NoteFlickType.Left:
                    polygon[0] = new PointF(x - r3, y);
                    polygon[1] = new PointF(x + r3 / 2, y + r3 / 2 * Definitions.Sqrt3);
                    polygon[2] = new PointF(x + r3 / 2, y - r3 / 2 * Definitions.Sqrt3);
                    break;
                case NoteFlickType.Right:
                    polygon[0] = new PointF(x + r3, y);
                    polygon[1] = new PointF(x - r3 / 2, y - r3 / 2 * Definitions.Sqrt3);
                    polygon[2] = new PointF(x - r3 / 2, y + r3 / 2 * Definitions.Sqrt3);
                    break;
            }
            context.FillPolygon(_flickNoteShapeFillInner, polygon);
        }

        private void DrawHoldNote(D2DRenderContext context, Note note, float x, float y, float r) {
            DrawCommonNoteOutline(context, note, x, y, r);

            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, HoldNoteShapeFillOuterColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_holdNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
            var r2 = r * Definitions.ScaleFactor3;
            context.FillEllipse(_holdNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
        }

        private void DrawSlideNote(D2DRenderContext context, Note note, float x, float y, float r, bool isMidway) {
            if (note.Basic.FlickType != NoteFlickType.None) {
                DrawFlickNote(context, note, x, y, r, note.Basic.FlickType);
                return;
            }

            DrawCommonNoteOutline(context, note, x, y, r);

            var fillColors = isMidway ? SlideNoteShapeFillOuterTranslucentColors : SlideNoteShapeFillOuterColors;
            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, fillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            var r2 = r * Definitions.ScaleFactor3;
            context.FillEllipse(_slideNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
            var l = r * Definitions.SlideNoteStrikeHeightFactor;
            context.FillRectangle(_slideNoteShapeFillInner, x - r1 - 1, y - l, r1 * 2 + 2, l * 2);
        }

    }
}
