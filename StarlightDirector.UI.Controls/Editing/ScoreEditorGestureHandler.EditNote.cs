using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;

namespace StarlightDirector.UI.Controls.Editing {
    partial class ScoreEditorGestureHandler {

        private void EditNoteModeSelect(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;
            if (hit.HitAnyNote) {
                if (editor.HasSelectedBars) {
                    editor.ClearSelectedBars();
                    editor.Invalidate();
                } else {
                    var note = hit.Note;
                    if (note == _lastMouseDownNote) {
                        switch (Control.ModifierKeys) {
                            case Keys.Control:
                                EditorToggleNoteSelectionMultipleMode(note);
                                break;
                            case Keys.Shift:
                                // TODO: select a series of notes.
                                Debug.Print("TODO: select a series of notes.");
                                EditorToggleNoteSelection(note);
                                break;
                            case Keys.None:
                                EditorToggleNoteSelection(note);
                                break;
                        }
                        editor.Invalidate();
                    }
                }
            } else {
                ClearNoteAndBarSelection();
            }
        }

        private void EditNoteModeTap(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;
            if (hit.HitAnyNote) {
                var note = hit.Note;
                if (note == _lastMouseDownNote) {
                    // If we hit any note, its behavior is like 'select' mode, except that this one doesn't accept
                    // modifier keys.
                    if (editor.HasSelectedBars) {
                        editor.ClearSelectedBars();
                    } else {
                        EditorToggleNoteSelection(note);
                    }
                    editor.Invalidate();
                }
            } else if (hit.HitBarGridIntersection) {
                // Add a note
                EditorAddNote(hit);
                editor.Invalidate();
            } else {
                ClearNoteAndBarSelection();
            }
        }

        private void EditNoteModeHoldFlick(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;

            Note lastNote = null;
            if (editor.HasOneSelectedNote) {
                lastNote = editor.GetSelectedNote();
            } else if (hit.HitAnyNote && editor.HasSelectedNotes) {
                Debug.Print("You can only select one note to create a hold pair.");
                return;
            }

            Note thisNote = null;
            var isNoteAdded = false;
            if (hit.HitAnyNote) {
                // The clicked note is always selected.
                thisNote = hit.Note == _lastMouseDownNote ? hit.Note : null;
            } else if (hit.HitBarGridIntersection) {
                // If not selected, add a note and select it.
                thisNote = EditorAddNote(hit);
                isNoteAdded = true;
            }

            // If the user clicked on nothing, then clear all selections.
            if (thisNote == null) {
                ClearNoteAndBarSelection();
                editor.Invalidate();
                return;
            }

            // If the user clicked on the same note, just perform the standard note selection.
            if (lastNote == null || lastNote == thisNote) {
                thisNote.EditorToggleSelected();
                editor.Invalidate();
                return;
            }

            var relationCreated = false;
            if (thisNote.Basic.FinishPosition == lastNote.Basic.FinishPosition) {
                do {
                    // If the selected note is already a hold note (start/end) and there is a hold relation between the
                    // two notes, then switch selection.
                    if (NoteUtilities.AreNotesInHoldChain(thisNote, lastNote)) {
                        // Yep just switch selection. Do nothing in this branch.
                    } else {
                        var errStr = EnsureHoldValid(thisNote, lastNote);
                        if (errStr != null) {
                            if (isNoteAdded) {
                                thisNote.Basic.Bar.RemoveNote(thisNote);
                            }
                            Debug.Print(errStr);
                            break;
                        }

                        // Make hold.
                        var (first, second) = NoteUtilities.Split(thisNote, lastNote);
                        NoteUtilities.MakeHold(first, second);
                        _visualizer.InformProjectModified();
                        relationCreated = true;
                    }
                } while (false);
            } else {
                do {
                    // If the selected note is already a flick note and there is a flick relation between the
                    // two notes, then switch selection.
                    if (NoteUtilities.AreNotesInFlickChain(thisNote, lastNote)) {
                        // Yep just switch selection. Do nothing in this branch.
                    } else {
                        var errStr = EnsureFlickValid(thisNote, lastNote);
                        if (errStr != null) {
                            if (isNoteAdded) {
                                thisNote.Basic.Bar.RemoveNote(thisNote);
                            }
                            Debug.Print(errStr);
                            break;
                        }

                        // Make flick.
                        var (first, second) = NoteUtilities.Split(thisNote, lastNote);
                        NoteUtilities.MakeFlick(first, second);
                        _visualizer.InformProjectModified();
                        relationCreated = true;
                    }
                } while (false);
            }

            // Now handle a special case: link flick after a slide.
            if (!relationCreated) {
                var (first, second) = NoteUtilities.Split(thisNote, lastNote);
                if (first.Helper.IsSlideEnd && second.Helper.IsFlickStart) {
                    NoteUtilities.MakeSlideToFlick(first, second);
                    _visualizer.InformProjectModified();
                    relationCreated = true;
                }
            }

            if (relationCreated) {
                thisNote.EditorUnselect();
                lastNote.EditorUnselect();
            } else {
                lastNote.EditorUnselect();
                thisNote.EditorSelect();
            }

            editor.Invalidate();
        }

        private void EditNoteModeSlide(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;

            Note lastNote = null;
            if (editor.HasOneSelectedNote) {
                lastNote = editor.GetSelectedNote();
            } else if (hit.HitAnyNote && editor.HasSelectedNotes) {
                Debug.Print("You can only select one note to create a slide group.");
                return;
            }

            Note thisNote = null;
            var isNoteAdded = false;
            if (hit.HitAnyNote) {
                // The clicked note is always selected.
                thisNote = hit.Note == _lastMouseDownNote ? hit.Note : null;
            } else if (hit.HitBarGridIntersection) {
                // If not selected, add a note and select it.
                thisNote = EditorAddNote(hit);
                isNoteAdded = true;
            }

            // If the user clicked on nothing, then clear all selections.
            if (thisNote == null) {
                ClearNoteAndBarSelection();
                editor.Invalidate();
                return;
            }

            // If the user clicked on the same note, just perform the standard note selection.
            if (lastNote == null || lastNote == thisNote) {
                thisNote.EditorToggleSelected();
                editor.Invalidate();
                return;
            }

            var relationCreated = false;
            do {
                // If the selected note is already a slide note and there is a slide relation between the
                // two notes, then switch selection.
                if (NoteUtilities.AreNotesInSlideChain(thisNote, lastNote)) {
                    // Yep just switch selection. Do nothing in this branch.
                } else {
                    var errStr = EnsureSlideValid(thisNote, lastNote);
                    if (errStr != null) {
                        if (isNoteAdded) {
                            thisNote.Basic.Bar.RemoveNote(thisNote);
                        }
                        Debug.Print(errStr);
                        break;
                    }

                    // Make slide.
                    var (first, second) = NoteUtilities.Split(thisNote, lastNote);
                    NoteUtilities.MakeSlide(first, second);
                    _visualizer.InformProjectModified();
                    relationCreated = true;
                }
            } while (false);

            // Now handle a special case: link flick after a slide.
            if (!relationCreated) {
                var (first, second) = NoteUtilities.Split(thisNote, lastNote);
                if (first.Helper.IsSlideEnd && second.Helper.IsFlickStart) {
                    NoteUtilities.MakeSlideToFlick(first, second);
                    _visualizer.InformProjectModified();
                    relationCreated = true;
                }
            }

            if (relationCreated) {
                thisNote.EditorUnselect();
                lastNote.EditorUnselect();
            } else {
                lastNote.EditorUnselect();
                thisNote.EditorSelect();
            }

            editor.Invalidate();
        }

        private static string EnsureHoldValid(Note targetNote, Note lastNote) {
            // Check: columns must be the same.
            if (lastNote.Basic.FinishPosition != targetNote.Basic.FinishPosition) {
                return ("Both notes should be on the same column.");
            }

            var (firstNote, secondNote) = NoteUtilities.Split(targetNote, lastNote);

            // Check: the first note must be a tap note.
            if (!firstNote.Helper.IsTap) {
                return ("The first note must be a tap note.");
            }
            // Check: the first note must not be a hold note.
            if (firstNote.Helper.IsHold) {
                return ("The first note must not be a hold start note or hold end note.");
            }
            // Check: the second note must not be a hold note.
            if (secondNote.Helper.IsHold) {
                return ("The second note must not be a hold start note or hold end note.");
            }

            var targetBar = targetNote.Basic.Bar;
            var lastBar = lastNote.Basic.Bar;
            var firstBarIndex = Math.Min(lastBar.Basic.Index, targetBar.Basic.Index);
            var secondBarIndex = firstBarIndex == targetBar.Basic.Index ? lastBar.Basic.Index : targetBar.Basic.Index;

            // Check: no other notes can be between these two notes.
            var anyNoteInBetween = false;
            if (secondBarIndex == firstBarIndex) {
                var firstRow = Math.Min(lastNote.Basic.IndexInGrid, targetNote.Basic.IndexInGrid);
                var secondRow = firstRow == lastNote.Basic.IndexInGrid ? targetNote.Basic.IndexInGrid : lastNote.Basic.IndexInGrid;
                anyNoteInBetween = targetBar.Notes.Any(n => n.Basic.FinishPosition == targetNote.Basic.FinishPosition && firstRow < n.Basic.IndexInGrid && n.Basic.IndexInGrid < secondRow);
            } else {
                var score = targetBar.Basic.Score;
                var firstBar = targetBar.Basic.Index == firstBarIndex ? targetBar : lastBar;
                var secondBar = firstBar == targetBar ? lastBar : targetBar;
                var firstRow = firstBar == targetBar ? targetNote.Basic.IndexInGrid : lastNote.Basic.IndexInGrid;
                var secondRow = firstBar == targetBar ? lastNote.Basic.IndexInGrid : targetNote.Basic.IndexInGrid;
                foreach (var bar in score.Bars) {
                    if (bar.Basic.Index < firstBarIndex) {
                        continue;
                    }
                    if (bar.Basic.Index > secondBarIndex) {
                        break;
                    }
                    IEnumerable<Note> notes;
                    if (bar == firstBar) {
                        notes = bar.Notes.Where(n => n.Basic.FinishPosition == targetNote.Basic.FinishPosition && n.Basic.IndexInGrid > firstRow);
                    } else if (bar == secondBar) {
                        notes = bar.Notes.Where(n => n.Basic.FinishPosition == targetNote.Basic.FinishPosition && n.Basic.IndexInGrid < secondRow);
                    } else {
                        notes = bar.Notes.Where(n => n.Basic.FinishPosition == targetNote.Basic.FinishPosition);
                    }
                    anyNoteInBetween = notes.Any();
                    if (anyNoteInBetween) {
                        break;
                    }
                }
            }
            if (anyNoteInBetween) {
                return ("There must not be any other notes between a hold note pair.");
            }

            return null;
        }

        private static string EnsureFlickValid(Note targetNote, Note lastNote) {
            // Check: rows must not be the same.
            if (targetNote.Basic.Bar == lastNote.Basic.Bar && targetNote.Basic.IndexInGrid == lastNote.Basic.IndexInGrid) {
                return "The rows of the notes must be different.";
            }
            // Check: columns must not be the same.
            if (targetNote.Basic.FinishPosition == lastNote.Basic.FinishPosition) {
                return "The columns of the notes must be different.";
            }

            var (firstNote, secondNote) = NoteUtilities.Split(targetNote, lastNote);

            // Check: the first note must not be a hold start.
            if (firstNote.Helper.IsHoldStart) {
                return "The first note must not be a hold start note.";
            }
            // Check: the second note must not be a hold start.
            if (secondNote.Helper.IsHoldStart) {
                return "The second note must not be a hold start note.";
            }
            // Check: the first note must not be a note in the middle of a flick group or start of a flick group.
            if (firstNote.Helper.IsFlickMidway || firstNote.Helper.IsFlickStart) {
                return "The first note must not be the start or middle of a flick group.";
            }
            // Check: the second note must not be a note in the middle of a flick group or end of a flick group.
            if (secondNote.Helper.IsFlickMidway || secondNote.Helper.IsFlickEnd) {
                return "The second note must not be the middle or end a flick group.";
            }
            // Check: the first note must not be a note in the middle of a slide group or start of a slide group.
            if (firstNote.Helper.IsSlideMidway || firstNote.Helper.IsSlideStart) {
                return "The first note must not be the start or middle a slide group.";
            }
            // Check: the second note must not be a note in the middle of a slide group or end of a slide group.
            if (secondNote.Helper.IsSlideMidway || secondNote.Helper.IsSlideEnd) {
                return "The second note must not be the middle or end a slide group.";
            }

            return null;
        }

        private static string EnsureSlideValid(Note targetNote, Note lastNote) {
            // Check: rows must not be the same.
            if (targetNote.Basic.Bar == lastNote.Basic.Bar && targetNote.Basic.IndexInGrid == lastNote.Basic.IndexInGrid) {
                return ("The rows of the notes must be different.");
            }

            var (firstNote, secondNote) = NoteUtilities.Split(targetNote, lastNote);

            // Check: the first note must not be a hold start.
            if (firstNote.Helper.IsHoldStart) {
                return ("The first note must not be a hold start note.");
            }
            // Check: the second note must not be a hold start.
            if (secondNote.Helper.IsHoldStart) {
                return ("The second note must not be a hold start note.");
            }
            // Check: the first note must not be a note in the middle of a flick group or start of a flick group.
            if (firstNote.Helper.IsFlick) {
                return ("The first note must not be a flick note.");
            }
            // Check: the second note must not be a note in the middle of a flick group or end of a flick group.
            if (secondNote.Helper.IsFlickMidway || secondNote.Helper.IsFlickEnd) {
                return ("The second note must not be the middle or end a flick group.");
            }
            // Check: the first note must not be a note in the middle of a slide group or start of a slide group.
            if (firstNote.Helper.IsSlideMidway || firstNote.Helper.IsSlideStart) {
                return ("The first note must not be the start or middle a slide group.");
            }
            // Check: the second note must not be a note in the middle of a slide group or end of a slide group.
            if (secondNote.Helper.IsSlideMidway || secondNote.Helper.IsSlideEnd) {
                return ("The second note must not be the middle or end a slide group.");
            }

            return null;
        }

    }
}
