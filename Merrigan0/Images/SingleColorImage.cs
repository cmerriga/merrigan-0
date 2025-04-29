using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ImagesInternal {
    public class SingleColorImage : GridImage {
        private Color color;

        public SingleColorImage(int y, int x, int h, int w, Color color) : base(h, w) {
            this.color = color;
        }

        public override Color Color(double y, double x) {
            return color;
        }

        public override Color Color(int y, int x) {
            return color;
        }
    }
}
