using StarlightDirector.Core;

namespace StarlightDirector.UI.Controls.Editing {
    public sealed class ScoreEditorConfig : ICloneable<ScoreEditorConfig> {

        public float NoteRadius { get; set; } = 15;

        public float BarAreaWidth { get; set; } = 560;

        public float GridAreaWidth { get; set; } = 320;

        public float GridNumberAreaWidth { get; set; } = 35;

        public float InfoAreaWidth { get; set; } = 85;

        public float SpecialNotesAreaWidth { get; set; } = 120;

        public int NumberOfColumns { get; set; } = 5;

        public ScoreEditorConfig Clone() {
            return new ScoreEditorConfig {
                BarAreaWidth = BarAreaWidth,
                GridAreaWidth = GridAreaWidth,
                GridNumberAreaWidth = GridNumberAreaWidth,
                InfoAreaWidth = InfoAreaWidth,
                NoteRadius = NoteRadius,
                NumberOfColumns = NumberOfColumns,
                SpecialNotesAreaWidth = SpecialNotesAreaWidth
            };
        }

    }
}
