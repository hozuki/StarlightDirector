using System.Diagnostics;
using System.Linq;
using StarlightDirector.Beatmap;
using StarlightDirector.Beatmap.Extensions;
using StarlightDirector.Commanding;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void CmdEditNoteStartPosition_Executed(object sender, ExecutedEventArgs e) {
            if (!visualizer.Editor.HasSelectedNotes) {
                return;
            }
            var startPosition = (NotePosition)e.Parameter;
            foreach (var note in visualizer.Editor.GetSelectedNotes()) {
                note.Basic.StartPosition = startPosition;
            }
            visualizer.RedrawScore();
        }

        private void CmdEditNoteDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedNotes();
            visualizer.RedrawScore();
        }

        //private void CmdEditNoteCreateRelationFlick_Executed(object sender, ExecutedEventArgs e) {
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


        //        previousNote = note;
        //    }

        //    Debug.Print("Flicked.");
        //}

        private readonly Command CmdEditNoteStartPosition = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteDelete = CommandManager.CreateCommand("Delete");
        //private readonly Command CmdEditNoteCreateRelationFlick = CommandManager.CreateCommand();

    }
}
