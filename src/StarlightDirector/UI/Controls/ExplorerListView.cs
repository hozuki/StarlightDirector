using System.Windows.Forms;

namespace OpenCGSS.StarlightDirector.UI.Controls {
    public sealed class ExplorerListView : ListView {

        protected override void CreateHandle() {
            base.CreateHandle();
            SystemStyleHelper.SetAdvancedTheme(this);
        }

    }
}
