using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ImagesInternal {
    public struct UnitRgba {
        public float R;
        public float G;
        public float B;
        public float A;

        public UnitRgba(double r, double g, double b, double a = 1.0) {
            R = (float)r;
            G = (float)g;
            B = (float)b;
            A = (float)a;
        }

        public UnitRgba(float r, float g, float b, float a = 1.0f) {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}
