using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class LeftShiftExpression : BinaryExpression {
        ////static LeftShiftExpression() {
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(sbyte), typeof(int), (v, t) => (sbyte)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(byte), typeof(int), (v, t) => (byte)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(short), typeof(int), (v, t) => (short)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(ushort), typeof(int), (v, t) => (ushort)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(char), typeof(int), (v, t) => (char)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(int), typeof(int), (v, t) => (int)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(uint), typeof(int), (v, t) => (uint)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(long), typeof(int), (v, t) => (long)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(ulong), typeof(int), (v, t) => (ulong)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(float), typeof(int), (v, t) => (long)(float)v << (int)t);
        ////    RegisterCalculationFunction(typeof(LeftShiftExpression), typeof(double), typeof(int), (v, t) => (ulong)(double)v << (int)t);
        ////}

        protected override string Separator { get { return "<<"; } }

        public LeftShiftExpression(Expression value, Expression times) : this(value, times, null) { }

        public LeftShiftExpression(Expression value, Expression times, Type type) : base(value, times, type) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return LeftShiftOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
