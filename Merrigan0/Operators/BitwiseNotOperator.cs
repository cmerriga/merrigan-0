using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class BitwiseNotOperator : TypedUnaryOperator<object> {
        public static BitwiseNotOperator Only = new BitwiseNotOperator();

        protected BitwiseNotOperator() : base("~") 
        {
            RegisterCalculationFunction(typeof(sbyte), o => ~(sbyte)o);
            RegisterCalculationFunction(typeof(byte), o => ~(byte)o);
            RegisterCalculationFunction(typeof(short), o => ~(short)o);
            RegisterCalculationFunction(typeof(ushort), o => ~(ushort)o);
            RegisterCalculationFunction(typeof(char), o => ~(char)o);
            RegisterCalculationFunction(typeof(int), o => ~(int)o);
            RegisterCalculationFunction(typeof(uint), o => ~(uint)o);
            RegisterCalculationFunction(typeof(long), o => ~(long)o);
            RegisterCalculationFunction(typeof(ulong), o => ~(ulong)o);
        }
    }
}
