﻿using StarlightDirector.App.Extensions;
using StarlightDirector.Core;

namespace StarlightDirector.App.UI.Forms {
    partial class FNewMeasures : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fnewmeasures.text");
            manager.ApplyText(label1, "ui.fnewmeasures.label.number_of_measures.text");
            manager.ApplyText(btnOK, "ui.fnewmeasures.button.ok.text");
            manager.ApplyText(btnCancel, "ui.fnewmeasures.button.cancel.text");
        }

    }
}
