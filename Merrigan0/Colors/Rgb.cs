using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ColorsInternal;

namespace Merrigan0 {
    public struct Rgb {
        public static implicit operator Color(Rgb rgb) {
            return new RgbColor(rgb);
        }

        public byte R;
        public byte G;
        public byte B;

        public Rgb(int r, int g, int b) {
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
        }
    }
}
