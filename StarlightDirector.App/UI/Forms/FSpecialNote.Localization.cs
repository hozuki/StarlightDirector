using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FSpecialNote : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "fspecialnote.text");
            manager.ApplyText(label2, "fspecialnote.label.in_measure.text");
            manager.ApplyText(label3, "fspecialnote.label.on_row.text");
            manager.ApplyText(label1, "fspecialnote.label.new_bpm.text");
            manager.ApplyText(radioButton1, "fspecialnote.radio.variant_bpm.text");
            manager.ApplyText(btnOK, "fspecialnote.button.ok.text");
            manager.ApplyText(btnCancel, "fspecialnote.button.cancel.text");
        }

    }
}
