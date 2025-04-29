using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ColorsInternal {
    public class GrayAColor : Color {
        public byte A { get; private set; }
        public byte K { get; private set; }

        public override Rgba Rgba { get { return new Rgba(K, K, K, A); } }

        public GrayAColor(int k, int a) {
            K = (byte)k;
            A = (byte)a;
        }
    }
}
