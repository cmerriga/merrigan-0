using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class RightShiftExpression : BinaryExpression {
        ////static RightShiftExpression() {
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(sbyte), typeof(int), (v, t) => (sbyte)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(byte), typeof(int), (v, t) => (byte)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(short), typeof(int), (v, t) => (short)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(ushort), typeof(int), (v, t) => (ushort)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(char), typeof(int), (v, t) => (char)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(int), typeof(int), (v, t) => (int)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(uint), typeof(int), (v, t) => (uint)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(long), typeof(int), (v, t) => (long)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(ulong), typeof(int), (v, t) => (ulong)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(float), typeof(int), (v, t) => (long)(float)v >> (int)t);
        ////    RegisterCalculationFunction(typeof(RightShiftExpression), typeof(double), typeof(int), (v, t) => (ulong)(double)v >> (int)t);
        ////}

        protected override string Separator { get { return ">>"; } }

        public RightShiftExpression(Expression value, Expression times) : this(value, times, null) { }

        public RightShiftExpression(Expression value, Expression times, Type type) : base(value, times, type) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return RightShiftOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
