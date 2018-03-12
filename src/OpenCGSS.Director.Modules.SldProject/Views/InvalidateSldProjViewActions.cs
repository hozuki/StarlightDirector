using System;

namespace OpenCGSS.Director.Modules.SldProject.Views {
    [Flags]
    public enum InvalidateSldProjViewActions {

        None = 0,

        Redraw = 0x1,
        UpdateVerticalLength = 0x2,

        All = Redraw | UpdateVerticalLength

    }
}
