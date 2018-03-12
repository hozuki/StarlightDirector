using System;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using OpenCGSS.Director.Modules.SldProject.Models;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Extensions;
using OpenCGSS.Director.Modules.SldProject.Rendering.Extensions;

namespace OpenCGSS.Director.Modules.SldProject.Rendering {
    partial class ScoreEditorRenderer {

        private void RenderNotes([NotNull] Graphics graphics, [NotNull] Score score, [NotNull] ScoreEditorConfig config, [NotNull] ScoreEditorLook look, float scrollOffsetY, Vector2 clientSize) {
            var gridArea = ScoreEditorLayout.GetGridArea(config, clientSize);
            var noteRadius = config.NoteRadius;
            var specialNoteArea = ScoreEditorLayout.GetSpecialNoteArea(config, clientSize);

            DrawNoteConnections(graphics, score, config, look, gridArea, scrollOffsetY, scrollOffsetY, noteRadius);
            DrawNotes(graphics, score, config, look, gridArea, specialNoteArea, scrollOffsetY, noteRadius);
        }

        private void DrawNotes([NotNull] Graphics graphics, [NotNull] Score score, [NotNull] ScoreEditorConfig config, [NotNull] ScoreEditorLook look, Rectangle gridArea, Rectangle specialNoteArea, float noteStartY, float noteRadius) {
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
                        if (!ScoreEditorLayout.IsNoteVisible(note, gridArea, noteStartY, unit, noteRadius)) {
                            continue;
                        }

                        if (note.Helper.IsGaming) {
                            var x = ScoreEditorLayout.GetNotePositionX(note, gridArea, numColumns);
                            var y = ScoreEditorLayout.GetNotePositionY(note, unit, noteStartY);
                            var h = note.Helper;

                            // Base notes
                            if (h.IsSlide) {
                                if (h.HasNextFlick) {
                                    DrawFlickNote(graphics, note, x, y, noteRadius, note.Basic.FlickType);
                                } else {
                                    DrawSlideNote(graphics, note, x, y, noteRadius, h.IsSlideMidway);
                                }
                            } else if (h.IsHoldStart) {
                                DrawHoldNote(graphics, note, x, y, noteRadius);
                            } else {
                                if (note.Basic.FlickType != NoteFlickType.None) {
                                    DrawFlickNote(graphics, note, x, y, noteRadius, note.Basic.FlickType);
                                } else if (h.IsHoldEnd) {
                                    DrawHoldNote(graphics, note, x, y, noteRadius);
                                } else {
                                    DrawTapNote(graphics, note, x, y, noteRadius);
                                }
                            }

                            // Indicators
                            if (shouldDrawIndicators) {
                                if (note.Helper.IsSync) {
                                    graphics.FillCircle(_syncIndicatorBrush, x + noteRadius, y - noteRadius, Definitions.IndicatorRadius);
                                }
                                if (note.Helper.IsHold) {
                                    graphics.FillCircle(_holdIndicatorBrush, x - noteRadius, y + noteRadius, Definitions.IndicatorRadius);
                                }
                                if (note.Helper.IsSlide) {
                                    graphics.FillCircle(_slideIndicatorBrush, x + noteRadius, y + noteRadius, Definitions.IndicatorRadius);
                                } else if (note.Helper.IsFlick) {
                                    graphics.FillCircle(_flickIndicatorBrush, x + noteRadius, y + noteRadius, Definitions.IndicatorRadius);
                                }
                            }

                            // Start position text (top left corner)
                            if (note.Basic.StartPosition != note.Basic.FinishPosition) {
                                var startPositionX = x - noteRadius - Definitions.StartPositionFontSize / 2;
                                var startPositionY = y - noteRadius - Definitions.StartPositionFontSize / 2;
                                var text = ((int)note.Basic.StartPosition).ToString();
                                var textSize = graphics.MeasureString(startPositionFont, text);

                                graphics.FillString(_noteCommonFill, startPositionFont, text, startPositionX, startPositionY + textSize.Y);
                            }
                        } else if (note.Helper.IsSpecial) {
                            // Draw a special note (a rectangle)
                            var left = specialNoteArea.Left + noteRadius * 4;
                            var gridY = ScoreEditorLayout.GetNotePositionY(note, unit, noteStartY);
                            var top = gridY - noteRadius * Definitions.SpecialNoteHeightFactor;
                            var width = specialNoteArea.Right - left;
                            var height = noteRadius * 2 * Definitions.SpecialNoteHeightFactor;

                            graphics.DrawRectangle(_specialNoteStroke, left, top, width, height);
                            graphics.FillRectangle(_specialNoteFill, left, top, width, height);

                            string specialNoteText;

                            switch (note.Basic.Type) {
                                case NoteType.VariantBpm:
                                    specialNoteText = $"BPM: {note.Params.NewBpm:0.00}";
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException(nameof(note.Basic.Type), note.Basic.Type, null);
                            }

                            var rect = graphics.MeasureString(_specialNoteDescriptionFont, specialNoteText);
                            var textLeft = left + 2;
                            var textBottom = gridY + rect.Y / 2;

                            graphics.FillString(_specialNoteTextBrush, _specialNoteDescriptionFont, specialNoteText, textLeft, textBottom);
                        }
                    }
                }

                noteStartY -= numberOfGrids * unit;
            }
        }

        private void DrawNoteConnections([NotNull] Graphics graphics, [NotNull] Score score, [NotNull] ScoreEditorConfig config, [NotNull] ScoreEditorLook look, Rectangle gridArea, float scrollOffsetY, float noteStartY, float noteRadius) {
            if (!score.HasAnyNote) {
                return;
            }

            var unit = look.BarLineSpaceUnit;
            var numColumns = config.NumberOfColumns;

            foreach (var bar in score.Bars) {
                var numberOfGrids = bar.GetNumberOfGrids();

                if (bar.Helper.HasAnyNote) {
                    foreach (var note in bar.Notes) {
                        // Radius of the note at the start of this connection line
                        var thisStatus = ScoreEditorLayout.GetNoteOnStageStatus(note, gridArea, noteStartY, unit, noteRadius);
                        var x1 = ScoreEditorLayout.GetNotePositionX(note, gridArea, numColumns);
                        var y1 = ScoreEditorLayout.GetNotePositionY(note, unit, noteStartY);

                        if (note.Helper.HasNextSync && thisStatus == OnStageStatus.OnStage) {
                            var n2 = note.Editor.NextSync;
                            var x2 = ScoreEditorLayout.GetNotePositionX(n2, gridArea, numColumns);

                            // Draw sync line
                            graphics.DrawLine(_syncLineStroke, x1, y1, x2, y1);
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

                            var thatStatus = ScoreEditorLayout.GetNoteOnStageStatus(n2, gridArea, s2, unit, noteRadius);

                            if ((int)thisStatus * (int)thatStatus <= 0) {
                                var x2 = ScoreEditorLayout.GetNotePositionX(n2, gridArea, numColumns);
                                var y2 = ScoreEditorLayout.GetNotePositionY(score.Bars, n2.Basic.Bar.Basic.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);

                                // Draw hold line
                                graphics.DrawLine(_holdLineStroke, x1, y1, x2, y2);
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

                            var thatStatus = ScoreEditorLayout.GetNoteOnStageStatus(n2, gridArea, s2, unit, noteRadius);

                            if ((int)thisStatus * (int)thatStatus <= 0) {
                                var x2 = ScoreEditorLayout.GetNotePositionX(n2, gridArea, numColumns);
                                var y2 = ScoreEditorLayout.GetNotePositionY(score.Bars, n2.Basic.Bar.Basic.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);

                                // Draw flick line
                                graphics.DrawLine(_flickLineStroke, x1, y1, x2, y2);
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

                            var thatStatus = ScoreEditorLayout.GetNoteOnStageStatus(n2, gridArea, s2, unit, noteRadius);

                            if ((int)thisStatus * (int)thatStatus <= 0) {
                                var x2 = ScoreEditorLayout.GetNotePositionX(n2, gridArea, numColumns);
                                var y2 = ScoreEditorLayout.GetNotePositionY(score.Bars, n2.Basic.Bar.Basic.Index, n2.Basic.IndexInGrid, unit, scrollOffsetY);

                                // Draw slide line
                                graphics.DrawLine(_slideLineStroke, x1, y1, x2, y2);
                            }
                        }
                    }
                }

                noteStartY -= numberOfGrids * unit;
            }
        }

        private void DrawCommonNoteOutline([NotNull] Graphics graphics, [NotNull]  Note note, float x, float y, float r) {
            graphics.FillCircle(_noteCommonFill, x, y, r);
            graphics.DrawCircle(_noteCommonStroke, x, y, r);

            if (note.Editor.IsSelected) {
                graphics.DrawCircle(_noteSelectedStroke, x, y, r * Definitions.ScaleFactor0);
            }
        }

        private void DrawTapNote([NotNull] Graphics graphics, [NotNull] Note note, float x, float y, float r) {
            DrawCommonNoteOutline(graphics, note, x, y, r);

            var r1 = r * Definitions.ScaleFactor1;

            using (var fill = GetFillBrush(x, y, r, TapNoteShapeFillColors)) {
                graphics.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }

            graphics.DrawEllipse(_tapNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
        }

        private void DrawFlickNote([NotNull] Graphics graphics, [NotNull] Note note, float x, float y, float r, NoteFlickType flickType) {
            DrawCommonNoteOutline(graphics, note, x, y, r);

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

            using (var fill = GetFillBrush(x, y, r1, FlickNoteShapeFillOuterColors)) {
                graphics.FillCircle(fill, x, y, r1);
            }

            graphics.DrawCircle(_flickNoteShapeStroke, x, y, r1);

            var r3 = r * Definitions.ScaleFactor2;
            // Triangle
            var polygon = new Vector2[3];

            switch (flickType) {
                case NoteFlickType.Left:
                    polygon[0] = new Vector2(x - r3, y);
                    polygon[1] = new Vector2(x + r3 / 2, y + r3 / 2 * Definitions.Sqrt3);
                    polygon[2] = new Vector2(x + r3 / 2, y - r3 / 2 * Definitions.Sqrt3);
                    break;
                case NoteFlickType.Right:
                    polygon[0] = new Vector2(x + r3, y);
                    polygon[1] = new Vector2(x - r3 / 2, y - r3 / 2 * Definitions.Sqrt3);
                    polygon[2] = new Vector2(x - r3 / 2, y + r3 / 2 * Definitions.Sqrt3);
                    break;
            }

            graphics.FillPolygon(_flickNoteShapeFillInner, polygon);
        }

        private void DrawHoldNote([NotNull] Graphics graphics, [NotNull] Note note, float x, float y, float r) {
            DrawCommonNoteOutline(graphics, note, x, y, r);

            var r1 = r * Definitions.ScaleFactor1;

            using (var fill = GetFillBrush(x, y, r, HoldNoteShapeFillOuterColors)) {
                graphics.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }

            graphics.DrawEllipse(_holdNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);

            var r2 = r * Definitions.ScaleFactor3;

            graphics.FillEllipse(_holdNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
        }

        private void DrawSlideNote([NotNull] Graphics graphics, [NotNull] Note note, float x, float y, float r, bool isMidway) {
            if (note.Basic.FlickType != NoteFlickType.None) {
                DrawFlickNote(graphics, note, x, y, r, note.Basic.FlickType);

                return;
            }

            DrawCommonNoteOutline(graphics, note, x, y, r);

            var fillColors = isMidway ? SlideNoteShapeFillOuterTranslucentColors : SlideNoteShapeFillOuterColors;
            var r1 = r * Definitions.ScaleFactor1;

            using (var fill = GetFillBrush(x, y, r, fillColors)) {
                graphics.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }

            var r2 = r * Definitions.ScaleFactor3;

            graphics.FillEllipse(_slideNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);

            var l = r * Definitions.SlideNoteStrikeHeightFactor;

            graphics.FillRectangle(_slideNoteShapeFillInner, x - r1 - 1, y - l, r1 * 2 + 2, l * 2);
        }

    }
}
