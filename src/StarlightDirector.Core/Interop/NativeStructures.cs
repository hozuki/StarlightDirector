using System.Drawing;
using System.Runtime.InteropServices;

namespace StarlightDirector.Core.Interop {
    public static class NativeStructures {

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct RECT {

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
