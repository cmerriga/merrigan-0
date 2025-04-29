using System;
using System.Collections.Generic;
using Merrigan0.Internal.DotNet.Extensions;

namespace Merrigan0 {
    public static class Math {
        [WhatItIs("The golden mean, or golden ration. (sqrt(5) - 1) / 2")]
        public static double Phi = 0.61803398875;

        [WhatItIs("The average or mean of a group of numbers.")]
        [Example("(1.1, 2.2, 3.3)", 2.2)]
        [Dependent("Sum")]
        [return: Range("values.Min", true, "values.Max", true)]
        public static double Average(params double[] values) {
            return Sum(values) / values.Length;
        }

        [WhatItIs("The average or mean of a group of numbers.")]
        [Example("(1.1, 2.2, 3.3)", 2.2)]
        [Dependent("Sum")]
        public static double Average([NotEmpty] Array<double> values) {
            return Sum(values) / values.Length;
        }

        [WhatItIs("The average or mean of a group of numbers.")]
        [Example(4, 24)]
        [return: Positive]
        public static long Factorial([NotNegative] long n) {
            long factorialSoFar = 1;
            while (n > 1) {
                factorialSoFar *= n;
                --n;
            }
            return factorialSoFar;
        }

        [Example("(1, 4, 2, 1, 3, 1, 5)", 2)]
        [Dependent("Array<>, Array<>.Sorted, Array<>[]")]
        public static double Median([Positive("Length")] double[] rs) {
            if (rs.Length == 0) {
                throw new Exception("Cannot get the median of a 0-item array.");
            }

            Array<double> sortedRs = ((Array<double>)rs).Sorted();
            long iBeforeMiddle = (sortedRs.Length - 1) / 2;
            if (rs.Length % 2 == 0) {
                return (sortedRs[iBeforeMiddle] + sortedRs[iBeforeMiddle + 1]) / 2.0;
            }
            return sortedRs[iBeforeMiddle];
        }

        [WhatItDoes("Whether the object is a numeric value that is negative.")]
        [Example(-1L, true)]
        [Example(1L, false)]
        //[Example(-1m, true)]
        [Example(-1f, true)]
        [Example(-1d, true)]
        [Example("abc", false)]
        private static bool Negative(object number, Type type = null) {
            if (type == null) {
                type = number.GetType();
            }
            if (type == typeof(int)) {
                return (int)number < 0;
            } else if (type == typeof(long)) {
                return (long)number < 0L;
            } else if (type == typeof(double)) {
                return (double)number < 0.0;
            } else if (type == typeof(float)) {
                return (float)number < 0.0f;
            } else if (type == typeof(decimal)) {
                return (decimal)number < 0.0m;
            } else if (type == typeof(short)) {
                return (short)number < 0;
            } else if (type == typeof(sbyte)) {
                return (sbyte)number < 0;
            }
            return false;
        }

        [WhatItIs("The remainder after dividing two numbers.")]
        [return: Range("-grain", false, "grain", false)]
        [Example(100.0, 10.0, 0.0)]
        [Example(101.0, 10.0, 1.0)]
        [Example(-101.0, 10.0, -1.0)]
        public static double Remainder(double r, double grain) {
            int grains = (int)(r / grain);
            return r - (grains * grain);
        }

        [Note("Also called sigmoid function or sigmoid squash function.")]
        [return: Range(-1.0, false, 1.0, false)]
        [Example(0.0, 0.5)]
        [Example(2.0, 0.880797077978)]
        [Example(-2.0, 0.119202922022)]
        public static double SigmoidSquash(double r) {
            return 1.0 / (1.0 + System.Math.Exp(-r));
        }

        [return: Range(-1.0, true, 1.0, true)]
        [Example(0.0, 0.0)]
        public static double Squash(double r) {
            return r / (1.0 + System.Math.Abs(r));
        }

        [WhatItDoes("Maps an unlimited non-negative value to a value in range [0.0, 1.0].")]
        [Dependent("SquashPositiveToReal")]
        [return: Normal]
        public static double SquashPositiveToNormal([NotNegative] double r) {
            if (r == 0.0) {
                return 0.5;
            }
            double positiveToReal = StretchPositiveToReal(r);
            return SquashToNormal(positiveToReal);
        }

        [return: Normal]
        [Example(0.0, 0.5)]
        public static double SquashToNormal(double r) {
            return SquashToRange(r, 0.0, 1.0);
        }

        [return: Range("min", true, "max", true)]
        [Example(0.0, 0.5, 1.5, 1.0)]
        public static double SquashToRange(double r, [Less("max")] double min, double max) {
            double average = (min + max) / 2.0;
            double range = max - min;
            return average + Squash(r) * range / 2.0;
        }

        [WhatItDoes("Maps an unlimited positive value to a value in range [-inf, inf].")]
        [Example(1.0, 0.0)]
        public static double StretchPositiveToReal([Positive] double r) {
            return System.Math.Log(r);
        }

        [Example("(-1.0, 0.0, 2.0)", 1.0)]
        public static double Sum(IEnumerable<double> values) {
            double sumSoFar = 0.0;
            foreach (double value in values) {
                sumSoFar += value;
            }
            return sumSoFar;
        }

        [Example(1.000001, 1.0, 0.001, true)]
        [Example(0.999999, 1.0, 0.001, true)]
        [Example(1.000001, 1.0, 0.0000001, false)]
        [Example(0.999999, 1.0, 0.0000001, false)]
        public static bool Within(double r1, double r2, [NotNegative] double epsilon) {
            double difference = System.Math.Abs(r1 - r2);
            if (difference < epsilon) {
                return true;
            }
            return false;
        }
    }
}
