using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    public abstract class GridImage : Image {
        public int H { get; private set; }
        public int W { get; private set; }

        public GridImage(int h, int w) {
            H = h;
            W = w;
        }

        public override Color Color(double y, double x) {
            return Color((int)(y + 0.5), (int)(x + 0.5));
        }

        public override Color Color(int y, int x) {
            throw new NotImplementedException();
        }
    }
}
