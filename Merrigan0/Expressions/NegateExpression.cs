using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class NegateExpression : UnaryExpression {
        ////static NegateExpression() {
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(sbyte), o => -(sbyte)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(byte), o => -(byte)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(short), o => -(short)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(ushort), o => -(ushort)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(char), o => -(char)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(int), o => -(int)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(uint), o => -(uint)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(long), o => -(long)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(float), o => -(float)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(double), o => -(double)o);
        ////    RegisterCalculationFunction(typeof(NegateExpression), typeof(decimal), o => -(decimal)o);
        ////}

        protected override String Prefix { get { return "-"; } }

        public NegateExpression(Expression baseExpression) : base(baseExpression) { }

        public override bool TryGetCalculateFunction(Type type, out Func<object, object> calculateFunction) {
            return NegateOperator.Only.TryGetCalculateFunction(type, out calculateFunction);
        }
    }
}
