using OpenCGSS.StarlightDirector.Localization;
using OpenCGSS.StarlightDirector.UI.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FMusicSettings : ILocalizable {

        public void Localize(ILanguageManager manager) {
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
