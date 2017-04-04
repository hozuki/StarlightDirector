using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using StarlightDirector.Core.Interop;
using StarlightDirector.UI.Controls;
using StarlightDirector.UI.Controls.Extensions;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        protected override void OnTextChanged(EventArgs e) {
            base.OnTextChanged(e);
            lblCaption.Text = Text;
        }

        protected override void OnActivated(EventArgs e) {
            base.OnActivated(e);
            lblCaption.ForeColor = ColorScheme.Current.ActiveWindowTitle;
            picIcon.Image = picIcon.InitialImage;
        }

        protected override void OnDeactivate(EventArgs e) {
            base.OnDeactivate(e);
            lblCaption.ForeColor = ColorScheme.Current.InactiveWindowTitle;
            picIcon.Image = picIcon.ErrorImage;
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (WindowState != FormWindowState.Maximized) {
                if (e.Button == MouseButtons.Left) {
                    const int horizontalMargin = 5;
                    const int verticalMargin = 5;
                    this.NcHitTest(e.Location, MouseButtonAction.LeftButtonDown, horizontalMargin, verticalMargin, CaptionMargin);
                }
            }
            if (e.Button == MouseButtons.Right) {
                if (e.Y <= lblCaption.Bottom) {
                    var mousePosition = MousePosition;
                    var displayRect = NativeStructures.RECT.FromRectangle(DisplayRectangle);
                    NativeMethods.TrackPopupMenu(NativeMethods.GetSystemMenu(Handle, false), NativeConstants.TPM_LEFTBUTTON, mousePosition.X, mousePosition.Y, 0, Handle, ref displayRect);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            if (WindowState != FormWindowState.Maximized) {
                if (e.Button == MouseButtons.Left) {
                    const int horizontalMargin = 5;
                    const int verticalMargin = 5;
                    this.NcHitTest(e.Location, MouseButtonAction.LeftButtonUp, horizontalMargin, verticalMargin, CaptionMargin);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (WindowState != FormWindowState.Maximized) {
                const int horizontalMargin = 5;
                const int verticalMargin = 5;
                this.NcHitTest(e.Location, MouseButtonAction.MouseMove, horizontalMargin, verticalMargin, CaptionMargin);
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            var g = e.Graphics;
            g.Clear(BackColor);

            var colorScheme = ColorScheme.Current;
            var clientRectangle = ClientRectangle;

            // Right panel (toolbars).
            var toolbarPanelRect = new Rectangle(clientRectangle.Width - ToolbarMargin, 0, ToolbarMargin, clientRectangle.Height);
            g.FillRectangle(colorScheme.ToolbarBackground, toolbarPanelRect);

            // Caption.
            var captionRect = new Rectangle(0, 0, clientRectangle.Width, CaptionMargin);
            g.FillRectangle(colorScheme.CaptionBackground, captionRect);

            // Status text and status text area.
            const int gripSize = 16;
            const int statusBarHeight = 24;
            const int statusTextLeftMargin = 5;
            var statusRect = new Rectangle(clientRectangle.Left, clientRectangle.Bottom - statusBarHeight, clientRectangle.Width - (clientRectangle.Width - btnDifficultySelection.Left), statusBarHeight);
            g.FillRectangle(colorScheme.WindowNormalStatusBackground, statusRect);
            g.DrawLine(colorScheme.WindowStatusSeparator, clientRectangle.Left, statusRect.Top, clientRectangle.Right, statusRect.Top);
            if (!string.IsNullOrEmpty(StatusText)) {
                using (var tf = new StringFormat()) {
                    tf.FormatFlags = StringFormatFlags.NoWrap;
                    tf.Trimming = StringTrimming.EllipsisCharacter;
                    tf.HotkeyPrefix = HotkeyPrefix.Show;
                    tf.Alignment = StringAlignment.Near;
                    tf.LineAlignment = StringAlignment.Center;
                    using (var tb = new SolidBrush(colorScheme.WindowNormalStatusText)) {
                        statusRect.X += statusTextLeftMargin;
                        statusRect.Width -= gripSize + statusTextLeftMargin;
                        g.DrawString(StatusText, Font, tb, statusRect, tf);
                    }
                }
            }

            // Grip.
            if (WindowState == FormWindowState.Normal) {
                using (var p = new Pen(colorScheme.WindowNormalStatusText, 1)) {
                    for (var i = 2; i < gripSize; i += 2) {
                        var x1 = clientRectangle.Width - i;
                        var y1 = clientRectangle.Height;
                        var x2 = clientRectangle.Width;
                        var y2 = clientRectangle.Height - i;
                        g.DrawLine(p, x1, y1, x2, y2);
                    }
                }
            }

            // Window border.
            using (var p = new Pen(colorScheme.WindowBorder, 1)) {
                g.DrawRectangle(p, clientRectangle.X, clientRectangle.Y, clientRectangle.Width - p.Width / 2, clientRectangle.Height - p.Width);
            }
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            sysMaximizeRestore.Icon = WindowState == FormWindowState.Maximized ? ModernSystemButtonIcon.Restore : ModernSystemButtonIcon.Maximize;
        }

        private static void CursorFixup(Control control) {
            if (!control.HasChildren) {
                return;
            }
            foreach (var c in control.Controls) {
                if (c is Control ctl) {
                    ctl.Cursor = Cursors.Default;
                }
            }
        }

    }
}
