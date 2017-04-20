using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;

namespace StarlightDirector.UI.Controls {
    internal partial class ScoreEditorSelectionHandler {

        public ScoreEditorSelectionHandler(ScoreVisualizer visualizer) {
            _visualizer = visualizer;
        }

        public void OnMouseDown(MouseEventArgs e) {
            var editor = _visualizer.Editor;
            var hit = editor.HitTest(e.Location);
            _mouseDownHitTestResult = hit;
            if (hit.Bar != null) {
                _selection.LastHitBar = hit.Bar;
            }
            if (hit.Note != null) {
                _selection.LastHitNote = hit.Note;
            }
            Debug.Print("Not implemented.");
        }

        public void OnMouseUp(MouseEventArgs e) {
            var editor = _visualizer.Editor;
            var hit = editor.HitTest(e.Location);
            var modifiers = Control.ModifierKeys;
            switch (hit.HitRegion) {
                case ScoreEditorHitRegion.None:
                    // Clear all selection.
                    ClearNoteAndBarSelection();
                    break;
                case ScoreEditorHitRegion.InfoArea:
                    switch (e.Button) {
                        case MouseButtons.Left: {
                                // Click info area to select measures.
                                if (editor.HasSelectedNotes) {
                                    // Clear note selection first.
                                    editor.ClearSelectedNotes();
                                    editor.Invalidate();
                                } else {
                                    // Select/unselect bar(s).
                                    var bar = hit.Bar;
                                    if (bar != null) {
                                        switch (modifiers) {
                                            case Keys.Control:
                                                EditorToggleBarSelectionMultipleMode(bar);
                                                break;
                                            case Keys.Shift:
                                            // TODO: select a series of measures.
                                            //break;
                                            case Keys.None:
                                                EditorToggleBarSelection(bar);
                                                break;
                                        }
                                        editor.Invalidate();
                                    } else {
                                        ClearNoteAndBarSelection();
                                    }
                                }
                                break;
                            }
                        case MouseButtons.Right:
                            EditorPopupContextMenu(hit, e.Location);
                            break;
                    }
                    break;
                case ScoreEditorHitRegion.GridNumberArea:
                    ClearNoteAndBarSelection();
                    break;
                case ScoreEditorHitRegion.GridArea:
                    switch (e.Button) {
                        case MouseButtons.Left:
                            if (hit.HitAnyNote) {
                                if (editor.HasSelectedBars) {
                                    editor.ClearSelectedBars();
                                } else {
                                    var note = hit.Note;
                                    switch (modifiers) {
                                        case Keys.Control:
                                            EditorToggleNoteSelectionMultipleMode(note);
                                            break;
                                        case Keys.Shift:
                                        // TODO: select a series of notes.
                                        //break;
                                        case Keys.None:
                                            EditorToggleNoteSelection(note);
                                            break;
                                    }
                                }
                                editor.Invalidate();
                            } else if (hit.HitBarGridIntersection) {
                                // Add a note
                                EditorAddNote(hit);
                            } else {
                                ClearNoteAndBarSelection();
                            }
                            break;
                        case MouseButtons.Right:
                            EditorPopupContextMenu(hit, e.Location);
                            break;
                    }
                    break;
                case ScoreEditorHitRegion.SpecialNoteArea:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hit.HitRegion), hit.HitRegion, null);
            }

            _mouseDownHitTestResult = null;

            void ClearNoteAndBarSelection()
            {
                if (editor.HasSelectedNotes) {
                    editor.ClearSelectedNotes();
                    editor.Invalidate();
                } else if (editor.HasSelectedBars) {
                    editor.ClearSelectedBars();
                    editor.Invalidate();
                }
            }
        }

        public void OnMouseMove(MouseEventArgs e) {

        }

        public void OnEditModeChanged() {
            _selection.Clear();
            _visualizer.Editor.Invalidate();
        }

        private void EditorToggleNoteSelection(Note note) {
            var editor = _visualizer.Editor;
            if (editor.HasSelectedNotes) {
                editor.ClearSelectedNotesExcept(note);
            }
            note.EditorToggleSelected();
        }

        private void EditorToggleNoteSelectionMultipleMode(Note note) {
            note.EditorToggleSelected();
        }

        private void EditorAddNote(ScoreEditorHitTestResult hit) {
            var editor = _visualizer.Editor;
            editor.AddNoteAt(hit.Bar, hit.Row, hit.Column);
            editor.Invalidate();
        }

        private void EditorToggleBarSelection(Bar bar) {
            var editor = _visualizer.Editor;
            if (editor.HasSelectedBars) {
                editor.ClearSelectedBarsExcept(bar);
            }
            bar.EditorToggleSelected();
        }

        private void EditorToggleBarSelectionMultipleMode(Bar bar) {
            bar.EditorToggleSelected();
        }

        private void EditorPopupContextMenu(ScoreEditorHitTestResult hit, Point location) {
            var ea = new ContextMenuRequestedEventArgs(hit.HitAnyNote ? VisualizerContextMenu.Note : VisualizerContextMenu.Bar, location);
            _visualizer.RequestContextMenu(ea);
        }

        //    // Check: note selection must not be empty.
        //    if (!visualizer.Editor.HasSelectedNotes) {
        //        return;
        //    }
        //    var selectedNotes = visualizer.Editor.GetSelectedNotes().ToList();
        //    // Check: must have at least 2 selected notes.
        //    if (selectedNotes.Count < 2) {
        //        return;
        //    }
        //    selectedNotes.Sort(Note.TimingThenPositionComparison);
        //    Note previousNote = null;
        //    foreach (var note in selectedNotes) {
        //        // Check: any note must not be a hold start.
        //        if (note.Helper.IsHoldStart) {
        //            return;
        //        }
        //        // Check: any note must not be a note in the middle of a flick group.
        //        if (note.Helper.IsFlickMidway) {
        //            return;
        //        }
        //        // Check: any note must not be a note in the middle of a slide group.
        //        if (note.Helper.IsSlideMidway) {
        //            return;
        //        }
        //        // Check: the note and its previous note must not be on the same row.
        //        if (previousNote != null && previousNote.IsOnTheSameRowWith(note)) {
        //            return;
        //        }
        //        // Check: the note and its previous note must not be on the same column.
        //        if (previousNote != null && previousNote.Basic.FinishPosition == note.Basic.FinishPosition) {
        //            return;
        //        }
        //        // TODO: Check: there must no be any slide groups between two notes.

        private ScoreEditorHitTestResult _mouseDownHitTestResult;
        private readonly ElementSelection _selection = new ElementSelection();
        private readonly ScoreVisualizer _visualizer;

    }
}
