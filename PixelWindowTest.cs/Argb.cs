
using System;
using System.Runtime.InteropServices;

namespace Merrigan0 {
    [StructLayout(LayoutKind.Explicit)]
    public struct Argb {
        public static Argb FromGray(byte gray) {
            return new Argb() {
                A = 255,
                R = gray,
                G = gray,
                B = gray
            };
        }

        public static Argb FromRgb(byte r, byte g, byte b) {
            return new Argb() {
                A = 255,
                R = r,
                G = g,
                B = b
            };
        }

        [FieldOffset(3)]
        public byte A;

        [FieldOffset(0)]
        public uint Uint;

        //[FieldOffset(0)]
        //public Color Color;

        [FieldOffset(2)]
        public byte R;

        [FieldOffset(1)]
        public byte G;

        [FieldOffset(0)]
        public byte B;
    }
}
