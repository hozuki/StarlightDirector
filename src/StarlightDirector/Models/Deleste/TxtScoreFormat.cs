using System.ComponentModel;
using OpenCGSS.StarlightDirector.Globalization;

namespace OpenCGSS.StarlightDirector.Models.Deleste {
    public enum TxtScoreFormat {

        [LocalizationKey("misc.deleste.score_format.default")]
        [Description("Default")]
        Default = 0,
        [LocalizationKey("misc.deleste.score_format.converted")]
        [Description("Converted (CSV-like)")]
        Converted = 1

    }
}
