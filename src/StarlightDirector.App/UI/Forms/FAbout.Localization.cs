using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FAbout : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fabout.text");
            manager.ApplyText(btnClose, "ui.fabout.button.close.text");
        }

    }
}
