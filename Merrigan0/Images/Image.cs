using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.ImagesInternal;

namespace Merrigan0 {
    public abstract class Image : IParent<Image> {
        //public Image From(int y, int x, long h, long w, Color color) {
        //    return new SingleColorImage(y, x, h, w, color);
        //}

        // All the component images
        public Array<Image> Children { get { return Array<Image>.Empty; } }

        public abstract Color Color(double y, double x);

        public virtual Color Color(int y, int x) {
            return Color((double)y, (double)x);
        }
    }
}
