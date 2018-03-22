using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.UI.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FBeatmapStats : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }
            manager.ApplyText(this, "ui.fbeatmapstats.text");
        }

    }
}
