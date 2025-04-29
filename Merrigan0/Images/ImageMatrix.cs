using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0 {
//    public class ImageMatrix : Matrix<double> {
//        private double a;
//        private double b;
//        private double c;
//        private double d;
//        private double e;
//        private double f;
//        private double g;
//        private double h;
//        private double i;
//        private bool noTranslation;

//        public override long Height { get { return 3; } }
//        public override long Width { get { return 3; } }

//        public ImageMatrix(
//            double a, double b, double c,
//            double d, double e, double f,
//            double g, double h, double i) {
//            this.a = a;
//            this.b = b;
//            this.c = c;
//            this.d = d;
//            this.e = e;
//            this.f = f;
//            this.g = g;
//            this.h = h;
//            this.i = i;

//            if (g == 0.0 && h == 0.0) {
//                noTranslation = true;
//            }
//        }

//        public Point Transform(Point point) {
//            if (noTranslation) {
//                // Transform the point using only the upper-left 2x2
//                return new Point(a * point.X + d * point.X, b * point.Y + e * point.Y);
//            }

//            // Transform the point using all 3x3
//            return new Point(a * point.X + d * point.Y + g, b * point.X + e * point.Y + h);
//        }

//        public override bool TryGetItem(long r, long c, out double item) {
//            if (r == 0) {
//                if (c == 0) {
//                    item = a;
//                } else if (c == 1) {
//                    item = b;
//                } else if (c == 2) {
//                    item = this.c;
//                } else {
//                    item = default(double);
//                    return false;
//                }
//                return true;
//            } else if (r == 1) {
//                if (c == 0) {
//                    item = d;
//                } else if (c == 1) {
//                    item = e;
//                } else if (c == 2) {
//                    item = f;
//                } else {
//                    item = default(double);
//                    return false;
//                }
//                return true;
//            } else if (r == 2) {
//                if (c == 0) {
//                    item = g;
//                } else if (c == 1) {
//                    item = h;
//                } else if (c == 2) {
//                    item = i;
//                } else {
//                    item = default(double);
//                    return false;
//                }
//                return true;
//            } else {
//                item = default(double);
//                return false;
//            }
//        }
//    }
//}
