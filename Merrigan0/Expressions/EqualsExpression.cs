using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class EqualsExpression : ComparisonExpression {
        ////static EqualsExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(EqualsExpression), (l, r) => (sbyte)l == (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(EqualsExpression), (l, r) => (byte)l == (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(EqualsExpression), (l, r) => (short)l == (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(EqualsExpression), (l, r) => (ushort)l == (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(EqualsExpression), (l, r) => (char)l == (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(EqualsExpression), (l, r) => (int)l == (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(EqualsExpression), (l, r) => (uint)l == (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(EqualsExpression), (l, r) => (long)l == (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(EqualsExpression), (l, r) => (ulong)l == (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(EqualsExpression), (l, r) => (float)l == (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(EqualsExpression), (l, r) => (double)l == (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(EqualsExpression), (l, r) => (decimal)l == (decimal)r);
        ////    RegisterTypedCalculationFunction<IComparable>(typeof(EqualsExpression), (l, r) => ((IComparable)l).CompareTo((IComparable)r) == 0);
        ////}

        protected override string Separator { get { return "=="; } }

        public EqualsExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return EqualsOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
