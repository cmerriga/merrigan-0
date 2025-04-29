using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class DividedByExpression : BinaryExpression {
        ////static DividedByExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(DividedByExpression), (l, r) => (sbyte)l / (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(DividedByExpression), (l, r) => (byte)l / (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(DividedByExpression), (l, r) => (short)l / (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(DividedByExpression), (l, r) => (ushort)l / (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(DividedByExpression), (l, r) => (char)l / (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(DividedByExpression), (l, r) => (int)l / (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(DividedByExpression), (l, r) => (uint)l / (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(DividedByExpression), (l, r) => (long)l / (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(DividedByExpression), (l, r) => (ulong)l / (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(DividedByExpression), (l, r) => (float)l / (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(DividedByExpression), (l, r) => (double)l / (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(DividedByExpression), (l, r) => (decimal)l / (decimal)r);
        ////}

        protected override string Separator { get { return "/"; } }

        public DividedByExpression(Expression left, Expression right) : this(left, right, null) { }

        public DividedByExpression(Expression left, Expression right, Type type) : base(left, right, type) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return DividedByOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
