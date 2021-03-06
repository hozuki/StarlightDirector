using System;
using System.Runtime.InteropServices;

namespace OpenCGSS.StarlightDirector.Interop {
    internal static class NativeMethods {

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern bool TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr prcRect);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, ref RECT prcRect);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern int SetWindowTheme(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pszAppName, [MarshalAs(UnmanagedType.LPWStr)] string pszSubIdList);

        [DllImport("user32.dll", EntryPoint = "EnumDisplaySettingsA", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, PreserveSig = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDisplaySettings([MarshalAs(UnmanagedType.LPStr)] string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

    }
}
