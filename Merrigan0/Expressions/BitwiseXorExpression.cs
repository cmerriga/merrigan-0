using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class BitwiseXorExpression : BinaryExpression {
        ////static BitwiseXorExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(BitwiseXorExpression), (l, r) => (sbyte)l ^ (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(BitwiseXorExpression), (l, r) => (byte)l ^ (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(BitwiseXorExpression), (l, r) => (short)l ^ (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(BitwiseXorExpression), (l, r) => (ushort)l ^ (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(BitwiseXorExpression), (l, r) => (char)l ^ (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(BitwiseXorExpression), (l, r) => (int)l ^ (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(BitwiseXorExpression), (l, r) => (uint)l ^ (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(BitwiseXorExpression), (l, r) => (long)l ^ (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(BitwiseXorExpression), (l, r) => (ulong)l ^ (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(BitwiseXorExpression), (l, r) => (ulong)(float)l ^ (ulong)(float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(BitwiseXorExpression), (l, r) => (ulong)(double)l ^ (ulong)(double)r);
        ////}

        protected override string Separator { get { return "^"; } }

        public BitwiseXorExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return BitwiseXorOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
