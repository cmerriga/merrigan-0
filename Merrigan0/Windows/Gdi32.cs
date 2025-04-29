using System;
using System.Runtime.InteropServices;

namespace Merrigan0 {
    public static class Gdi32 {
        public const int DIB_RGB_COLORS = 0;
        public const int BI_RGB = 0x0;
        public const int SRCCOPY = 0x00CC0020;

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public uint biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;

            public void Init() {
                biSize = (uint)Marshal.SizeOf(this);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO {
            public BITMAPINFOHEADER bmiHeader;
            public byte bmiColors_rgbBlue;
            public byte bmiColors_rgbGreen;
            public byte bmiColors_rgbRed;
            public byte bmiColors_rgbReserved;
        }

        [DllImport("gdi32.dll")]
        public static extern int SetPixelV(IntPtr hdc, int x, int y, int color);

        [DllImport("gdi32.dll")]
        public static extern int SetDIBitsToDevice(
            IntPtr hdc,
            int xDestination, int yDestination,
            uint dwWidth, uint dwHeight,
            int xSource, int ySource,
            uint uStartScan, uint cScanLines,
            IntPtr pbBits,
            ref BITMAPINFO pbmi,
            uint fuColorUse);


        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(
            IntPtr hdc,        // handle to DC
            int nWidth,     // width of bitmap, in pixels
            int nHeight     // height of bitmap, in pixels
        );

        [DllImport("gdi32.dll")]
        public static extern int BitBlt(
            IntPtr hdcDest, // handle to destination DC
            int nXDest,  // x-coord of destination upper-left corner
            int nYDest,  // y-coord of destination upper-left corner
            int nWidth,  // width of destination rectangle
            int nHeight, // height of destination rectangle
            IntPtr hdcSrc,  // handle to source DC
            int nXSrc,   // x-coordinate of source upper-left corner
            int nYSrc,   // y-coordinate of source upper-left corner
            uint dwRop  // raster operation code
        );

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
            IntPtr hDC,
            IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern int DeleteDC(IntPtr hdc);
    }
}
