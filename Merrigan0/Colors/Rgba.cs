using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ColorsInternal;

namespace Merrigan0 {
    public struct Rgba {
        public static implicit operator Color(Rgba rgba) {
            return new RgbaColor(rgba);
        }

        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public Rgba(int r, int g, int b, int a = 255) {
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
            A = (byte)a;
        }
    }
}
