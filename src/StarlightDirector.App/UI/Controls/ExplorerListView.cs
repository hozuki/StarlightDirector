using System.Windows.Forms;

namespace StarlightDirector.App.UI.Controls {
    public sealed class ExplorerListView : ListView {

        protected override void CreateHandle() {
            base.CreateHandle();
            UIUtilities.SetAdvancedTheme(this);
        }

    }
}
