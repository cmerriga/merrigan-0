using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class PlusExpression : BinaryExpression {
        ////static PlusExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(PlusExpression), (l, r) => (sbyte)l + (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(PlusExpression), (l, r) => (byte)l + (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(PlusExpression), (l, r) => (short)l + (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(PlusExpression), (l, r) => (ushort)l + (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(PlusExpression), (l, r) => (char)l + (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(PlusExpression), (l, r) => (int)l + (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(PlusExpression), (l, r) => (uint)l + (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(PlusExpression), (l, r) => (long)l + (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(PlusExpression), (l, r) => (ulong)l + (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(PlusExpression), (l, r) => (float)l + (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(PlusExpression), (l, r) => (double)l + (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(PlusExpression), (l, r) => (decimal)l + (decimal)r);
        ////    RegisterTypedCalculationFunction<string>(typeof(PlusExpression), (l, r) => (string)l + (string)r);
        ////    RegisterCalculationFunction(typeof(PlusExpression), typeof(string), typeof(object), (l, r) => (string)l + r);
        ////    RegisterCalculationFunction(typeof(PlusExpression), typeof(object), typeof(string), (l, r) => l + (string)r);
        ////    RegisterTypedCalculationFunction<String>(typeof(PlusExpression), (l, r) => (String)l + (String)r);
        ////    RegisterCalculationFunction(typeof(PlusExpression), typeof(String), typeof(object), (l, r) => (String)l + r);
        ////    RegisterCalculationFunction(typeof(PlusExpression), typeof(object), typeof(String), (l, r) => l + (String)r);
        ////}

        protected override string Separator { get { return "+"; } }

        public PlusExpression(Expression left, Expression right) : this(left, right, null) { }

        public PlusExpression(Expression left, Expression right, Type type) : base(left, right, type) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return PlusOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
