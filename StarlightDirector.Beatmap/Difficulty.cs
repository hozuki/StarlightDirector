using System.ComponentModel;

namespace StarlightDirector.Beatmap {
    public enum Difficulty {

        Invalid = 0,
        Debut = 1,
        Regular = 2,
        Pro = 3,
        Master = 4,
        [Description("Master+")]
        MasterPlus = 5

    }
}
