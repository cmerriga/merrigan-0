using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ColorsInternal {
    public class RgbColor : Color {
        private Rgb rgb;

        public byte B { get { return rgb.B; } }
        public byte G { get { return rgb.G; } }
        public byte R { get { return rgb.R; } }

        public override Rgba Rgba { get { return new Rgba(R, G, B); } }

        public RgbColor(int r, int g, int b) :
            this(new Rgb(r, g, b)) {
        }

        public RgbColor(Rgb rgb) {
            this.rgb = rgb;
        }
    }
}
