using System.ComponentModel;

namespace StarlightDirector.Beatmap {
    public enum NotePosition {

        [Description("Default")]
        Nowhere = 0,
        [Description("Left")]
        Left = 1,
        [Description("Center Left")]
        CenterLeft = 2,
        [Description("Center")]
        Center = 3,
        [Description("Center Right")]
        CenterRight = 4,
        [Description("Right")]
        Right = 5,
        Max = 5

    }
}
