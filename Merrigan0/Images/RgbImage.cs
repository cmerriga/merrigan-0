using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ColorsInternal;

namespace Merrigan0.ImagesInternal {
    public class RgbImage : GridImage {
        private Rgb[,] aRgb;

        public Rgb this[int y, int x] {
            get {
                return aRgb[y, x];
            }
        }

        public RgbImage(Rgb[,] aRgb) :
            base(aRgb.GetLength(0), aRgb.GetLength(1)) {
            this.aRgb = aRgb;
        }

        public RgbImage(int h, int w) :
            base(h, w) {
            aRgb = new Rgb[h, w];
        }

        public override Color Color(int y, int x) {
            return new RgbColor(this[y, x]);
        }
    }
}
