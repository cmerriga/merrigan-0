using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ColorsInternal;

namespace Merrigan0.ImagesInternal {
    public class RgbaImage : GridImage {
        private Rgba[,] aRgba;

        public Rgba this[int y, int x] {
            get {
                return aRgba[y, x];
            }
        }

        public RgbaImage(Rgba[,] aRgba) :
            base(aRgba.GetLength(0), aRgba.GetLength(1)) {
            this.aRgba = aRgba;
        }

        public RgbaImage(int h, int w) :
            base(h, w) {
            aRgba = new Rgba[h, w];
        }

        public override Color Color(int y, int x) {
            return new RgbaColor(this[y, x]);
        }
    }
}
