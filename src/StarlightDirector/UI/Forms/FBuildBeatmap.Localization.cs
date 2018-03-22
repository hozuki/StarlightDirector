using OpenCGSS.StarlightDirector.Globalization;
using OpenCGSS.StarlightDirector.UI.Extensions;

namespace OpenCGSS.StarlightDirector.UI.Forms {
    partial class FBuildBeatmap : ILocalizable {

        public void Localize(LanguageManager manager) {
            if (manager == null) {
                return;
            }

            manager.ApplyText(this, "ui.fbuildbeatmap.text");
            manager.ApplyText(label1, "ui.fbuildbeatmap.label.end_time.text");
            manager.ApplyText(label2, "ui.fbuildbeatmap.label.end_time.estimated.text");
            manager.ApplyText(label11, "ui.fbuildbeatmap.label.build_type.text");
            manager.ApplyText(label5, "ui.fbuildbeatmap.label.csv.difficulty.text");
            manager.ApplyText(label3, "ui.fbuildbeatmap.label.bdb.live_id.text");
            manager.ApplyText(label4, "ui.fbuildbeatmap.label.bdb.difficulty_mapping.text");
            manager.ApplyText(label6, "ui.fbuildbeatmap.label.bdb.difficulty_mapping.debut.text");
            manager.ApplyText(label7, "ui.fbuildbeatmap.label.bdb.difficulty_mapping.regular.text");
            manager.ApplyText(label8, "ui.fbuildbeatmap.label.bdb.difficulty_mapping.pro.text");
            manager.ApplyText(label9, "ui.fbuildbeatmap.label.bdb.difficulty_mapping.master.text");
            manager.ApplyText(label10, "ui.fbuildbeatmap.label.bdb.difficulty_mapping.master_plus.text");
            manager.ApplyText(radEndTimeAuto, "ui.fbuildbeatmap.radio.end_time.auto.text");
            manager.ApplyText(radEndTimeCustom, "ui.fbuildbeatmap.radio.end_time.custom.text");
            manager.ApplyText(radBuildCsv, "ui.fbuildbeatmap.radio.build_type.csv.text");
            manager.ApplyText(radBuildBdb, "ui.fbuildbeatmap.radio.build_type.bdb.text");
            manager.ApplyText(btnSelectLiveID, "ui.fbuildbeatmap.button.select_live_id.text");
            manager.ApplyText(btnBuild, "ui.fbuildbeatmap.button.build.text");
            manager.ApplyText(btnClose, "ui.fbuildbeatmap.button.close.text");
        }

    }
}
