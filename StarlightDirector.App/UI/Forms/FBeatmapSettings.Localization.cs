using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FBeatmapSettings : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "fbeatmapsettings.text");
            manager.ApplyText(label1, "fbeatmapsettings.label.bpm.text");
            manager.ApplyText(label2, "fbeatmapsettings.label.score_offset.text");
            manager.ApplyText(btnOK, "fbeatmapsettings.button.ok.text");
            manager.ApplyText(btnCancel, "fbeatmapsettings.button.cancel.text");
        }

    }
}
