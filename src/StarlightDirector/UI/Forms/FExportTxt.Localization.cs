using OpenCGSS.StarlightDirector.Localization;
using OpenCGSS.StarlightDirector.UI.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FExportTxt : ILocalizable {

        public void Localize(ILanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fexporttxt.text");
            manager.ApplyText(lblTitle, "ui.fexporttxt.label.title.text");
            manager.ApplyText(lblComposer, "ui.fexporttxt.label.composer.text");
            manager.ApplyText(lblLyricist, "ui.fexporttxt.label.lyricist.text");
            manager.ApplyText(lblBgFile, "ui.fexporttxt.label.bg_file.text");
            manager.ApplyText(lblSongFile, "ui.fexporttxt.label.song_file.text");
            manager.ApplyText(lblDifficulty, "ui.fexporttxt.label.difficulty.text");
            manager.ApplyText(lblLevel, "ui.fexporttxt.label.level.text");
            manager.ApplyText(lblColor, "ui.fexporttxt.label.color.text");
            manager.ApplyText(lblBgmVolume, "ui.fexporttxt.label.bgm_volume.text");
            manager.ApplyText(lblSeVolume, "ui.fexporttxt.label.se_volume.text");
            manager.ApplyText(lblFormat, "ui.fexporttxt.label.format.text");
            manager.ApplyText(btnExport, "ui.fexporttxt.button.export.text");
            manager.ApplyText(btnCancel, "ui.fexporttxt.button.cancel.text");
        }

    }
}
