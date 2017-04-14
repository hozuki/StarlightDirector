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

    }
}
