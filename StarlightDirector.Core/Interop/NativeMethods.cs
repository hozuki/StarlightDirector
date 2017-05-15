using System;
using System.Runtime.InteropServices;

namespace StarlightDirector.Core.Interop {
    public static class NativeMethods {

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern bool QueryPerformanceFrequency(out long lpFrequency);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern bool TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr prcRect);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern bool TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, ref NativeStructures.RECT prcRect);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall, PreserveSig = true, SetLastError = true)]
        public static extern int SetWindowTheme(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pszAppName, [MarshalAs(UnmanagedType.LPWStr)] string pszSubIdList);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetTokenInformation(IntPtr tokenHandle, NativeConstants.TokenInformationClass tokenInformationClass, IntPtr tokenInformation, int tokenInformationLength, out int returnLength);


    }
}
