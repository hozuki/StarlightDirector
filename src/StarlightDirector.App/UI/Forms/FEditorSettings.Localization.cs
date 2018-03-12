using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FEditorSettings : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.feditorsettings.text");
            manager.ApplyText(tabPage1, "ui.feditorsettings.tab.editor.text");
            manager.ApplyText(tabPage2, "ui.feditorsettings.tab.preview.text");
            manager.ApplyText(tabPage3, "ui.feditorsettings.tab.misc.text");
            manager.ApplyText(label1, "ui.feditorsettings.label.scrolling_style.text");
            manager.ApplyText(label2, "ui.feditorsettings.label.scrolling_speed.text");
            manager.ApplyText(label3, "ui.feditorsettings.label.language.text");
            manager.ApplyText(label4, "ui.feditorsettings.label.preview_render_mode.text");
            manager.ApplyText(label5, "ui.feditorsettings.label.preview_speed.text");
            manager.ApplyText(radInvertedScrollingOff, "ui.feditorsettings.radio.scrolling_style.normal.text");
            manager.ApplyText(radInvertedScrollingOn, "ui.feditorsettings.radio.scrolling_style.inverted.text");
            manager.ApplyText(chkShowNoteIndicators, "ui.feditorsettings.check.show_note_indicators.text");
            manager.ApplyText(btnOK, "ui.feditorsettings.button.ok.text");
            manager.ApplyText(btnCancel, "ui.feditorsettings.button.cancel.text");
        }

    }
}
