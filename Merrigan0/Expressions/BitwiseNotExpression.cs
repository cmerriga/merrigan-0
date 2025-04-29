using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class BitwiseNotExpression : UnaryExpression {
        ////static BitwiseNotExpression() {
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(sbyte), o => ~(sbyte)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(byte), o => ~(byte)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(short), o => ~(short)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(ushort), o => ~(ushort)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(char), o => ~(char)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(int), o => ~(int)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(uint), o => ~(uint)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(long), o => ~(long)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(ulong), o => ~(ulong)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(float), o => ~(ulong)(float)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(double), o => ~(ulong)(double)o);
        ////    RegisterCalculationFunction(typeof(BitwiseNotExpression), typeof(decimal), o => ~(ulong)(decimal)o);
        ////}

        protected override String Prefix { get { return "~"; } }

        public BitwiseNotExpression(Expression baseExpression) : base(baseExpression) { }

        public override bool TryGetCalculateFunction(Type type, out Func<object, object> calculateFunction) {
            return BitwiseNotOperator.Only.TryGetCalculateFunction(type, out calculateFunction);
        }
    }
}
