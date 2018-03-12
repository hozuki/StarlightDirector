using System.ComponentModel;

namespace StarlightDirector.Beatmap {
    public enum NotePosition {

        [LocalizationKey("misc.note_position.default")]
        [Description("Default")]
        Default = 0,
        [LocalizationKey("misc.note_position.p1")]
        [Description("P1")]
        P1 = 1,
        [LocalizationKey("misc.note_position.p2")]
        [Description("P2")]
        P2 = 2,
        [LocalizationKey("misc.note_position.p3")]
        [Description("P3")]
        P3 = 3,
        [LocalizationKey("misc.note_position.p4")]
        [Description("P4")]
        P4 = 4,
        [LocalizationKey("misc.note_position.p5")]
        [Description("P5")]
        P5 = 5,

        // Definition order matters.
        Min = 1,
        Max = 5

    }
}
