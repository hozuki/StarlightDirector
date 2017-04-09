using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.UI.Controls {
    public partial class ScoreVisualizer : UserControl {

        public ScoreVisualizer() {
            InitializeComponent();
            RegisterEventHandlers();

            editor.ScrollBar = vScroll;
        }

        ~ScoreVisualizer() {
            UnregisterEventHandlers();
        }

        public event EventHandler<ContextMenuRequestedEventArgs> ContextMenuRequested;

        [Browsable(false)]
        public ScoreEditor Editor => editor;

        [Browsable(false)]
        public VScrollBar ScrollBar => vScroll;

        public void RedrawScore() {
            editor.Invalidate();
        }

        protected override void OnLayout(LayoutEventArgs e) {
            // Anchor doesn't seem to work.
            base.OnLayout(e);

            var clientSize = ClientSize;
            var scrollBarLeft = clientSize.Width - vScroll.Width - ConstMargin * 2;
            var scrollBarTop = ConstMargin;
            var scrollBarHeight = clientSize.Height - ConstMargin * 2;
            vScroll.Location = new Point(scrollBarLeft, scrollBarTop);
            vScroll.Height = scrollBarHeight;

            var rendererLeft = ConstMargin;
            var rendererTop = ConstMargin;
            var rendererWidth = scrollBarLeft - ConstMargin;
            var rendererHeight = clientSize.Height - ConstMargin * 2;
            editor.Location = new Point(rendererLeft, rendererTop);
            editor.Size = new Size(rendererWidth, rendererHeight);
        }

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);

            var modifiers = ModifierKeys;
            if ((modifiers & Keys.Control) != 0) {
                if (e.Delta > 0) {
                    editor.ZoomIn();
                } else {
                    editor.ZoomOut();
                }
                return;
            }

            var newScrollValue = vScroll.Value;
            if (e.Delta > 0) {
                // Up
                if ((modifiers & Keys.Shift) != 0) {
                    newScrollValue -= vScroll.LargeChange;
                } else {
                    newScrollValue -= vScroll.SmallChange;
                }
            } else if (e.Delta < 0) {
                // Down
                if ((modifiers & Keys.Shift) != 0) {
                    newScrollValue += vScroll.LargeChange;
                } else {
                    newScrollValue += vScroll.SmallChange;
                }
            }
            newScrollValue = newScrollValue.Clamp(vScroll.Minimum, vScroll.Maximum);
            vScroll.Value = newScrollValue;
        }

        private void UnregisterEventHandlers() {
            vScroll.ValueChanged -= VScroll_ValueChanged;
            editor.MouseDown -= Editor_MouseDown;
        }

        private void RegisterEventHandlers() {
            vScroll.ValueChanged += VScroll_ValueChanged;
            editor.MouseDown += Editor_MouseDown;
        }

        private void Editor_MouseDown(object sender, MouseEventArgs e) {
            var hit = editor.HitTest(e.Location);
            if (!hit.HitAnyBar) {
                return;
            }

            if (e.Button == MouseButtons.Left) {
                var modifiers = ModifierKeys;
                if (hit.HitAnyNote) {
                    if (editor.HasSelectedBars) {
                        editor.ClearSelectedBars();
                    } else {
                        var note = hit.Note;
                        switch (modifiers) {
                            case Keys.Control:
                                note.Editor.IsSelected = !note.Editor.IsSelected;
                                break;
                            case Keys.Shift:
                            // TODO
                            //break;
                            case Keys.None:
                                if (editor.HasSelectedNotes) {
                                    editor.ClearSelectedNotesExcept(note);
                                }
                                note.Editor.IsSelected = !note.Editor.IsSelected;
                                break;
                        }
                    }
                    editor.Invalidate();
                } else if (hit.HitBarGridIntersection) {
                    // Add a note
                    var note = hit.Bar.AddNote();
                    note.Basic.IndexInGrid = hit.Row;
                    note.Basic.StartPosition = note.Basic.FinishPosition = hit.Column;
                    editor.Invalidate();
                } else {
                    if (editor.HasSelectedNotes) {
                        // Clear note selection first.
                        editor.ClearSelectedNotes();
                    } else {
                        // Select/unselect bar(s).
                        var bar = hit.Bar;
                        switch (modifiers) {
                            case Keys.Control:
                                bar.IsSelected = !bar.IsSelected;
                                break;
                            case Keys.Shift:
                            // TODO
                            //break;
                            case Keys.None:
                                if (editor.HasSelectedBars) {
                                    editor.ClearSelectedBarsExcept(bar);
                                }
                                bar.IsSelected = !bar.IsSelected;
                                break;
                        }
                    }
                    editor.Invalidate();
                }
            } else if (e.Button == MouseButtons.Right) {
                if (hit.HitAnyNote) {
                    ContextMenuRequested?.Invoke(this, new ContextMenuRequestedEventArgs(VisualizerContextMenu.Note, e.Location));
                } else {
                    ContextMenuRequested?.Invoke(this, new ContextMenuRequestedEventArgs(VisualizerContextMenu.Bar, e.Location));
                }
            }
        }

        private void VScroll_ValueChanged(object sender, EventArgs eventArgs) {
            editor.ScrollOffsetY = vScroll.Value;
        }

        private static readonly int ConstMargin = 3;

    }
}
