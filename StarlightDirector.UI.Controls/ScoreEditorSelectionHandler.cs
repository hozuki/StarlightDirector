using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.UI.Controls.Extensions;

namespace StarlightDirector.UI.Controls {
    internal sealed partial class ScoreEditorSelectionHandler {

        public ScoreEditorSelectionHandler(ScoreVisualizer visualizer) {
            _visualizer = visualizer;
        }

        public void OnMouseDown(MouseEventArgs e) {
            var editor = _visualizer.Editor;
            var hit = editor.HitTest(e.Location);
            _mouseDownHitResult = hit;
            // If the mousedown note and mouseup note are not the same, we did not perform a successful 'click' on a note.
            _lastMouseDownNote = hit.Note;
            _selectionRectangle.Location = e.Location;
            switch (hit.HitRegion) {
                case ScoreEditorHitRegion.None:
                    break;
                case ScoreEditorHitRegion.InfoArea:
                    InfoAreaOnMouseDown(hit, e);
                    break;
                case ScoreEditorHitRegion.GridNumberArea:
                    GridNumberAreaOnMouseDown(hit, e);
                    break;
                case ScoreEditorHitRegion.GridArea:
                    GridAreaOnMouseDown(hit, e);
                    break;
                case ScoreEditorHitRegion.SpecialNoteArea:
                    SpecialNoteAreaOnMouseDown(hit, e);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(hit.HitRegion), hit.HitRegion, null);
            }
        }

        public void OnMouseUp(MouseEventArgs e) {
            if (_mouseDownHitResult != null) {
                var editor = _visualizer.Editor;
                var hit = editor.HitTest(e.Location);
                switch (hit.HitRegion) {
                    case ScoreEditorHitRegion.None:
                        ClearNoteAndBarSelection();
                        break;
                    case ScoreEditorHitRegion.InfoArea:
                        InfoAreaOnMouseUp(hit, e);
                        break;
                    case ScoreEditorHitRegion.GridNumberArea:
                        GridNumberAreaOnMouseUp(hit, e);
                        break;
                    case ScoreEditorHitRegion.GridArea:
                        GridAreaOnMouseUp(hit, e);
                        break;
                    case ScoreEditorHitRegion.SpecialNoteArea:
                        SpecialNoteAreaOnMouseUp(hit, e);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(hit.HitRegion), hit.HitRegion, null);
                }
                _mouseDownHitResult = null;
            }
            _selectionRectangle.Clear();
            _lastMouseDownNote = null;
        }

        public void OnMouseMove(MouseEventArgs e) {
            var button = e.Button;
            if (button == MouseButtons.Left) {
                var rect = _selectionRectangle;
                _selectionRectangle.Size = new Size(e.X - rect.X, e.Y - rect.Y);
            }
        }

        public void OnEditModeChanged() {
            var editor = _visualizer.Editor;
            editor.ClearSelectedBars();
            editor.ClearSelectedNotes();
            editor.Invalidate();
        }

        private void InfoAreaOnMouseDown(ScoreEditorHitTestResult hit, MouseEventArgs e) {
        }

        private void InfoAreaOnMouseUp(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;
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
                                switch (Control.ModifierKeys) {
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
        }

        private void GridNumberAreaOnMouseDown(ScoreEditorHitTestResult hit, MouseEventArgs e) {
        }

        private void GridNumberAreaOnMouseUp(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            ClearNoteAndBarSelection();
        }

        private void GridAreaOnMouseDown(ScoreEditorHitTestResult hit, MouseEventArgs e) {
        }

        private void GridAreaOnMouseUp(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            var editor = _visualizer.Editor;
            switch (e.Button) {
                case MouseButtons.Left:
                    // Whatever note you hit, change its start up position.
                    // If no note is hit and we are hitting a grid crossing, the EditorAddNote method is invoked
                    // and the newly added note's StartPosition is automatically set there.
                    if (hit.HitAnyNote && editor.NoteStartPosition != NotePosition.Nowhere) {
                        hit.Note.Basic.StartPosition = editor.NoteStartPosition;
                    }
                    // Then handle the mode-specific actions.
                    switch (editor.EditMode) {
                        case ScoreEditMode.Select:
                            EditNoteModeSelect(hit, e);
                            break;
                        case ScoreEditMode.Tap:
                            EditNoteModeTap(hit, e);
                            break;
                        case ScoreEditMode.Hold:
                            EditNoteModeHold(hit, e);
                            break;
                        case ScoreEditMode.Flick:
                            EditNoteModeFlick(hit, e);
                            break;
                        case ScoreEditMode.Slide:
                            EditNoteModeSlide(hit, e);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(editor.EditMode), editor.EditMode, null);
                    }
                    break;
                case MouseButtons.Right:
                    EditorPopupContextMenu(hit, e.Location);
                    break;
            }
        }

        private void SpecialNoteAreaOnMouseDown(ScoreEditorHitTestResult hit, MouseEventArgs e) {
        }

        private void SpecialNoteAreaOnMouseUp(ScoreEditorHitTestResult hit, MouseEventArgs e) {
            ClearNoteAndBarSelection();
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

        private Note EditorAddNote(ScoreEditorHitTestResult hit) {
            var editor = _visualizer.Editor;
            var note = editor.AddNoteAt(hit.Bar, hit.Row, hit.Column);
            return note;
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

        private void ClearNoteAndBarSelection() {
            var editor = _visualizer.Editor;
            if (editor.HasSelectedNotes) {
                editor.ClearSelectedNotes();
                editor.Invalidate();
            } else if (editor.HasSelectedBars) {
                editor.ClearSelectedBars();
                editor.Invalidate();
            }
        }

        private ScoreEditorHitTestResult _mouseDownHitResult;
        
        private Note _lastMouseDownNote;

        private Rectangle _selectionRectangle;

        private readonly ScoreVisualizer _visualizer;

    }
}
