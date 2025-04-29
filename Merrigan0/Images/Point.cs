using System;

namespace Merrigan0 {
    public struct Point {
        public double X;
        public double Y;

        public Point(double x, double y) {
            X = x;
            Y = y;
        }

        //// Calculates the absolute position from the information in m.
        //public Point Transform(Graphics2DMatrix<double> m) {
        //    // Find this point via the first two columns of the matrix
        //    Graphics2DMatrix<double> transformed = m.TransformPoint(this);
        //}
    }
}
