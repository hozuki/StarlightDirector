using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FMusicSettings : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fmusicsettings.text");
            manager.ApplyText(label1, "ui.fmusicsettings.label.file_location.text");
            manager.ApplyText(btnBrowseFile, "ui.fmusicsettings.button.browse_file.text");
            manager.ApplyText(btnClearFile, "ui.fmusicsettings.button.clear_file.text");
            manager.ApplyText(btnOK, "ui.fmusicsettings.button.ok.text");
            manager.ApplyText(btnCancel, "ui.fmusicsettings.button.cancel.text");
        }

    }
}
