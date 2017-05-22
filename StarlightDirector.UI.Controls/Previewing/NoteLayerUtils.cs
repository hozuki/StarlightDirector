using System.Drawing;
using StarlightDirector.Beatmap;
using StarlightDirector.UI.Rendering;

namespace StarlightDirector.UI.Controls.Previewing {

    internal static class NotesLayerUtils {

        public static void GetBezierFromQuadratic(float x1, float xmid, float x4, out float x2, out float x3) {
            float xcontrol = xmid * 2f - (x1 + x4) * 0.5f;
            x2 = (x1 + xcontrol * 2f) / 3f;
            x3 = (x4 + xcontrol * 2f) / 3f;
        }

        public static void GetNotePairPositions(RenderContext context, double now, Note note1, Note note2, out float x1, out float x2, out float y1, out float y2) {
            var clientSize = context.ClientSize;
            if (IsNotePassed(note1, now)) {
                x1 = GetEndXByNotePosition(clientSize, note1.Basic.FinishPosition);
                y1 = GetAvatarYPosition(clientSize);
            } else if (IsNoteComing(note1, now)) {
                x1 = GetStartXByNotePosition(clientSize, note1.Basic.FinishPosition);
                y1 = GetBirthYPosition(clientSize);
            } else {
                x1 = GetNoteXPosition(context, now, note1);
                y1 = GetNoteYPosition(context, now, note1);
            }
            if (IsNotePassed(note2, now)) {
                x2 = GetEndXByNotePosition(clientSize, note2.Basic.FinishPosition);
                y2 = GetAvatarYPosition(clientSize);
            } else if (IsNoteComing(note2, now)) {
                x2 = GetStartXByNotePosition(clientSize, note2.Basic.FinishPosition);
                y2 = GetBirthYPosition(clientSize);
            } else {
                x2 = GetNoteXPosition(context, now, note2);
                y2 = GetNoteYPosition(context, now, note2);
            }
        }

        public static float NoteTimeTransform(float timeRemainingInWindow) {
            return timeRemainingInWindow / (2f - timeRemainingInWindow);
        }

        public static float NoteXTransform(float timeTransformed) {
            return timeTransformed;
        }

        public static float NoteYTransform(float timeTransformed) {
            return timeTransformed + 2.05128205f * timeTransformed * (1f - timeTransformed);
        }

        public static float GetNoteTransformedTime(double now, Note note, bool clampComing = false, bool clampPassed = false) {
            return GetNoteTransformedTime(now, note.Temporary.HitTiming.TotalSeconds, clampComing, clampPassed);
        }

        public static float GetNoteTransformedTime(double now, double hitTiming, bool clampComing = false, bool clampPassed = false) {
            var timeRemaining = hitTiming - now;
            var timeRemainingInWindow = (float)timeRemaining / Definitions.FutureTimeWindow;
            if (clampComing && timeRemaining > Definitions.FutureTimeWindow) {
                timeRemainingInWindow = 1f;
            }
            if (clampPassed && timeRemaining < 0f) {
                timeRemainingInWindow = 0f;
            }
            return NoteTimeTransform(timeRemainingInWindow);
        }

        public static float GetNoteXPosition(RenderContext context, double now, double hitTiming, NotePosition finishPosition, bool clampComing = false, bool clampPassed = false) {
            var timeTransformed = GetNoteTransformedTime(now, hitTiming, clampComing, clampPassed);
            return GetNoteXPosition(context, finishPosition, timeTransformed);
        }

        public static float GetNoteXPosition(RenderContext context, double now, Note note, bool clampComing = false, bool clampPassed = false) {
            var timeTransformed = GetNoteTransformedTime(now, note, clampComing, clampPassed);
            return GetNoteXPosition(context, note.Basic.FinishPosition, timeTransformed);
        }

        public static float GetNoteXPosition(RenderContext context, NotePosition finishPosition, float timeTransformed) {
            var clientSize = context.ClientSize;
            var endPos = Definitions.AvatarCenterXEndPositions[(int)finishPosition - 1] * clientSize.Width;
            var displayStartPosition = finishPosition;
            var startPos = Definitions.AvatarCenterXStartPositions[(int)displayStartPosition - 1] * clientSize.Width;
            return endPos - (endPos - startPos) * NoteXTransform(timeTransformed);
        }

        public static float GetNoteYPosition(RenderContext context, double now, double hitTiming, bool clampComing = false, bool clampPassed = false) {
            var timeTransformed = GetNoteTransformedTime(now, hitTiming, clampComing, clampPassed);
            return GetNoteYPosition(context, timeTransformed);
        }

        public static float GetNoteYPosition(RenderContext context, double now, Note note, bool clampComing = false, bool clampPassed = false) {
            var timeTransformed = GetNoteTransformedTime(now, note, clampComing, clampPassed);
            return GetNoteYPosition(context, timeTransformed);
        }

        public static float GetNoteYPosition(RenderContext context, float timeTransformed) {
            var clientSize = context.ClientSize;
            float ceiling = Definitions.FutureNoteCeiling * clientSize.Height,
                baseLine = Definitions.BaseLineYPosition * clientSize.Height;
            return baseLine - (baseLine - ceiling) * NoteYTransform(timeTransformed);
        }

        public static float GetNoteRadius(double now, Note note) => GetNoteRadius(now, note.Temporary.HitTiming.TotalSeconds);

        public static float GetNoteRadius(double now, double hitTiming) {
            var timeRemaining = hitTiming - now;
            var timeTransformed = NoteTimeTransform((float)timeRemaining / Definitions.FutureTimeWindow);
            if (timeTransformed < 0.75f) {
                if (timeTransformed < 0f) {
                    return Definitions.AvatarCircleRadius;
                } else {
                    return Definitions.AvatarCircleRadius * (1f - timeTransformed * 0.933333333f);
                }
            } else {
                if (timeTransformed < 1f) {
                    return Definitions.AvatarCircleRadius * ((1f - timeTransformed) * 1.2f);
                } else {
                    return 0f;
                }
            }
        }

        public static float GetAvatarYPosition(SizeF clientSize) {
            return clientSize.Height * Definitions.BaseLineYPosition;
        }

        public static float GetStartXByNotePosition(SizeF clientSize, NotePosition position) {
            return clientSize.Width * Definitions.AvatarCenterXStartPositions[(int)position - 1];
        }

        public static float GetEndXByNotePosition(SizeF clientSize, NotePosition position) {
            return clientSize.Width * Definitions.AvatarCenterXEndPositions[(int)position - 1];
        }

        public static float GetBirthYPosition(SizeF clientSize) {
            return clientSize.Height * Definitions.FutureNoteCeiling;
        }

        public static OnStageStatus GetNoteOnStageStatus(Note note, double now) {
            if (note.Temporary.HitTiming.TotalSeconds < now) {
                return OnStageStatus.Passed;
            }
            if (note.Temporary.HitTiming.TotalSeconds > now + Definitions.FutureTimeWindow) {
                return OnStageStatus.Upcoming;
            }
            return OnStageStatus.OnStage;
        }

        public static bool IsNoteOnStage(Note note, double now) {
            return now <= note.Temporary.HitTiming.TotalSeconds && note.Temporary.HitTiming.TotalSeconds <= now + Definitions.FutureTimeWindow;
        }

        public static bool IsNotePassed(Note note, double now) {
            return note.Temporary.HitTiming.TotalSeconds < now;
        }

        public static bool IsNoteComing(Note note, double now) {
            return note.Temporary.HitTiming.TotalSeconds > now + Definitions.FutureTimeWindow;
        }

    }

}
