using System;
using System.Diagnostics;

namespace StarlightDirector.Beatmap.Extensions {
    public static class NoteExtensions {

        public static void SetSpecialType(this Note note, NoteType type) {
            switch (type) {
                case NoteType.VariantBpm:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
            note.Basic.Type = type;
        }

        public static void ConnectSync(this Note first, Note second) {
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

        public static void RemoveSync(this Note note) {
            /*
             * Before:
             *     ... <==> prev <==> this <==> next <==> ...
             *
             * After:
             *     ... <==> prev <============> next <==> ...
             *                        this
             */
            note.Editor.PrevSync?.SetNextSyncTargetInternal(note.Editor.NextSync);
            note.Editor.NextSync?.SetPrevSyncTargetInternal(note.Editor.PrevSync);
            note.SetPrevSyncTargetInternal(null);
            note.SetNextSyncTargetInternal(null);
        }

        public static void FixSyncWhenAdded(this Note @this) {
            // TODO
            if (!@this.Helper.IsGaming) {
                return;
            }
            @this.RemoveSync();
            Note prev = null;
            Note next = null;
            foreach (var note in @this.Basic.Bar.Notes) {
                if (note == @this) {
                    continue;
                }
                if (!note.Helper.IsGaming) {
                    continue;
                }
                if (note.Basic.IndexInGrid == @this.Basic.IndexInGrid) {
                    if (note.Basic.FinishPosition < @this.Basic.FinishPosition) {
                        if (prev == null || prev.Basic.FinishPosition < note.Basic.FinishPosition) {
                            prev = note;
                        }
                    } else {
                        if (next == null || note.Basic.FinishPosition < next.Basic.FinishPosition) {
                            next = note;
                        }
                    }
                }
            }
            ConnectSync(prev, @this);
            ConnectSync(@this, next);
        }

        public static void ResetAsTap(this Note note) {
            // Fix sync notes.
            note.RemoveSync();

            // Fix flick notes.
            Note prevFlick = null, nextFlick = null;
            if (note.Helper.HasPrevFlick) {
                prevFlick = note.Editor.PrevFlick;
                prevFlick.Editor.NextFlick = note.Editor.NextFlick;
            }
            if (note.Helper.HasNextFlick) {
                nextFlick = note.Editor.NextFlick;
                nextFlick.Editor.PrevFlick = note.Editor.PrevFlick;
            }
            if (prevFlick != null) {
                if (!prevFlick.Helper.HasPrevFlick && !prevFlick.Helper.HasNextFlick) {
                    prevFlick.Basic.FlickType = NoteFlickType.None;
                }
            }
            if (nextFlick != null) {
                if (!nextFlick.Helper.HasPrevFlick && !nextFlick.Helper.HasNextFlick) {
                    nextFlick.Basic.FlickType = NoteFlickType.None;
                }
            }
            if (prevFlick != null || nextFlick != null) {
                FixFlickDirections(prevFlick ?? nextFlick);
            }

            // Fix slide notes.
            Note prevSlide = null, nextSlide = null;
            if (note.Helper.HasPrevSlide) {
                prevSlide = note.Editor.PrevSlide;
                prevSlide.Editor.NextSlide = note.Editor.NextSlide;
            }
            if (note.Helper.HasNextSlide) {
                nextSlide = note.Editor.NextSlide;
                nextSlide.Editor.PrevSlide = note.Editor.PrevSlide;
            }
            if (prevSlide != null) {
                if (!prevSlide.Helper.HasPrevSlide && !prevSlide.Helper.HasNextSlide) {
                    prevSlide.Basic.Type = NoteType.TapOrFlick;
                }
            }
            if (nextSlide != null) {
                if (!nextSlide.Helper.HasPrevSlide && !nextSlide.Helper.HasNextSlide) {
                    nextSlide.Basic.Type = NoteType.TapOrFlick;
                }
            }

            // Fix hold notes.
            if (note.Helper.HasHoldPair) {
                var holdPair = note.Editor.HoldPair;
                holdPair.Editor.HoldPair = null;
                if (holdPair.Basic.Type == NoteType.Hold) {
                    holdPair.Basic.Type = NoteType.TapOrFlick;
                }
            }

            var e = note.Editor;
            e.PrevSync = e.NextSync = e.HoldPair = e.NextFlick = e.PrevFlick = e.NextSlide = e.PrevSlide = null;
            var b = note.Basic;
            b.Type = NoteType.TapOrFlick;
            b.FlickType = NoteFlickType.None;
        }

        [DebuggerStepThrough]
        public static bool IsOnTheSameRowWith(this Note @this, Note toTest) {
            return @this.Basic.Bar == toTest.Basic.Bar && @this.Basic.IndexInGrid == toTest.Basic.IndexInGrid;
        }

        [DebuggerStepThrough]
        public static void EditorToggleSelected(this Note note) {
            note.Editor.IsSelected = !note.Editor.IsSelected;
        }

        [DebuggerStepThrough]
        public static void EditorSelect(this Note note) {
            note.Editor.IsSelected = true;
        }

        [DebuggerStepThrough]
        public static void EditorUnselect(this Note note) {
            note.Editor.IsSelected = false;
        }

        [DebuggerStepThrough]
        private static void SetPrevSyncTargetInternal(this Note @this, Note prev) {
            @this.Editor.PrevSync = prev;
        }

        [DebuggerStepThrough]
        private static void SetNextSyncTargetInternal(this Note @this, Note next) {
            @this.Editor.NextSync = next;
        }

        private static void FixFlickDirections(this Note flickNoteInGroup) {
            if (!flickNoteInGroup.Helper.HasPrevFlick && !flickNoteInGroup.Helper.HasNextFlick) {
                return;
            }

            // Find the first flick note in this group.
            var firstFlick = flickNoteInGroup;
            while (flickNoteInGroup.Helper.HasPrevFlick) {
                firstFlick = flickNoteInGroup.Editor.PrevFlick;
            }

            // Set the directions.
            var currentFlick = firstFlick;
            var nextFlick = currentFlick.Editor.NextFlick;
            do {
                currentFlick.Basic.FlickType = nextFlick.Basic.FinishPosition > currentFlick.Basic.FinishPosition ? NoteFlickType.Right : NoteFlickType.Left;
                // Trick. So we don't need to track the previous flick to decide the flick direction of the last note of the group.
                nextFlick.Basic.FlickType = currentFlick.Basic.FlickType;
                currentFlick = nextFlick;
                nextFlick = currentFlick.Editor.NextFlick;
            } while (nextFlick != null);
        }

    }
}
