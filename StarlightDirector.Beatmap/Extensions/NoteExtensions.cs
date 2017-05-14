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


        public static void FixSyncWhenAdded(this Note @this) {
            // TODO
            if (!@this.Helper.IsGaming) {
                return;
            }
            @this.RemoveSync();
            Note prev = null, next = null;
            foreach (var note in @this.Basic.Bar.Notes) {
                if (note == @this) {
                    continue;
                }
                if (!note.Helper.IsGaming) {
                    continue;
                }
                if (note.Basic.IndexInGrid != @this.Basic.IndexInGrid) {
                    continue;
                }
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
            NoteUtilities.MakeSync(prev, @this);
            NoteUtilities.MakeSync(@this, next);
        }

        public static void ResetAsTap(this Note note) {
            // Fix sync notes.
            note.RemoveSync();

            // Fix flick notes.
            Note prevFlick = note.Editor.PrevFlick, nextFlick = note.Editor.NextFlick;
            //var flickGroupStillValid = true;
            //// Test if we delete this note, the rest still form a valid flick group.
            //if (prevFlick != null && nextFlick != null) {
            //    if (prevFlick.Basic.FinishPosition == nextFlick.Basic.FinishPosition) {
            //        // prevFlick and nextFlick form a straight vertical line. Now it is invalid.
            //        flickGroupStillValid = false;
            //    }
            //}
            //if (prevFlick != null) {
            //    // If the rest cannot form one valid flick group, then split them into two.
            //    prevFlick.Editor.NextFlick = flickGroupStillValid ? note.Editor.NextFlick : null;
            //}
            //if (nextFlick != null) {
            //    nextFlick.Editor.PrevFlick = flickGroupStillValid ? note.Editor.PrevFlick : null;
            //}

            // New strategy: force split the flick group into two. Reconnect if the user needs to.
            if (prevFlick != null) {
                prevFlick.Editor.NextFlick = null;
            }
            if (nextFlick != null) {
                nextFlick.Editor.PrevFlick = null;
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
            // Actually deleting a flick note only influences the flick note before it (prevFlick).
            if (prevFlick != null) {
                FixFlickDirections(prevFlick);
            }
            if (nextFlick != null) {
                FixFlickDirections(nextFlick);
            }

            // Fix slide notes.
            Note prevSlide = note.Editor.PrevSlide, nextSlide = note.Editor.NextSlide;
            if (prevSlide != null) {
                prevSlide.Editor.NextSlide = null;
            }
            if (nextSlide != null) {
                nextSlide.Editor.PrevSlide = null;
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
            if (prevSlide != null) {
                FixFlickDirections(prevSlide);
            }
            if (nextSlide != null) {
                FixFlickDirections(nextSlide);
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
        internal static void SetPrevSyncTargetInternal(this Note @this, Note prev) {
            @this.Editor.PrevSync = prev;
        }

        [DebuggerStepThrough]
        internal static void SetNextSyncTargetInternal(this Note @this, Note next) {
            @this.Editor.NextSync = next;
        }

        private static void RemoveSync(this Note note) {
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

        private static void FixFlickDirections(this Note flickNoteInGroup) {
            if (!flickNoteInGroup.Helper.HasPrevFlick && !flickNoteInGroup.Helper.HasNextFlick) {
                return;
            }

            // Find the first flick note in this group.
            var firstFlick = flickNoteInGroup;
            while (firstFlick.Helper.HasPrevFlick) {
                firstFlick = firstFlick.Editor.PrevFlick;
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
