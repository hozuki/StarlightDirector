using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.UI.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FSpecialNote : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fspecialnote.text");
            manager.ApplyText(label2, "ui.fspecialnote.label.in_measure.text");
            manager.ApplyText(label3, "ui.fspecialnote.label.on_row.text");
            manager.ApplyText(label1, "ui.fspecialnote.label.new_bpm.text");
            manager.ApplyText(radioButton1, "ui.fspecialnote.radio.variant_bpm.text");
            manager.ApplyText(btnOK, "ui.fspecialnote.button.ok.text");
            manager.ApplyText(btnCancel, "ui.fspecialnote.button.cancel.text");
        }

    }
}
