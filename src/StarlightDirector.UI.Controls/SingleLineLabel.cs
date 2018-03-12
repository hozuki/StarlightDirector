using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using StarlightDirector.Core.Interop;

namespace StarlightDirector.UI.Controls {
    public sealed class SingleLineLabel : Control {

        public SingleLineLabel() {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Opaque | ControlStyles.ContainerControl | ControlStyles.Selectable, false);
        }

        [Browsable(true)]
        [Description("Gets/sets whether this label is click-through.")]
        [DefaultValue(false)]
        public bool ClickThrough { get; set; } = false;

        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            Invalidate();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if (disposing) {
                _stringFormat.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            var measureResult = e.Graphics.MeasureString(Text, Font);
            var clientRectangle = ClientRectangle;
            var y = (clientRectangle.Height - measureResult.Height) / 2;
            var rect = new RectangleF(0, y, 0, measureResult.Height);
            rect.Width = measureResult.Width > clientRectangle.Width ? clientRectangle.Width : measureResult.Width;
            rect.X = (clientRectangle.Width - rect.Width) / 2;
            using (var brush = new SolidBrush(ForeColor)) {
                e.Graphics.DrawString(Text, Font, brush, rect, _stringFormat);
            }
        }

        protected override void WndProc(ref Message m) {
            if (ClickThrough && m.Msg == NativeConstants.WM_NCHITTEST) {
                m.Result = (IntPtr)NativeConstants.HTTRANSPARENT;
            } else {
                base.WndProc(ref m);
            }
        }

        private readonly StringFormat _stringFormat = new StringFormat {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            HotkeyPrefix = HotkeyPrefix.None,
            Trimming = StringTrimming.EllipsisCharacter,
        };

    }
}
