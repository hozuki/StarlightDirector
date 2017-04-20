using StarlightDirector.Beatmap;
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
            visualizer.Editor.Invalidate();
        }

        private void CmdEditNoteDelete_Executed(object sender, ExecutedEventArgs e) {
            visualizer.Editor.RemoveSelectedNotes();
            visualizer.Editor.Invalidate();
        }

        private readonly Command CmdEditNoteStartPosition = CommandManager.CreateCommand();
        private readonly Command CmdEditNoteDelete = CommandManager.CreateCommand("Delete");

    }
}
