using System.Collections.Generic;
using System.Drawing;
using StarlightDirector.Beatmap;

namespace StarlightDirector.UI.Controls {
    partial class ScoreEditorSelectionHandler {

        private sealed class ElementSelection {

            public Bar LastHitBar { get; set; }
            public Note LastHitNote { get; set; }
            public List<Bar> HitBars { get; } = new List<Bar>();
            public List<Note> HitNotes { get; } = new List<Note>();
            public Rectangle SelectionRect { get; set; }

            public void Clear() {
                LastHitBar = null;
                LastHitNote = null;
                HitBars.Clear();
                HitNotes.Clear();
                SelectionRect = Rectangle.Empty;
            }

        }

    }
}
