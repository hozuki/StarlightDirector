using System.Runtime.InteropServices;

namespace OpenCGSS.StarlightDirector.Interop {
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    internal unsafe struct DEVMODE {

        public const int CCHDEVICENAME = 32;
        public const int CCHFORMNAME = 32;
        
        [FieldOffset(0)]
        public fixed char dmDeviceName[CCHDEVICENAME];
        [FieldOffset(32)]
        public short dmSpecVersion;
        [FieldOffset(34)]
        public short dmDriverVersion;
        [FieldOffset(36)]
        public short dmSize;
        [FieldOffset(38)]
        public short dmDriverExtra;
        [FieldOffset(40)]
        public int dmFields;

        [FieldOffset(44)]
        short dmOrientation;
        [FieldOffset(46)]
        short dmPaperSize;
        [FieldOffset(48)]
        short dmPaperLength;
        [FieldOffset(50)]
        short dmPaperWidth;
        [FieldOffset(52)]
        short dmScale;
        [FieldOffset(54)]
        short dmCopies;
        [FieldOffset(56)]
        short dmDefaultSource;
        [FieldOffset(58)]
        short dmPrintQuality;

        [FieldOffset(44)]
        public POINTL dmPosition;
        [FieldOffset(52)]
        public int dmDisplayOrientation;
        [FieldOffset(56)]
        public int dmDisplayFixedOutput;

        [FieldOffset(60)]
        public short dmColor;
        [FieldOffset(62)]
        public short dmDuplex;
        [FieldOffset(64)]
        public short dmYResolution;
        [FieldOffset(66)]
        public short dmTTOption;
        [FieldOffset(68)]
        public short dmCollate;
        [FieldOffset(70)]
        public fixed char dmFormName[CCHFORMNAME];
        [FieldOffset(102)]
        public short dmLogPixels;
        [FieldOffset(104)]
        public int dmBitsPerPel;
        [FieldOffset(108)]
        public int dmPelsWidth;
        [FieldOffset(112)]
        public int dmPelsHeight;
        [FieldOffset(116)]
        public int dmDisplayFlags;
        [FieldOffset(116)]
        public int dmNup;
        [FieldOffset(120)]
        public int dmDisplayFrequency;

    }
}
