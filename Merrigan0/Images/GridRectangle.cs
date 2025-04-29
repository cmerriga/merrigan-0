using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    public class GridRectangle {
        // The exclusive bottom border
        private int bottom;

        // The inclusive left border
        private int left;

        // The exclusive right border
        private int right;

        // The inclusive top border
        private int top;

        public int Bottom { get { return bottom; } }
        public int Height { get { return bottom - top; } }
        public int Left { get { return left; } }
        public int Right { get { return right; } }
        public int Top { get { return top; } }
        public int Width { get { return right - left; } }

        public GridRectangle(int top, int left, int bottom, int right) {
            this.bottom = bottom;
            this.left = left;
            this.right = right;
            this.top = top;
        }
    }
}
