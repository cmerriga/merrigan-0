using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ColorsInternal {
    public class GrayColor : Color {
        public byte K { get; private set; }

        public override Rgba Rgba { get { return new Rgba(K, K, K); } }

        public GrayColor(int k) {
            K = (byte)k;
        }
    }
}
