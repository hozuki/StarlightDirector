using System;
using System.Drawing;
using System.Windows.Forms;

namespace StarlightDirector.UI.Controls {
    public partial class ScoreVisualizer : UserControl {

        public ScoreVisualizer() {
            InitializeComponent();
        }

        protected override void OnLayout(LayoutEventArgs e) {
            base.OnLayout(e);

            var clientSize = ClientSize;
            var scrollBarLeft = clientSize.Width - vScrollBar1.Width - ConstMargin * 2;
            var scrollBarTop = ConstMargin;
            var scrollBarHeight = clientSize.Height - ConstMargin * 2;
            vScrollBar1.Location = new Point(scrollBarLeft, scrollBarTop);
            vScrollBar1.Height = scrollBarHeight;

            var rendererLeft = ConstMargin;
            var rendererTop = ConstMargin;
            var rendererWidth = scrollBarLeft - ConstMargin;
            var rendererHeight = clientSize.Height - ConstMargin * 2;
            scoreRenderer1.Location = new Point(rendererLeft, rendererTop);
            scoreRenderer1.Size = new Size(rendererWidth, rendererHeight);
        }

        private static readonly int ConstMargin = 3;

    }
}
