using System;
using JetBrains.Annotations;
using OpenCGSS.Director.Modules.SldProject.Core;

namespace OpenCGSS.Director.Modules.SldProject.Models {
    public sealed class ScoreEditorConfig : ICloneable, ICloneable<ScoreEditorConfig> {

        public float NoteRadius { get; set; } = 15;

        public float BarAreaWidth { get; set; } = 560;

        public float GridAreaWidth { get; set; } = 320;

        public float GridNumberAreaWidth { get; set; } = 35;

        public float InfoAreaWidth { get; set; } = 85;

        public float SpecialNotesAreaWidth { get; set; } = 150;

        ///// <summary>
        ///// Bottom Y of the first bar (origin: bottom left)
        ///// </summary>
        //public float FirstBarBottomY { get; set; } = 50;

        public int NumberOfColumns { get; set; } = 5;

        [NotNull]
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

        object ICloneable.Clone() {
            return Clone();
        }

    }
}
