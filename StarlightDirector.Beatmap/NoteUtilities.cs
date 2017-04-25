namespace StarlightDirector.Beatmap {
    public static class NoteUtilities {

        public static (Note First, Note Second) Split(Note note1, Note note2) {
            var comparisonResult = Note.TimingThenPositionComparison(note1, note2);
            if (comparisonResult > 0) {
                return (note2, note1);
            } else if (comparisonResult < 0) {
                return (note1, note2);
            } else {
                return (note1, note2);
            }
        }

        public static void MakeHold(Note startNote, Note endNote) {
            startNote.Basic.Type = NoteType.Hold;
            startNote.Editor.HoldPair = endNote;
            endNote.Editor.HoldPair = startNote;
        }

    }
}
