using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class BitwiseXorOperator : ComparisonOperator {
        public static BitwiseXorOperator Only = new BitwiseXorOperator();

        protected BitwiseXorOperator()
            : base("^") {
            RegisterTypedCalculationFunction<sbyte>((l, r) => (sbyte)l ^ (sbyte)r);
            RegisterTypedCalculationFunction<byte>((l, r) => (byte)l ^ (byte)r);
            RegisterTypedCalculationFunction<short>((l, r) => (short)l ^ (short)r);
            RegisterTypedCalculationFunction<ushort>((l, r) => (ushort)l ^ (ushort)r);
            RegisterTypedCalculationFunction<char>((l, r) => (char)l ^ (char)r);
            RegisterTypedCalculationFunction<int>((l, r) => (int)l ^ (int)r);
            RegisterTypedCalculationFunction<uint>((l, r) => (uint)l ^ (uint)r);
            RegisterTypedCalculationFunction<long>((l, r) => (long)l ^ (long)r);
            RegisterTypedCalculationFunction<ulong>((l, r) => (ulong)l ^ (ulong)r);
        }
    }
}
