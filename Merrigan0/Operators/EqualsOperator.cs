using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class EqualsOperator : ComparisonOperator {
        public static EqualsOperator Only = new EqualsOperator();

        protected EqualsOperator() : base("==") {
            RegisterTypedCalculationFunction<bool>((l, r) => (bool)l == (bool)r);
            RegisterTypedCalculationFunction<sbyte>((l, r) => (sbyte)l == (sbyte)r);
            RegisterTypedCalculationFunction<byte>((l, r) => (byte)l == (byte)r);
            RegisterTypedCalculationFunction<short>((l, r) => (short)l == (short)r);
            RegisterTypedCalculationFunction<ushort>((l, r) => (ushort)l == (ushort)r);
            RegisterTypedCalculationFunction<char>((l, r) => (char)l == (char)r);
            RegisterTypedCalculationFunction<int>((l, r) => (int)l == (int)r);
            RegisterTypedCalculationFunction<uint>((l, r) => (uint)l == (uint)r);
            RegisterTypedCalculationFunction<long>((l, r) => (long)l == (long)r);
            RegisterTypedCalculationFunction<ulong>((l, r) => (ulong)l == (ulong)r);
            RegisterTypedCalculationFunction<float>((l, r) => (float)l == (float)r);
            RegisterTypedCalculationFunction<double>((l, r) => (double)l == (double)r);
            RegisterTypedCalculationFunction<decimal>((l, r) => (decimal)l == (decimal)r);
            RegisterTypedCalculationFunction<IComparable>((l, r) => ((IComparable)l).CompareTo((IComparable)r) == 0);
            RegisterTypedCalculationFunction<object>((l, r) => Object.Equals(l, r));
        }
    }
}
