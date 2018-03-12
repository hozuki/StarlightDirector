using System;
using System.Diagnostics;
using JetBrains.Annotations;
using OpenCGSS.Director.Modules.SldProject.Models.Beatmap.Extensions;

namespace OpenCGSS.Director.Modules.SldProject.Models.Beatmap {
    public static class NoteUtilities {

        /// <summary>
        /// Split a note pair to an ordered note pair, where the first returned note is always "less than" the second returned note.
        /// </summary>
        /// <param name="note1"></param>
        /// <param name="note2"></param>
        /// <returns></returns>
        public static (Note First, Note Second) Split([NotNull] Note note1, [NotNull] Note note2) {
            var comparisonResult = Note.TimingThenPositionComparison(note1, note2);

            if (comparisonResult > 0) {
                return (note2, note1);
            } else if (comparisonResult < 0) {
                return (note1, note2);
            } else {
                return (note1, note2);
            }
        }

        public static void MakeSync([CanBeNull] Note first, [CanBeNull] Note second) {
            /*
             * Before:
             *     ... <==>    first    <==> first_next   <==> ...
             *     ... <==> second_prev <==>   second     <==> ...
             *
             * After:
             *                               first_next   <==> ...
             *     ... <==>    first    <==>   second     <==> ...
             *     ... <==> second_prev
             */
            if (first == second) {
                throw new ArgumentException("A note should not be connected to itself", nameof(second));
            } else if (first?.Editor.NextSync == second && second?.Editor.PrevSync == first) {
                return;
            }

            first?.Editor.NextSync?.SetPrevSyncTargetInternal(null);
            second?.Editor.PrevSync?.SetNextSyncTargetInternal(null);
            first?.SetNextSyncTargetInternal(second);
            second?.SetPrevSyncTargetInternal(first);
        }

        public static void MakeHold([NotNull] Note startNote, [NotNull] Note endNote) {
            startNote.Basic.Type = NoteType.Hold;
            startNote.Basic.FlickType = NoteFlickType.None;
            startNote.Editor.HoldPair = endNote;
            endNote.Editor.HoldPair = startNote;
        }

        public static void MakeFlick([NotNull] Note startNote, [NotNull] Note endNote) {
            // No need to set the note types.
            startNote.Editor.NextFlick = endNote;
            endNote.Editor.PrevFlick = startNote;

            var flickType = startNote.Basic.FinishPosition > endNote.Basic.FinishPosition ? NoteFlickType.Left : NoteFlickType.Right;

            if (endNote.Basic.FlickType == NoteFlickType.None) {
                endNote.Basic.FlickType = flickType;
            }

            startNote.Basic.FlickType = flickType;
        }

        public static void MakeSlide([NotNull] Note startNote, [NotNull] Note endNote) {
            startNote.Basic.Type = NoteType.Slide;
            endNote.Basic.Type = NoteType.Slide;
            startNote.Editor.NextSlide = endNote;
            endNote.Editor.PrevSlide = startNote;
            startNote.Basic.FlickType = NoteFlickType.None;

            if (endNote.Helper.HasNextFlick) {
                var next = endNote.Editor.NextFlick;

                Debug.Assert(next != null, nameof(next) + " != null");

                if (next.Basic.FinishPosition > endNote.Basic.FinishPosition) {
                    endNote.Basic.FlickType = NoteFlickType.Right;
                } else if (next.Basic.FinishPosition < endNote.Basic.FinishPosition) {
                    endNote.Basic.FlickType = NoteFlickType.Left;
                } else {
                    throw new ArgumentOutOfRangeException(nameof(endNote), "endNote's next note should be a flick note, not in the same column of endNote.");
                }
            } else {
                endNote.Basic.FlickType = NoteFlickType.None;
            }
        }

        public static void MakeSlideToFlick([NotNull] Note note1, [NotNull] Note note2) {
            note1.Editor.NextFlick = note2;
            note2.Editor.PrevSlide = note1;
            note1.Basic.FlickType = note2.Basic.FinishPosition > note1.Basic.FinishPosition ? NoteFlickType.Right : NoteFlickType.Left;
            // Flick type of note2 doesn't need to be changed, since it is already in a flick group.
        }

        public static bool AreNotesInHoldChain([NotNull] Note note1, [NotNull] Note note2) {
            return note1.Editor.HoldPair == note2 && note2.Editor.HoldPair == note1;
        }

        public static bool AreNotesInFlickChain([NotNull] Note note1, [NotNull] Note note2) {
            var next = note1.Editor.NextFlick;

            while (next != null) {
                if (next == note2) {
                    return true;
                }

                next = next.Editor.NextFlick;
            }

            var prev = note1.Editor.PrevFlick;

            while (prev != null) {
                if (prev == note2) {
                    return true;
                }

                prev = prev.Editor.PrevFlick;
            }

            return false;
        }

        public static bool AreNotesInSlideChain([NotNull] Note note1, [NotNull] Note note2) {
            var next = note1.Editor.NextSlide;

            while (next != null) {
                if (next == note2) {
                    return true;
                }

                next = next.Editor.NextSlide;
            }

            var prev = note1.Editor.PrevSlide;

            while (prev != null) {
                if (prev == note2) {
                    return true;
                }

                prev = prev.Editor.PrevSlide;
            }

            return false;
        }

    }
}
