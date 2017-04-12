using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using StarlightDirector.Core;
using StarlightDirector.Core.Interop;

namespace StarlightDirector.UI.Controls {
    public static class ControlUtils {

        public static bool PointInRect(Point point, Rectangle rectangle) {
            return point.X >= rectangle.X && point.Y >= rectangle.Y && point.X < rectangle.Right && point.Y < rectangle.Bottom;
        }

        public static bool PointInRect(int x, int y, Rectangle rectangle) {
            return x >= rectangle.X && y >= rectangle.Y && x < rectangle.Right && y < rectangle.Bottom;
        }

        public static int DetermineNcRegion(this Form form, Point mouseLocation, int horizontalMargin, int verticalMargin, int captionMargin) {
            var size = form.Size;
            if (mouseLocation.X <= horizontalMargin) {
                if (mouseLocation.Y <= verticalMargin) {
                    return NativeConstants.HTTOPLEFT;
                } else if (mouseLocation.Y > size.Height - verticalMargin) {
                    return NativeConstants.HTBOTTOMLEFT;
                } else {
                    return NativeConstants.HTLEFT;
                }
            } else if (mouseLocation.X >= size.Width - horizontalMargin) {
                if (mouseLocation.Y <= verticalMargin) {
                    return NativeConstants.HTTOPRIGHT;
                } else if (mouseLocation.Y > size.Height - verticalMargin) {
                    return NativeConstants.HTBOTTOMRIGHT;
                } else {
                    return NativeConstants.HTRIGHT;
                }
            } else {
                if (mouseLocation.Y <= verticalMargin) {
                    return NativeConstants.HTTOP;
                } else if (mouseLocation.Y <= captionMargin) {
                    return NativeConstants.HTCAPTION;
                } else if (mouseLocation.Y > size.Height - verticalMargin) {
                    return NativeConstants.HTBOTTOM;
                } else {
                    return NativeConstants.HTCLIENT;
                }
            }
        }

        public static IntPtr NcHitTest(this Form form, Point mouseLocation, MouseButtonAction action, int horizontalMargin, int verticalMargin, int captionMargin) {
            if (action == MouseButtonAction.None) {
                return IntPtr.Zero;
            }

            try {
                NativeMethods.ReleaseCapture();

                int ncAction;
                switch (action) {
                    case MouseButtonAction.LeftButtonDown:
                        ncAction = NativeConstants.WM_NCLBUTTONDOWN;
                        break;
                    case MouseButtonAction.LeftButtonUp:
                        ncAction = NativeConstants.WM_NCLBUTTONUP;
                        break;
                    case MouseButtonAction.LeftButtonDoubleClick:
                        ncAction = NativeConstants.WM_NCLBUTTONDBLCLK;
                        break;
                    case MouseButtonAction.RightButtonDown:
                        ncAction = NativeConstants.WM_NCRBUTTONDOWN;
                        break;
                    case MouseButtonAction.RightButtonUp:
                        ncAction = NativeConstants.WM_NCRBUTTONUP;
                        break;
                    case MouseButtonAction.RightButtonDoubleClick:
                        ncAction = NativeConstants.WM_NCRBUTTONDBLCLK;
                        break;
                    case MouseButtonAction.MouseMove:
                        ncAction = NativeConstants.WM_NCMOUSEMOVE;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(action), action, null);
                }

                int ncLocation;
                Cursor formCursor;
                var size = form.Size;
                var isMaximized = form.WindowState == FormWindowState.Maximized;
                if (mouseLocation.X <= horizontalMargin) {
                    if (!isMaximized) {
                        if (mouseLocation.Y <= verticalMargin) {
                            formCursor = Cursors.SizeNWSE;
                            ncLocation = NativeConstants.HTTOPLEFT;
                        } else if (mouseLocation.Y >= size.Height - verticalMargin) {
                            formCursor = Cursors.SizeNESW;
                            ncLocation = NativeConstants.HTBOTTOMLEFT;
                        } else {
                            formCursor = Cursors.SizeWE;
                            ncLocation = NativeConstants.HTLEFT;
                        }
                    } else {
                        formCursor = Cursors.Default;
                        ncLocation = NativeConstants.HTCLIENT;
                    }
                } else if (mouseLocation.X >= size.Width - horizontalMargin) {
                    if (!isMaximized) {
                        if (mouseLocation.Y <= verticalMargin) {
                            formCursor = Cursors.SizeNESW;
                            ncLocation = NativeConstants.HTTOPRIGHT;
                        } else if (mouseLocation.Y >= size.Height - verticalMargin) {
                            formCursor = Cursors.SizeNWSE;
                            ncLocation = NativeConstants.HTBOTTOMRIGHT;
                        } else {
                            formCursor = Cursors.SizeWE;
                            ncLocation = NativeConstants.HTRIGHT;
                        }
                    } else {
                        formCursor = Cursors.Default;
                        ncLocation = NativeConstants.HTCLIENT;
                    }
                } else {
                    if (mouseLocation.Y <= verticalMargin) {
                        if (!isMaximized) {
                            formCursor = Cursors.SizeNS;
                            ncLocation = NativeConstants.HTTOP;
                        } else {
                            formCursor = Cursors.Default;
                            ncLocation = NativeConstants.HTCLIENT;
                        }
                    } else if (mouseLocation.Y <= captionMargin) {
                        formCursor = Cursors.Default;
                        ncLocation = NativeConstants.HTCAPTION;
                        if (action == MouseButtonAction.LeftButtonDoubleClick) {
                            form.Cursor = formCursor;
                            form.WindowState = form.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
                            return IntPtr.Zero;
                        }
                    } else if (mouseLocation.Y >= size.Height - verticalMargin) {
                        if (!isMaximized) {
                            formCursor = Cursors.SizeNS;
                            ncLocation = NativeConstants.HTBOTTOM;
                        } else {
                            formCursor = Cursors.Default;
                            ncLocation = NativeConstants.HTCLIENT;
                        }
                    } else {
                        formCursor = Cursors.Default;
                        ncLocation = NativeConstants.HTCLIENT;
                    }
                }

                form.Cursor = formCursor;
                return NativeMethods.SendMessage(form.Handle, ncAction, (IntPtr)ncLocation, IntPtr.Zero);
            } catch (Win32Exception ex) {
                Debug.Print(ex.Message);
            }
            return IntPtr.Zero;
        }

    }
}
