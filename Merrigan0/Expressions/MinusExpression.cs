using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class MinusExpression : BinaryExpression {
        ////static MinusExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(MinusExpression), (l, r) => (sbyte)l - (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(MinusExpression), (l, r) => (byte)l - (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(MinusExpression), (l, r) => (short)l - (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(MinusExpression), (l, r) => (ushort)l - (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(MinusExpression), (l, r) => (char)l - (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(MinusExpression), (l, r) => (int)l - (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(MinusExpression), (l, r) => (uint)l - (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(MinusExpression), (l, r) => (long)l - (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(MinusExpression), (l, r) => (ulong)l - (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(MinusExpression), (l, r) => (float)l - (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(MinusExpression), (l, r) => (double)l - (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(MinusExpression), (l, r) => (decimal)l - (decimal)r);
        ////}

        protected override string Separator { get { return "-"; } }

        public MinusExpression(Expression left, Expression right) : this(left, right, null) { }

        public MinusExpression(Expression left, Expression right, Type type) : base(left, right, type) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return MinusOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
