using System.ComponentModel;

namespace OpenCGSS.Director.Modules.SldProject.Models.Beatmap {
    public enum Difficulty {

        Invalid = 0,
        [Description("Debut")]
        Debut = 1,
        [Description("Regular")]
        Regular = 2,
        [Description("Pro")]
        Pro = 3,
        [Description("Master")]
        Master = 4,
        [Description("Master+")]
        MasterPlus = 5

    }
}
