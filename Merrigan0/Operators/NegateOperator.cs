using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class NegateOperator : TypedUnaryOperator<object> {
        public static NegateOperator Only = new NegateOperator();

        protected NegateOperator()
            : base("-") {
            RegisterCalculationFunction(typeof(sbyte), o => -(sbyte)o);
            RegisterCalculationFunction(typeof(byte), o => -(byte)o);
            RegisterCalculationFunction(typeof(short), o => -(short)o);
            RegisterCalculationFunction(typeof(ushort), o => -(ushort)o);
            RegisterCalculationFunction(typeof(char), o => -(char)o);
            RegisterCalculationFunction(typeof(int), o => -(int)o);
            RegisterCalculationFunction(typeof(uint), o => -(uint)o);
            RegisterCalculationFunction(typeof(long), o => -(long)o);
            RegisterCalculationFunction(typeof(float), o => -(float)o);
            RegisterCalculationFunction(typeof(double), o => -(double)o);
            RegisterCalculationFunction(typeof(decimal), o => -(decimal)o);
        }
    }
}
