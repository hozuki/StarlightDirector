using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditorSelectionHandler {

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
                MessageBox.Show("You can only select one note to create a hold note.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Note thisHoldNote = null;
            bool isNoteAdded = false;
            if (hit.HitAnyNote) {
                // The clicked note is always selected.
                thisHoldNote = hit.Note == _lastMouseDownNote ? hit.Note : null;
            } else if (hit.HitBarGridIntersection) {
                // If not selected, add a note and select it.
                thisHoldNote = EditorAddNote(hit);
                isNoteAdded = true;
            }
            if (thisHoldNote != null) {
                if (lastNote != null) {
                    try {
                        EnsureHoldValid(thisHoldNote, lastNote);
                    } catch (InvalidOperationException ex) {
                        if (isNoteAdded) {
                            thisHoldNote.Basic.Bar.RemoveNote(thisHoldNote);
                        }
                        MessageBox.Show(ex.Message, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Make hold.
                    var (first, second) = NoteUtilities.Split(thisHoldNote, lastNote);
                    NoteUtilities.MakeHold(first, second);

                    thisHoldNote.EditorUnselect();
                    lastNote.EditorUnselect();
                } else {
                    thisHoldNote.EditorSelect();
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
                try {
                    EnsureFlickValid(hit.Bar, hit.Row, hit.Column, lastNote);
                } catch (InvalidOperationException ex) {
                    MessageBox.Show(ex.Message, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            } else if (editor.HasSelectedNotes) {
                MessageBox.Show("You can only select one note to create a flick group.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Note thisFlickNote = null;
            if (hit.HitAnyNote) {
                // The clicked note is always selected.
                thisFlickNote = hit.Note == _lastMouseDownNote ? hit.Note : null;
            } else if (hit.HitBarGridIntersection) {
                // If not selected, add a note and select it.
                thisFlickNote = EditorAddNote(hit);
            }
            if (thisFlickNote != null) {
                if (lastNote != null) {
                    // TODO: Make flick.

                    lastNote.EditorUnselect();
                }
                thisFlickNote.EditorSelect();
            } else {
                ClearNoteAndBarSelection();
            }
            editor.Invalidate();
        }

        private void EditNoteModeSlide(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;

            if (editor.HasOneSelectedNote) {
                try {
                    EnsureSlideValid(hit.Bar, hit.Row, hit.Column, editor.GetSelectedNote());
                } catch (InvalidOperationException ex) {
                    MessageBox.Show(ex.Message, ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            } else if (editor.HasSelectedNotes) {
                MessageBox.Show("You can only select one note to create a slide group.", ApplicationHelper.GetTitle(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Note thisSlideNote = null;
            if (hit.HitAnyNote) {
                // The clicked note is always selected.
                thisSlideNote = hit.Note == _lastMouseDownNote ? hit.Note : null;
            } else if (hit.HitBarGridIntersection) {
                // If not selected, add a note and select it.
                thisSlideNote = EditorAddNote(hit);
            }
            if (thisSlideNote != null) {
                thisSlideNote.EditorSelect();

                // TODO: Make slide.
            } else {
                ClearNoteAndBarSelection();
            }
            editor.Invalidate();
        }

        private void EnsureHoldValid(Note targetNote, Note lastNote) {
            // Check: columns must be the same.
            if (lastNote.Basic.FinishPosition != targetNote.Basic.FinishPosition) {
                throw new InvalidOperationException("Both hold notes should be on the same column.");
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
            var firstBarIndex = Math.Min(lastBar.Index, targetBar.Index);
            var secondBarIndex = firstBarIndex == targetBar.Index ? lastBar.Index : targetBar.Index;

            // Check: no other notes can be between these two notes.
            bool anyNoteInBetween = false;
            if (secondBarIndex == firstBarIndex) {
                var firstRow = Math.Min(lastNote.Basic.IndexInGrid, targetNote.Basic.IndexInGrid);
                var secondRow = firstRow == lastNote.Basic.IndexInGrid ? targetNote.Basic.IndexInGrid : lastNote.Basic.IndexInGrid;
                anyNoteInBetween = targetBar.Notes.Any(n => n.Basic.FinishPosition == targetNote.Basic.FinishPosition && firstRow < n.Basic.IndexInGrid && n.Basic.IndexInGrid < secondRow);
            } else {
                var score = targetBar.Score;
                var firstBar = targetBar.Index == firstBarIndex ? targetBar : lastBar;
                var secondBar = firstBar == targetBar ? lastBar : targetBar;
                var firstRow = firstBar == targetBar ? targetNote.Basic.IndexInGrid : lastNote.Basic.IndexInGrid;
                var secondRow = firstBar == targetBar ? lastNote.Basic.IndexInGrid : targetNote.Basic.IndexInGrid;
                foreach (var bar in score.Bars) {
                    if (bar.Index < firstBarIndex) {
                        continue;
                    }
                    if (bar.Index > secondBarIndex) {
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

        private void EnsureFlickValid(Bar targetBar, int targetRow, NotePosition targetColumn, Note lastNote) {
            // Check: note selection must not be 0 or 1 note.
            // Done.
            Note previousNote = null;
            //foreach (var note in selectedNotes) {
            //    // Check: any note must not be a hold start.
            //    if (note.Helper.IsHoldStart) {
            //        return;
            //    }
            //    // Check: any note must not be a note in the middle of a flick group.
            //    if (note.Helper.IsFlickMidway) {
            //        return;
            //    }
            //    // Check: any note must not be a note in the middle of a slide group.
            //    if (note.Helper.IsSlideMidway) {
            //        return;
            //    }
            //    // Check: the note and its previous note must not be on the same row.
            //    if (previousNote != null && previousNote.IsOnTheSameRowWith(note)) {
            //        return;
            //    }
            //    // Check: the note and its previous note must not be on the same column.
            //    if (previousNote != null && previousNote.Basic.FinishPosition == note.Basic.FinishPosition) {
            //        return;
            //    }
            //    // TODO: Check: there must no be any slide groups between two notes.
            //}
        }

        private void EnsureSlideValid(Bar targetBar, int targetRow, NotePosition targetColumn, Note lastNote) {
        }

    }
}
