using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class LeftShiftOperator : TypedBinaryOperator<object> {
        public static LeftShiftOperator Only = new LeftShiftOperator();

        protected LeftShiftOperator()
            : base("<<") {
            RegisterCalculationFunction(typeof(sbyte), typeof(int), (v, t) => (sbyte)v << (int)t);
            RegisterCalculationFunction(typeof(byte), typeof(int), (v, t) => (byte)v << (int)t);
            RegisterCalculationFunction(typeof(short), typeof(int), (v, t) => (short)v << (int)t);
            RegisterCalculationFunction(typeof(ushort), typeof(int), (v, t) => (ushort)v << (int)t);
            RegisterCalculationFunction(typeof(char), typeof(int), (v, t) => (char)v << (int)t);
            RegisterCalculationFunction(typeof(int), typeof(int), (v, t) => (int)v << (int)t);
            RegisterCalculationFunction(typeof(uint), typeof(int), (v, t) => (uint)v << (int)t);
            RegisterCalculationFunction(typeof(long), typeof(int), (v, t) => (long)v << (int)t);
            RegisterCalculationFunction(typeof(ulong), typeof(int), (v, t) => (ulong)v << (int)t);
        }
    }
}
