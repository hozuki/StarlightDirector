﻿using System;
using System.Diagnostics;
using StarlightDirector.Beatmap.Extensions;

namespace StarlightDirector.Beatmap {
    public static class NoteUtilities {

        public static (Note First, Note Second) Split(Note note1, Note note2) {
            var comparisonResult = Note.TimingThenPositionComparison(note1, note2);
            if (comparisonResult > 0) {
                return (note2, note1);
            } else if (comparisonResult < 0) {
                return (note1, note2);
            } else {
                return (note1, note2);
            }
        }

        public static void MakeSync(Note first, Note second) {
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
            } else if (first?.Editor?.NextSync == second && second?.Editor.PrevSync == first) {
                return;
            }
            first?.Editor?.NextSync?.SetPrevSyncTargetInternal(null);
            second?.Editor?.PrevSync?.SetNextSyncTargetInternal(null);
            first?.SetNextSyncTargetInternal(second);
            second?.SetPrevSyncTargetInternal(first);
        }

        public static void MakeHold(Note startNote, Note endNote) {
            startNote.Basic.Type = NoteType.Hold;
            startNote.Editor.HoldPair = endNote;
            endNote.Editor.HoldPair = startNote;
        }

        public static void MakeFlick(Note startNote, Note endNote) {
            // No need to set the note types.
            startNote.Editor.NextFlick = endNote;
            endNote.Editor.PrevFlick = startNote;
            var flickType = startNote.Basic.FinishPosition > endNote.Basic.FinishPosition ? NoteFlickType.Left : NoteFlickType.Right;
            if (endNote.Basic.FlickType == NoteFlickType.None) {
                endNote.Basic.FlickType = flickType;
            }
            startNote.Basic.FlickType = flickType;
        }

        public static void MakeSlide(Note startNote, Note endNote) {
            startNote.Basic.Type = NoteType.Slide;
            endNote.Basic.Type = NoteType.Slide;
            startNote.Editor.NextSlide = endNote;
            endNote.Editor.PrevSlide = startNote;
        }

        public static bool AreNotesInHoldChain(Note note1, Note note2) {
            return note1.Editor.HoldPair == note2 && note2.Editor.HoldPair == note1;
        }

        public static bool AreNotesInFlickChain(Note note1, Note note2) {
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

        public static bool AreNotesInSlideChain(Note note1, Note note2) {
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