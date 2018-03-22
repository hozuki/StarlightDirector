using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Overlay;
using OpenCGSS.StarlightDirector.Models.Beatmap;
using OpenCGSS.StarlightDirector.UI.Controls.Extensions;
using OpenCGSS.StarlightDirector.UI.Controls.Previewing;

namespace OpenCGSS.StarlightDirector.UI.Controls.Rendering {
    partial class ScorePreviewerRenderer {

        private void DrawNotes(Graphics context, double now, IEnumerable<Note> notes) {
            DrawNotes(context, now, notes, 0, -1);
        }

        private void DrawNotes(Graphics context, double now, IEnumerable<Note> notes, int startIndex, int endIndex) {
            if (startIndex < 0) {
                return;
            }
            IEnumerable<Note> selectedNotes;
            if (endIndex >= 0) {
                selectedNotes = notes.Skip(startIndex).Take(endIndex - startIndex + 1);
            } else {
                selectedNotes = notes;
            }
            foreach (var note in selectedNotes) {
                switch (note.Basic.Type) {
                    case NoteType.TapOrFlick:
                    case NoteType.Hold:
                    case NoteType.Slide:
                        if (note.Helper.HasNextSync) {
                            DrawSyncLine(context, now, note, note.Editor.NextSync);
                        }
                        break;
                }
                switch (note.Basic.Type) {
                    case NoteType.TapOrFlick:
                        if (note.Helper.IsFlick) {
                            if (note.Helper.HasNextFlick) {
                                DrawFlickRibbon(context, now, note, note.Editor.NextFlick);
                            }
                        }
                        break;
                    case NoteType.Hold:
                        if (note.Helper.IsHoldStart) {
                            DrawHoldRibbon(context, now, note, note.Editor.HoldPair);
                        }
                        if (note.Helper.IsHoldEnd) {
                            if (!NotesLayerUtils.IsNoteOnStage(note.Editor.HoldPair, now)) {
                                DrawHoldRibbon(context, now, note.Editor.HoldPair, note);
                            }
                        }
                        break;
                    case NoteType.Slide:
                        if (note.Helper.HasNextSlide) {
                            DrawSlideRibbon(context, now, note, note.Editor.NextSlide);
                        } else if (note.Helper.HasNextFlick) {
                            DrawSlideRibbon(context, now, note, note.Editor.NextFlick);
                        }
                        if (note.Helper.HasPrevSlide) {
                            if (!NotesLayerUtils.IsNoteOnStage(note.Editor.PrevSlide, now)) {
                                DrawSlideRibbon(context, now, note.Editor.PrevSlide, note);
                            }
                        }
                        break;
                }
                switch (note.Basic.Type) {
                    case NoteType.TapOrFlick:
                        if (note.Basic.FlickType == NoteFlickType.None) {
                            if (note.Helper.IsHoldEnd) {
                                DrawHoldNote(context, now, note);
                            } else {
                                DrawTapNote(context, now, note);
                            }
                        } else {
                            DrawFlickNote(context, now, note);
                        }
                        break;
                    case NoteType.Hold:
                        DrawHoldNote(context, now, note);
                        break;
                    case NoteType.Slide:
                        if (note.Helper.HasNextFlick || (note.Helper.IsSlideEnd && note.Basic.FlickType != NoteFlickType.None)) {
                            DrawFlickNote(context, now, note);
                        } else if (note.Basic.FlickType != NoteFlickType.None) {
                            DrawFlickNote(context, now, note);
                        } else {
                            DrawSlideNote(context, now, note);
                        }
                        break;
                }
            }
        }

        private void DrawTapNote(Graphics context, double now, Note note) {
            if (!NotesLayerUtils.IsNoteOnStage(note, now)) {
                return;
            }
            float x = NotesLayerUtils.GetNoteXPosition(context, now, note),
                y = NotesLayerUtils.GetNoteYPosition(context, now, note),
                r = NotesLayerUtils.GetNoteRadius(now, note);

            DrawCommonNoteOutline(context, note, x, y, r);

            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(x, y, r, TapNoteShapeFillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_tapNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
        }

        private void DrawFlickNote(Graphics context, double now, Note note) {
            if (!NotesLayerUtils.IsNoteOnStage(note, now)) {
                return;
            }
            if (note.Basic.FlickType == NoteFlickType.None) {
                Debug.Print("WARNING: Tap/hold/slide note requested in DrawFlickNote.");
                return;
            }
            float x = NotesLayerUtils.GetNoteXPosition(context, now, note),
                y = NotesLayerUtils.GetNoteYPosition(context, now, note),
                r = NotesLayerUtils.GetNoteRadius(now, note);

            DrawCommonNoteOutline(context, note, x, y, r);

            var flickType = note.Basic.FlickType;
            switch (flickType) {
                case NoteFlickType.Left:
                case NoteFlickType.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(flickType), "Unknown flick type for flick note rendering.");
            }

            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(x, y, r1, FlickNoteShapeFillOuterColors)) {
                context.FillCircle(fill, x, y, r1);
            }
            context.DrawCircle(_flickNoteShapeStroke, x, y, r1);

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
            context.FillPolygon(_flickNoteShapeFillInner, polygon);
        }

        private void DrawHoldNote(Graphics context, double now, Note note) {
            if (!NotesLayerUtils.IsNoteOnStage(note, now)) {
                return;
            }
            float x = NotesLayerUtils.GetNoteXPosition(context, now, note),
                y = NotesLayerUtils.GetNoteYPosition(context, now, note),
                r = NotesLayerUtils.GetNoteRadius(now, note);

            DrawCommonNoteOutline(context, note, x, y, r);

            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(x, y, r, HoldNoteShapeFillOuterColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_holdNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
            var r2 = r * Definitions.ScaleFactor3;
            context.FillEllipse(_holdNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
        }

        private void DrawSlideNote(Graphics context, double now, Note note) {
            float x, y, r;
            if (NotesLayerUtils.IsNoteOnStage(note, now)) {
                if (note.Helper.IsSlideEnd && NotesLayerUtils.IsNotePassed(note, now)) {
                    return;
                }
                x = NotesLayerUtils.GetNoteXPosition(context, now, note);
                y = NotesLayerUtils.GetNoteYPosition(context, now, note);
                r = NotesLayerUtils.GetNoteRadius(now, note);
            } else if (NotesLayerUtils.IsNotePassed(note, now)) {
                if (!note.Helper.HasNextSlide || NotesLayerUtils.IsNotePassed(note.Editor.NextSlide, now)) {
                    return;
                }
                var nextSlideNote = note.Editor.NextSlide;
                if (nextSlideNote == null) {
                    // Actually, here is an example of invalid format. :)
                    DrawTapNote(context, now, note);
                    return;
                } else {
                    var startX = NotesLayerUtils.GetEndXByNotePosition(context.Bounds, note.Basic.FinishPosition);
                    var endX = NotesLayerUtils.GetEndXByNotePosition(context.Bounds, nextSlideNote.Basic.FinishPosition);
                    y = NotesLayerUtils.GetAvatarYPosition(context.Bounds);
                    x = (float)((now - note.Temporary.HitTiming.TotalSeconds) / (nextSlideNote.Temporary.HitTiming.TotalSeconds - note.Temporary.HitTiming.TotalSeconds)) * (endX - startX) + startX;
                    r = Definitions.AvatarCircleRadius;
                }
            } else {
                return;
            }

            DrawCommonNoteOutline(context, note, x, y, r);

            var isMidway = note.Helper.IsSlideMidway;
            var fillColors = isMidway ? SlideNoteShapeFillOuterTranslucentColors : SlideNoteShapeFillOuterColors;
            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(x, y, r, fillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            var r2 = r * Definitions.ScaleFactor3;
            context.FillEllipse(_slideNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
            var l = r * Definitions.SlideNoteStrikeHeightFactor;
            context.FillRectangle(_slideNoteShapeFillInner, x - r1 - 1, y - l, r1 * 2 + 2, l * 2);
        }

        private void DrawCommonNoteOutline(Graphics context, Note note, float x, float y, float r) {
            context.FillCircle(_noteCommonFill, x, y, r);
            context.DrawCircle(_noteCommonStroke, x, y, r);
            if (note.Editor.IsSelected) {
                context.DrawCircle(_noteSelectedStroke, x, y, r * Definitions.ScaleFactor0);
            }
        }

        private void DrawSyncLine(Graphics context, double now, Note note1, Note note2) {
            if (!NotesLayerUtils.IsNoteOnStage(note1, now) || !NotesLayerUtils.IsNoteOnStage(note2, now)) {
                return;
            }
            float x1 = NotesLayerUtils.GetNoteXPosition(context, now, note1),
                y = NotesLayerUtils.GetNoteYPosition(context, now, note2),
                x2 = NotesLayerUtils.GetNoteXPosition(context, now, note2);
            var r = NotesLayerUtils.GetNoteRadius(now, note2);
            float xLeft = Math.Min(x1, x2), xRight = Math.Max(x1, x2);
            context.DrawLine(_syncLineStroke, xLeft + r, y, xRight - r, y);
        }

        [DebuggerStepThrough]
        private static LinearGradientBrush GetFillBrush(float x, float y, float r, Color[] colors) {
            return new LinearGradientBrush(new Vector2(x, y - r), new Vector2(x, y + r), colors);
        }

    }
}
