using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FGoTo : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fgoto.text");
            manager.ApplyText(radGoToMeasure, "ui.fgoto.radio.measure.text");
            manager.ApplyText(radGoToTime, "ui.fgoto.radio.time.text");
            manager.ApplyText(btnOK, "ui.fgoto.button.ok.text");
            manager.ApplyText(btnCancel, "ui.fgoto.button.cancel.text");
        }

    }
}
