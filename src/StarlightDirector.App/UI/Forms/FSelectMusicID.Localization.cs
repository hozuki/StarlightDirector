using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FSelectMusicID : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fselectmusicid.text");
            manager.ApplyText(btnOK, "ui.fselectmusicid.button.ok.text");
            manager.ApplyText(btnCancel, "ui.fselectmusicid.button.cancel.text");
        }

    }
}
