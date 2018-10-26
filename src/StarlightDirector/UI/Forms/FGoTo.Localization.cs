using OpenCGSS.StarlightDirector.Localization;
using OpenCGSS.StarlightDirector.UI.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FGoTo : ILocalizable {

        public void Localize(ILanguageManager manager) {
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
