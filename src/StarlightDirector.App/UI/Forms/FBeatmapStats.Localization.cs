using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FBeatmapStats : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }
            manager.ApplyText(this, "ui.fbeatmapstats.text");
        }

    }
}
