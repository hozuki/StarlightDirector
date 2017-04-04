using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using StarlightDirector.Core;

namespace StarlightDirector.UI.Controls {
    public partial class ScoreVisualizer : UserControl {

        public ScoreVisualizer() {
            InitializeComponent();
            RegisterEventHandlers();

            scoreRenderer1.ScrollBar = vScrollBar1;
        }

        ~ScoreVisualizer() {
            UnregisterEventHandlers();
        }

        [Browsable(false)]
        public ScoreRenderer Renderer => scoreRenderer1;

        [Browsable(false)]
        public VScrollBar ScrollBar => vScrollBar1;

        public void RedrawScore() {
            scoreRenderer1.Invalidate();
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

        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);

            var modifiers = ModifierKeys;
            if ((modifiers & Keys.Control) != 0) {
                if (e.Delta > 0) {
                    scoreRenderer1.ZoomIn();
                } else {
                    scoreRenderer1.ZoomOut();
                }
                return;
            }

            var newScrollValue = vScrollBar1.Value;
            if (e.Delta > 0) {
                // Up
                if ((modifiers & Keys.Shift) != 0) {
                    newScrollValue -= vScrollBar1.LargeChange;
                } else {
                    newScrollValue -= vScrollBar1.SmallChange;
                }
            } else if (e.Delta < 0) {
                // Down
                if ((modifiers & Keys.Shift) != 0) {
                    newScrollValue += vScrollBar1.LargeChange;
                } else {
                    newScrollValue += vScrollBar1.SmallChange;
                }
            }
            newScrollValue = newScrollValue.Clamp(vScrollBar1.Minimum, vScrollBar1.Maximum);
            vScrollBar1.Value = newScrollValue;
        }

        private void UnregisterEventHandlers() {
            vScrollBar1.ValueChanged -= VScrollBar1OnValueChanged;
        }

        private void RegisterEventHandlers() {
            vScrollBar1.ValueChanged += VScrollBar1OnValueChanged;
        }

        private void VScrollBar1OnValueChanged(object sender, EventArgs eventArgs) {
            scoreRenderer1.ScrollOffsetY = vScrollBar1.Value;
        }

        private static readonly int ConstMargin = 3;

    }
}
