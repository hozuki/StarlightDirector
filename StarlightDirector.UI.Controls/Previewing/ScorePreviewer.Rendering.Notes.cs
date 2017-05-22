using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering;
using StarlightDirector.UI.Rendering.Direct2D;
using StarlightDirector.UI.Rendering.Extensions;

namespace StarlightDirector.UI.Controls.Previewing {
    partial class ScorePreviewer {

        private void DrawNotes(D2DRenderContext context, double now, IEnumerable<Note> notes) {
            DrawNotes(context, now, notes, 0, -1);
        }

        private void DrawNotes(D2DRenderContext context, double now, IEnumerable<Note> notes, int startIndex, int endIndex) {
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
                        DrawSlideNote(context, now, note);
                        break;
                }
            }
        }

        private void DrawTapNote(D2DRenderContext context, double now, Note note) {
            if (!NotesLayerUtils.IsNoteOnStage(note, now)) {
                return;
            }
            float x = NotesLayerUtils.GetNoteXPosition(context, now, note),
                y = NotesLayerUtils.GetNoteYPosition(context, now, note),
                r = NotesLayerUtils.GetNoteRadius(now, note);

            DrawCommonNoteOutline(context, note, x, y, r);

            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, TapNoteShapeFillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_tapNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
        }

        private void DrawFlickNote(D2DRenderContext context, double now, Note note) {
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

        private void DrawHoldNote(D2DRenderContext context, double now, Note note) {
            if (!NotesLayerUtils.IsNoteOnStage(note, now)) {
                return;
            }
            float x = NotesLayerUtils.GetNoteXPosition(context, now, note),
                y = NotesLayerUtils.GetNoteYPosition(context, now, note),
                r = NotesLayerUtils.GetNoteRadius(now, note);

            DrawCommonNoteOutline(context, note, x, y, r);

            var r1 = r * Definitions.ScaleFactor1;
            using (var fill = GetFillBrush(context, x, y, r, HoldNoteShapeFillOuterColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            context.DrawEllipse(_holdNoteShapeStroke, x - r1, y - r1, r1 * 2, r1 * 2);
            var r2 = r * Definitions.ScaleFactor3;
            context.FillEllipse(_holdNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
        }

        private void DrawSlideNote(D2DRenderContext context, double now, Note note) {
            if (note.Basic.FlickType != NoteFlickType.None) {
                DrawFlickNote(context, now, note);
                return;
            }

            float x, y, r;
            if (note.Helper.IsSlideEnd || NotesLayerUtils.IsNoteOnStage(note, now)) {
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
                    var startX = NotesLayerUtils.GetEndXByNotePosition(context.ClientSize, note.Basic.FinishPosition);
                    var endX = NotesLayerUtils.GetEndXByNotePosition(context.ClientSize, nextSlideNote.Basic.FinishPosition);
                    y = NotesLayerUtils.GetAvatarYPosition(context.ClientSize);
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
            using (var fill = GetFillBrush(context, x, y, r, fillColors)) {
                context.FillEllipse(fill, x - r1, y - r1, r1 * 2, r1 * 2);
            }
            var r2 = r * Definitions.ScaleFactor3;
            context.FillEllipse(_slideNoteShapeFillInner, x - r2, y - r2, r2 * 2, r2 * 2);
            var l = r * Definitions.SlideNoteStrikeHeightFactor;
            context.FillRectangle(_slideNoteShapeFillInner, x - r1 - 1, y - l, r1 * 2 + 2, l * 2);
        }

        private void DrawCommonNoteOutline(RenderContext context, Note note, float x, float y, float r) {
            context.FillCircle(_noteCommonFill, x, y, r);
            context.DrawCircle(_noteCommonStroke, x, y, r);
            if (note.Editor.IsSelected) {
                context.DrawCircle(_noteSelectedStroke, x, y, r * Definitions.ScaleFactor0);
            }
        }

        private void DrawSyncLine(RenderContext context, double now, Note note1, Note note2) {
            if (!NotesLayerUtils.IsNoteOnStage(note1, now) || !NotesLayerUtils.IsNoteOnStage(note2, now)) {
                return;
            }
            float x1 = NotesLayerUtils.GetNoteXPosition(context, now, note1),
                y = NotesLayerUtils.GetNoteYPosition(context, now, note2),
                x2 = NotesLayerUtils.GetNoteXPosition(context, now, note2);
            float r = NotesLayerUtils.GetNoteRadius(now, note2);
            float xLeft = Math.Min(x1, x2), xRight = Math.Max(x1, x2);
            context.DrawLine(_syncLineStroke, xLeft + r, y, xRight - r, y);
        }

        [DebuggerStepThrough]
        private static D2DLinearGradientBrush GetFillBrush(D2DRenderContext context, float x, float y, float r, Color[] colors) {
            return new D2DLinearGradientBrush(context, new PointF(x, y - r), new PointF(x, y + r), colors);
        }

    }
}
