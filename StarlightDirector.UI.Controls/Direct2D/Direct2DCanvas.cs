using System.Windows.Forms;

namespace StarlightDirector.UI.Controls.Direct2D {
    public abstract class Direct2DCanvas : Direct2DControl {
        
        protected sealed override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Render();
        }

        protected sealed override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }

    }
}
