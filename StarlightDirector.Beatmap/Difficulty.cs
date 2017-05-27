using System.ComponentModel;

namespace StarlightDirector.Beatmap {
    public enum Difficulty {

        Invalid = 0,
        [LocalizationKey("misc.difficulty.debut")]
        Debut = 1,
        [LocalizationKey("misc.difficulty.regular")]
        Regular = 2,
        [LocalizationKey("misc.difficulty.pro")]
        Pro = 3,
        [LocalizationKey("misc.difficulty.master")]
        Master = 4,
        [LocalizationKey("misc.difficulty.master_plus")]
        [Description("Master+")]
        MasterPlus = 5

    }
}
