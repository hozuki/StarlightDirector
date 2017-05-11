using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.UI.Controls {
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

        private void EditNoteModeHold(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;

            Note lastNote = null;
            if (editor.HasOneSelectedNote) {
                lastNote = editor.GetSelectedNote();
            } else if (editor.HasSelectedNotes) {
                //MessageBox.Show("You can only select one note to create a hold pair.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Debug.Print("You can only select one note to create a hold pair.");
                return;
            }

            Note thisNote = null;
            bool isNoteAdded = false;
            if (hit.HitAnyNote) {
                // The clicked note is always selected.
                thisNote = hit.Note == _lastMouseDownNote ? hit.Note : null;
            } else if (hit.HitBarGridIntersection) {
                // If not selected, add a note and select it.
                thisNote = EditorAddNote(hit);
                isNoteAdded = true;
            }

            if (thisNote != null) {
                if (lastNote != null && lastNote != thisNote) {
                    // If the selected note is already a hold note (start/end) and there is a hold relation between the
                    // two notes, then switch selection.
                    if (NoteUtilities.AreNotesInHoldChain(thisNote, lastNote)) {
                        // Yep just switch selection. Do nothing in this branch.
                    } else {
                        try {
                            EnsureHoldValid(thisNote, lastNote);
                        } catch (InvalidOperationException ex) {
                            if (isNoteAdded) {
                                thisNote.Basic.Bar.RemoveNote(thisNote);
                            }
                            //MessageBox.Show(ex.Message, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Debug.Print(ex.Message);
                            return;
                        }

                        // Make hold.
                        var (first, second) = NoteUtilities.Split(thisNote, lastNote);
                        NoteUtilities.MakeHold(first, second);
                        _visualizer.InformProjectModified();
                    }
                    thisNote.EditorUnselect();
                    lastNote.EditorUnselect();
                } else {
                    thisNote.EditorSelect();
                }
            } else {
                ClearNoteAndBarSelection();
            }
            editor.Invalidate();
        }

        private void EditNoteModeFlick(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;

            Note lastNote = null;
            if (editor.HasOneSelectedNote) {
                lastNote = editor.GetSelectedNote();
            } else if (editor.HasSelectedNotes) {
                //MessageBox.Show("You can only select one note to create a flick group.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Debug.Print("You can only select one note to create a flick group.");
                return;
            }

            Note thisNote = null;
            bool isNoteAdded = false;
            if (hit.HitAnyNote) {
                // The clicked note is always selected.
                thisNote = hit.Note == _lastMouseDownNote ? hit.Note : null;
            } else if (hit.HitBarGridIntersection) {
                // If not selected, add a note and select it.
                thisNote = EditorAddNote(hit);
                isNoteAdded = true;
            }

            if (thisNote != null) {
                if (lastNote != null && lastNote != thisNote) {
                    // If the selected note is already a flick note and there is a flick relation between the
                    // two notes, then switch selection.
                    if (NoteUtilities.AreNotesInFlickChain(thisNote, lastNote)) {
                        // Yep just switch selection. Do nothing in this branch.
                    } else {
                        try {
                            EnsureFlickValid(thisNote, lastNote);
                        } catch (InvalidOperationException ex) {
                            if (isNoteAdded) {
                                thisNote.Basic.Bar.RemoveNote(thisNote);
                            }
                            //MessageBox.Show(ex.Message, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Debug.Print(ex.Message);
                            return;
                        }

                        // Make flick.
                        var (first, second) = NoteUtilities.Split(thisNote, lastNote);
                        NoteUtilities.MakeFlick(first, second);
                        _visualizer.InformProjectModified();
                    }
                    lastNote.EditorUnselect();
                    thisNote.EditorSelect();
                } else {
                    thisNote.EditorSelect();
                }
            } else {
                ClearNoteAndBarSelection();
            }
            editor.Invalidate();
        }

        private void EditNoteModeSlide(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;

            Note lastNote = null;
            if (editor.HasOneSelectedNote) {
                lastNote = editor.GetSelectedNote();
            } else if (editor.HasSelectedNotes) {
                //MessageBox.Show("You can only select one note to create a slide group.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Debug.Print("You can only select one note to create a slide group.");
                return;
            }

            Note thisNote = null;
            bool isNoteAdded = false;
            if (hit.HitAnyNote) {
                // The clicked note is always selected.
                thisNote = hit.Note == _lastMouseDownNote ? hit.Note : null;
            } else if (hit.HitBarGridIntersection) {
                // If not selected, add a note and select it.
                thisNote = EditorAddNote(hit);
                isNoteAdded = true;
            }

            if (thisNote != null) {
                if (lastNote != null && lastNote != thisNote) {
                    // If the selected note is already a slide note and there is a slide relation between the
                    // two notes, then switch selection.
                    if (NoteUtilities.AreNotesInSlideChain(thisNote, lastNote)) {
                        // Yep just switch selection. Do nothing in this branch.
                    } else {
                        try {
                            EnsureSlideValid(thisNote, lastNote);
                        } catch (InvalidOperationException ex) {
                            if (isNoteAdded) {
                                thisNote.Basic.Bar.RemoveNote(thisNote);
                            }
                            //MessageBox.Show(ex.Message, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Debug.Print(ex.Message);
                            return;
                        }

                        // Make slide.
                        var (first, second) = NoteUtilities.Split(thisNote, lastNote);
                        NoteUtilities.MakeSlide(first, second);
                        _visualizer.InformProjectModified();
                    }
                    lastNote.EditorUnselect();
                    thisNote.EditorSelect();
                } else {
                    thisNote.EditorSelect();
                }
            } else {
                ClearNoteAndBarSelection();
            }
            editor.Invalidate();
        }

        private void EnsureHoldValid(Note targetNote, Note lastNote) {
            // Check: columns must be the same.
            if (lastNote.Basic.FinishPosition != targetNote.Basic.FinishPosition) {
                throw new InvalidOperationException("Both notes should be on the same column.");
            }

            var (firstNote, secondNote) = NoteUtilities.Split(targetNote, lastNote);

            // Check: the first note must be a tap note.
            if (!firstNote.Helper.IsTap) {
                throw new InvalidOperationException("The first note must be a tap note.");
            }
            // Check: the first note must not be a hold note.
            if (firstNote.Helper.IsHold) {
                throw new InvalidOperationException("The first note must not be a hold start note or hold end note.");
            }
            // Check: the second note must not be a hold note.
            if (secondNote.Helper.IsHold) {
                throw new InvalidOperationException("The second note must not be a hold start note or hold end note.");
            }

            var targetBar = targetNote.Basic.Bar;
            var lastBar = lastNote.Basic.Bar;
            var firstBarIndex = Math.Min(lastBar.Basic.Index, targetBar.Basic.Index);
            var secondBarIndex = firstBarIndex == targetBar.Basic.Index ? lastBar.Basic.Index : targetBar.Basic.Index;

            // Check: no other notes can be between these two notes.
            bool anyNoteInBetween = false;
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
                throw new InvalidOperationException("There must not be any other notes between a hold note pair.");
            }
        }

        private void EnsureFlickValid(Note targetNote, Note lastNote) {
            // Check: rows must not be the same.
            if (targetNote.Basic.Bar == lastNote.Basic.Bar && targetNote.Basic.IndexInGrid == lastNote.Basic.IndexInGrid) {
                throw new InvalidOperationException("The rows of the notes must be different.");
            }
            // Check: columns must not be the same.
            if (targetNote.Basic.FinishPosition == lastNote.Basic.FinishPosition) {
                throw new InvalidOperationException("The columns of the notes must be different.");
            }

            var (firstNote, secondNote) = NoteUtilities.Split(targetNote, lastNote);

            // Check: the first note must not be a hold start.
            if (firstNote.Helper.IsHoldStart) {
                throw new InvalidOperationException("The first note must not be a hold start note.");
            }
            // Check: the second note must not be a hold start.
            if (secondNote.Helper.IsHoldStart) {
                throw new InvalidOperationException("The second note must not be a hold start note.");
            }
            // Check: the first note must not be a note in the middle of a flick group or start of a flick group.
            if (firstNote.Helper.IsFlickMidway || firstNote.Helper.IsFlickStart) {
                throw new InvalidOperationException("The first note must not be the start or middle of a flick group.");
            }
            // Check: the second note must not be a note in the middle of a flick group or end of a flick group.
            if (secondNote.Helper.IsFlickMidway || secondNote.Helper.IsFlickEnd) {
                throw new InvalidOperationException("The second note must not be the middle or end a flick group.");
            }
            // Check: the first note must not be a note in the middle of a slide group or start of a slide group.
            if (firstNote.Helper.IsSlideMidway || firstNote.Helper.IsSlideStart) {
                throw new InvalidOperationException("The first note must not be the start or middle a slide group.");
            }
            // Check: the second note must not be a note in the middle of a slide group or end of a slide group.
            if (secondNote.Helper.IsSlideMidway || secondNote.Helper.IsSlideEnd) {
                throw new InvalidOperationException("The second note must not be the middle or end a slide group.");
            }
        }

        private void EnsureSlideValid(Note targetNote, Note lastNote) {
            // Check: rows must not be the same.
            if (targetNote.Basic.Bar == lastNote.Basic.Bar && targetNote.Basic.IndexInGrid == lastNote.Basic.IndexInGrid) {
                throw new InvalidOperationException("The rows of the notes must be different.");
            }

            var (firstNote, secondNote) = NoteUtilities.Split(targetNote, lastNote);

            // Check: the first note must not be a hold start.
            if (firstNote.Helper.IsHoldStart) {
                throw new InvalidOperationException("The first note must not be a hold start note.");
            }
            // Check: the second note must not be a hold start.
            if (secondNote.Helper.IsHoldStart) {
                throw new InvalidOperationException("The second note must not be a hold start note.");
            }
            // Check: the first note must not be a note in the middle of a flick group or start of a flick group.
            if (firstNote.Helper.IsFlickMidway || firstNote.Helper.IsFlickStart) {
                throw new InvalidOperationException("The first note must not be the start or middle of a flick group.");
            }
            // Check: the second note must not be a note in the middle of a flick group or end of a flick group.
            if (secondNote.Helper.IsFlickMidway || secondNote.Helper.IsFlickEnd) {
                throw new InvalidOperationException("The second note must not be the middle or end a flick group.");
            }
            // Check: the first note must not be a note in the middle of a slide group or start of a slide group.
            if (firstNote.Helper.IsSlideMidway || firstNote.Helper.IsSlideStart) {
                throw new InvalidOperationException("The first note must not be the start or middle a slide group.");
            }
            // Check: the second note must not be a note in the middle of a slide group or end of a slide group.
            if (secondNote.Helper.IsSlideMidway || secondNote.Helper.IsSlideEnd) {
                throw new InvalidOperationException("The second note must not be the middle or end a slide group.");
            }
        }

    }
}
