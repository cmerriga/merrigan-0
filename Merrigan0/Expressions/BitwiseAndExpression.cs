using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class BitwiseAndExpression : BinaryExpression {
        ////static BitwiseAndExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(BitwiseAndExpression), (l, r) => (sbyte)l & (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(BitwiseAndExpression), (l, r) => (byte)l & (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(BitwiseAndExpression), (l, r) => (short)l & (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(BitwiseAndExpression), (l, r) => (ushort)l & (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(BitwiseAndExpression), (l, r) => (char)l & (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(BitwiseAndExpression), (l, r) => (int)l & (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(BitwiseAndExpression), (l, r) => (uint)l & (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(BitwiseAndExpression), (l, r) => (long)l & (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(BitwiseAndExpression), (l, r) => (ulong)l & (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(BitwiseAndExpression), (l, r) => (ulong)(float)l & (ulong)(float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(BitwiseAndExpression), (l, r) => (ulong)(double)l & (ulong)(double)r);
        ////}

        protected override string Separator { get { return "&"; } }

        public BitwiseAndExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return BitwiseAndOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
