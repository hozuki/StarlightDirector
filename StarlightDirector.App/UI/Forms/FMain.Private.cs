using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using StarlightDirector.App.Extensions;
using StarlightDirector.Beatmap;
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
            var region = this.DetermineNcRegion(e.Location, FrameBorderSize.Width, FrameBorderSize.Height, CaptionMargin);
            if (region != NativeConstants.HTCLIENT) {
                if (WindowState != FormWindowState.Maximized) {
                    MouseButtonAction action;
                    if (e.Button == MouseButtons.Left)
                        action = MouseButtonAction.LeftButtonDown;
                    else if (e.Button == MouseButtons.Right) {
                        action = MouseButtonAction.RightButtonDown;
                    } else {
                        return;
                    }
                    this.NcHitTest(e.Location, action, FrameBorderSize.Width, FrameBorderSize.Height, CaptionMargin);
                }
            }
            if (e.Button == MouseButtons.Right && e.Y <= lblCaption.Bottom) {
                var mousePosition = MousePosition;
                var displayRect = NativeStructures.RECT.FromRectangle(DisplayRectangle);
                var hMenu = NativeMethods.GetSystemMenu(Handle, false);
                NativeMethods.TrackPopupMenu(hMenu, NativeConstants.TPM_LEFTBUTTON, mousePosition.X, mousePosition.Y, 0, Handle, ref displayRect);
            }
            if (e.Clicks == 2) {
                MouseButtonAction action;
                if (e.Button == MouseButtons.Left)
                    action = MouseButtonAction.LeftButtonDoubleClick;
                else if (e.Button == MouseButtons.Right) {
                    action = MouseButtonAction.RightButtonDoubleClick;
                } else {
                    return;
                }
                this.NcHitTest(e.Location, action, FrameBorderSize.Width, FrameBorderSize.Height, CaptionMargin);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);
            var region = this.DetermineNcRegion(e.Location, FrameBorderSize.Width, FrameBorderSize.Height, CaptionMargin);
            if (region != NativeConstants.HTCLIENT) {
                if (WindowState != FormWindowState.Maximized) {
                    MouseButtonAction action;
                    if (e.Button == MouseButtons.Left)
                        action = MouseButtonAction.LeftButtonUp;
                    else if (e.Button == MouseButtons.Right) {
                        action = MouseButtonAction.RightButtonUp;
                    } else {
                        return;
                    }
                    this.NcHitTest(e.Location, action, FrameBorderSize.Width, FrameBorderSize.Height, CaptionMargin);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (WindowState != FormWindowState.Maximized) {
                this.NcHitTest(e.Location, MouseButtonAction.MouseMove, FrameBorderSize.Width, FrameBorderSize.Height, CaptionMargin);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Escape:
                    CmdEditSelectClearAll.Execute(this, null);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            switch (e.KeyData) {
                case Keys.D0:
                case Keys.NumPad0:
                    CmdEditNoteStartPositionAt0.Execute(this, NotePosition.Default);
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    CmdEditNoteStartPositionAt1.Execute(this, NotePosition.Left);
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    CmdEditNoteStartPositionAt2.Execute(this, NotePosition.CenterLeft);
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    CmdEditNoteStartPositionAt3.Execute(this, NotePosition.Center);
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    CmdEditNoteStartPositionAt4.Execute(this, NotePosition.CenterRight);
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    CmdEditNoteStartPositionAt5.Execute(this, NotePosition.Right);
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            switch (e.KeyData) {
                case Keys.D0:
                case Keys.NumPad0:
                case Keys.D1:
                case Keys.NumPad1:
                case Keys.D2:
                case Keys.NumPad2:
                case Keys.D3:
                case Keys.NumPad3:
                case Keys.D4:
                case Keys.NumPad4:
                case Keys.D5:
                case Keys.NumPad5:
                    CmdEditNoteStartPositionAt0.Execute(this, NotePosition.Default);
                    break;
                default:
                    base.OnKeyUp(e);
                    break;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
        }

        protected override void OnPaint(PaintEventArgs e) {
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
            const int statusBarHeight = 22;
            var statusTextHorizontalMargin = 5;
            var statusRect = new Rectangle(clientRectangle.Left, clientRectangle.Bottom - statusBarHeight, clientRectangle.Width - statusTextHorizontalMargin, statusBarHeight);
            var windowState = WindowState;
            if (windowState == FormWindowState.Maximized) {
                statusRect.Y -= FrameBorderSize.Height + 3;
                statusTextHorizontalMargin += FrameBorderSize.Width;
            }
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
                        statusRect.X += statusTextHorizontalMargin;
                        statusRect.Width -= gripSize + statusTextHorizontalMargin;
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
            var newWindowState = WindowState;
            sysMaximizeRestore.Icon = newWindowState == FormWindowState.Maximized ? ModernSystemButtonIcon.Restore : ModernSystemButtonIcon.Maximize;
        }

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case NativeConstants.WM_NCCALCSIZE:
                    // Set to 0 to hide the frames while not deleting system menu.
                    // This is a trick that I found when I was in high school, but over the years I forgot it. :(
                    m.Result = IntPtr.Zero;
                    break;
                case NativeConstants.WM_COMMAND:
                    // Yep, so we must handle the command messages ourselves.
                    var commandID = m.WParam.ToInt32();
                    switch (commandID) {
                        case NativeConstants.SC_CLOSE:
                            Close();
                            break;
                        case NativeConstants.SC_MAXIMIZE:
                            WindowState = FormWindowState.Maximized;
                            break;
                        case NativeConstants.SC_MINIMIZE:
                            WindowState = FormWindowState.Minimized;
                            break;
                        case NativeConstants.SC_RESTORE:
                            WindowState = FormWindowState.Normal;
                            if (Left < 0) {
                                Left = 0;
                            }
                            if (Top < 0) {
                                Top = 0;
                            }
                            break;
                        default:
                            base.WndProc(ref m);
                            break;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
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
