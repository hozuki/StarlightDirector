using System.Drawing;
using System.Runtime.InteropServices;

namespace OpenCGSS.StarlightDirector.Interop {
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RECT {

        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public Rectangle ToRectangle() {
            return Rectangle.FromLTRB(Left, Top, Right, Bottom);
        }

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
