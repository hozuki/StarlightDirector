using System.ComponentModel;

namespace StarlightDirector.Beatmap {
    public enum NotePosition {

        [Description("Default")]
        Default = 0,
        [Description("P1")]
        P1 = 1,
        [Description("P1")]
        P2 = 2,
        [Description("P3")]
        P3 = 3,
        [Description("P4")]
        P4 = 4,
        [Description("P5")]
        P5 = 5,

        // Definition order matters.
        Min = 1,
        Max = 5

    }
}
