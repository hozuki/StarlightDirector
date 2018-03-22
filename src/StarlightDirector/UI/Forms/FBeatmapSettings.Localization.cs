using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.UI.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FBeatmapSettings : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fbeatmapsettings.text");
            manager.ApplyText(label1, "ui.fbeatmapsettings.label.bpm.text");
            manager.ApplyText(label2, "ui.fbeatmapsettings.label.score_offset.text");
            manager.ApplyText(btnOneMeasureMore, "ui.fbeatmapsettings.button.one_measure_more.text");
            manager.ApplyText(btnOneMeasureLess, "ui.fbeatmapsettings.button.one_measure_less.text");
            manager.ApplyText(btnOK, "ui.fbeatmapsettings.button.ok.text");
            manager.ApplyText(btnCancel, "ui.fbeatmapsettings.button.cancel.text");
        }

    }
}
