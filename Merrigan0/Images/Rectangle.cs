using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    public class Rectangle {
        // The exclusive bottom border
        private double bottom;

        // The inclusive left border
        private double left;

        // The exclusive right border
        private double right;

        // The inclusive top border
        private double top;

        public double Bottom { get { return bottom; } }
        public double Height { get { return bottom - top; } }
        public double Left { get { return left; } }
        public double Right { get { return right; } }
        public double Top { get { return top; } }
        public double Width { get { return right - left; } }

        public Rectangle(double top, double left, double bottom, double right) {
            this.bottom = bottom;
            this.left = left;
            this.right = right;
            this.top = top;
        }
    }
}
