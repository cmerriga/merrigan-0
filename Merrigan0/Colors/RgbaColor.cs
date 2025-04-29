using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ColorsInternal {
    public class RgbaColor : Color {
        private Rgba rgba;

        public byte A { get { return rgba.A; } }
        public byte B { get { return rgba.B; } }
        public byte G { get { return rgba.G; } }
        public byte R { get { return rgba.R; } }

        public override Rgba Rgba { get { return rgba; } }

        public RgbaColor(int r, int g, int b, int a) :
            this(new Rgba(r, g, b, a)) {
        }

        public RgbaColor(Rgba rgba) {
            this.rgba = rgba;
        }
    }
}
