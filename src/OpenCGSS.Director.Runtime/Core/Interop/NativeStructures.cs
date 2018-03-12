using System.Drawing;
using System.Runtime.InteropServices;

namespace OpenCGSS.Director.Core.Interop {
    internal static class NativeStructures {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct RECT {

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public static RECT FromRectangle(Rectangle rectangle) {
                return new RECT {
                    Left = rectangle.Left,
                    Top = rectangle.Top,
                    Bottom = rectangle.Bottom,
                    Right = rectangle.Right
                };
            }

        }


    }
}
