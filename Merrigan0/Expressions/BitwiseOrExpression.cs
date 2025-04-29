using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class BitwiseOrExpression : BinaryExpression {
        ////static BitwiseOrExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(BitwiseOrExpression), (l, r) => (sbyte)l | (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(BitwiseOrExpression), (l, r) => (byte)l | (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(BitwiseOrExpression), (l, r) => (short)l | (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(BitwiseOrExpression), (l, r) => (ushort)l | (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(BitwiseOrExpression), (l, r) => (char)l | (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(BitwiseOrExpression), (l, r) => (int)l | (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(BitwiseOrExpression), (l, r) => (uint)l | (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(BitwiseOrExpression), (l, r) => (long)l | (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(BitwiseOrExpression), (l, r) => (ulong)l | (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(BitwiseOrExpression), (l, r) => (ulong)(float)l | (ulong)(float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(BitwiseOrExpression), (l, r) => (ulong)(double)l | (ulong)(double)r);
        ////}

        protected override string Separator { get { return "|"; } }

        public BitwiseOrExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return BitwiseOrOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
