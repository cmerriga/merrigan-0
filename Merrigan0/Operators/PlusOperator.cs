using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class PlusOperator : TypedBinaryOperator<bool> {
        public static PlusOperator Only = new PlusOperator();

        protected PlusOperator()
            : base("+") {
            ////RegisterTypedCalculationFunction<sbyte>((l, r) => (sbyte)l + (sbyte)r);
            ////RegisterTypedCalculationFunction<byte>((l, r) => (byte)l + (byte)r);
            ////RegisterTypedCalculationFunction<short>((l, r) => (short)l + (short)r);
            ////RegisterTypedCalculationFunction<ushort>((l, r) => (ushort)l + (ushort)r);
            ////RegisterTypedCalculationFunction<char>((l, r) => (char)l + (char)r);
            RegisterTypedCalculationFunction<int>((l, r) => (int)l + (int)r);
            RegisterTypedCalculationFunction<uint>((l, r) => (uint)l + (uint)r);
            RegisterTypedCalculationFunction<long>((l, r) => (long)l + (long)r);
            RegisterTypedCalculationFunction<ulong>((l, r) => (ulong)l + (ulong)r);
            RegisterTypedCalculationFunction<float>((l, r) => (float)l + (float)r);
            RegisterTypedCalculationFunction<double>((l, r) => (double)l + (double)r);
            RegisterTypedCalculationFunction<decimal>((l, r) => (decimal)l + (decimal)r);
            RegisterTypedCalculationFunction<string>((l, r) => (string)l + (string)r);
            RegisterCalculationFunction(typeof(string), typeof(object), (l, r) => (string)l + r);
            RegisterCalculationFunction(typeof(object), typeof(string), (l, r) => l + (string)r);
            RegisterTypedCalculationFunction<String>((l, r) => (String)l + (String)r);
            RegisterCalculationFunction(typeof(String), typeof(object), (l, r) => (String)l + r);
            RegisterCalculationFunction(typeof(object), typeof(String), (l, r) => l + (String)r);
        }
    }
}
