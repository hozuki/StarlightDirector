using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using StarlightDirector.Core.Interop;
using StarlightDirector.UI.Controls;

namespace StarlightDirector.App.UI.Forms {
    partial class FMain {

        private void UnregisterEventHandlers() {
            SystemEvents.DisplaySettingsChanged -= SystemEventsOnDisplaySettingsChanged;
            Load -= OnLoad;
            sysMaximizeRestore.Click -= SysMaximizeRestoreOnClick;
            sysMinimize.Click -= SysMinimizeOnClick;
            lblCaption.MouseDoubleClick -= LblCaptionOnMouseDoubleClick;
            lblCaption.MouseMove -= LblCaptionOnMouseMove;
            picIcon.MouseDown -= PicIconOnMouseDown;
            btnDifficultySelection.Click -= BtnDifficultySelectionOnClick;
        }

        private void RegisterEventHandlers() {
            SystemEvents.DisplaySettingsChanged += SystemEventsOnDisplaySettingsChanged;
            Load += OnLoad;
            sysMaximizeRestore.Click += SysMaximizeRestoreOnClick;
            sysMinimize.Click += SysMinimizeOnClick;
            lblCaption.MouseDoubleClick += LblCaptionOnMouseDoubleClick;
            lblCaption.MouseMove += LblCaptionOnMouseMove;
            picIcon.MouseDown += PicIconOnMouseDown;
            btnDifficultySelection.Click += BtnDifficultySelectionOnClick;
        }

        private void BtnDifficultySelectionOnClick(object sender, EventArgs e) {
            var items = new ToolStripItem[mnuEditDifficulty.DropDownItems.Count];
            mnuEditDifficulty.DropDownItems.CopyTo(items, 0);
            ctxDifficultySelection.Items.AddRange(items);
            ctxDifficultySelection.Closed += CtxClosed;
            ctxDifficultySelection.Show(btnDifficultySelection, Point.Empty, ToolStripDropDownDirection.AboveRight);

            void CtxClosed(object s, EventArgs ev)
            {
                mnuEditDifficulty.DropDownItems.AddRange(items);
                ctxDifficultySelection.Closed -= CtxClosed;
            }
        }

        private void PicIconOnMouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (e.Clicks == 1) {
                    var pt = picIcon.Location;
                    pt.Y = picIcon.Bottom;
                    pt = picIcon.PointToScreen(pt);
                    var dispRect = NativeStructures.RECT.FromRectangle(ClientRectangle);
                    var hMenu = NativeMethods.GetSystemMenu(Handle, false);
                    NativeMethods.TrackPopupMenu(hMenu, NativeConstants.TPM_LEFTBUTTON, pt.X, pt.Y, 0, Handle, ref dispRect);
                } else {
                    Close();
                }
            } else if (e.Button == MouseButtons.Right) {
                var mouseScreen = MousePosition;
                var dispRect = NativeStructures.RECT.FromRectangle(ClientRectangle);
                NativeMethods.TrackPopupMenu(NativeMethods.GetSystemMenu(Handle, false), NativeConstants.TPM_LEFTBUTTON, mouseScreen.X, mouseScreen.Y, 0, Handle, ref dispRect);
            }
        }

        private void LblCaptionOnMouseMove(object sender, MouseEventArgs e) {
            var activeForm = ActiveForm;
            if (activeForm == this) {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, NativeConstants.WM_NCLBUTTONDOWN, (IntPtr)NativeConstants.HTCAPTION, IntPtr.Zero);
            }
        }

        private void LblCaptionOnMouseDoubleClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            }
        }

        private void SysMinimizeOnClick(object sender, EventArgs eventArgs) {
            WindowState = FormWindowState.Minimized;
        }

        private void SysMaximizeRestoreOnClick(object sender, EventArgs eventArgs) {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void OnLoad(object sender, EventArgs eventArgs) {
            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            using (var smallIcon = new Icon(Icon, picIcon.ClientSize)) {
                picIcon.InitialImage = picIcon.Image = smallIcon.ToBitmap();
                picIcon.ErrorImage = ToolStripRenderer.CreateDisabledImage(picIcon.InitialImage);
            }

            ApplyColorScheme(ColorScheme.Default);
            CursorFixup(this);
        }

        private void SystemEventsOnDisplaySettingsChanged(object sender, EventArgs eventArgs) {
            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

    }
}
