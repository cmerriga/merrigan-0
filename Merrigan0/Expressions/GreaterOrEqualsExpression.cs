using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class GreaterOrEqualsExpression : ComparisonExpression {
        ////static GreaterOrEqualsExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(GreaterOrEqualsExpression), (l, r) => (sbyte)l >= (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(GreaterOrEqualsExpression), (l, r) => (byte)l >= (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(GreaterOrEqualsExpression), (l, r) => (short)l >= (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(GreaterOrEqualsExpression), (l, r) => (ushort)l >= (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(GreaterOrEqualsExpression), (l, r) => (char)l >= (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(GreaterOrEqualsExpression), (l, r) => (int)l >= (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(GreaterOrEqualsExpression), (l, r) => (uint)l >= (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(GreaterOrEqualsExpression), (l, r) => (long)l >= (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(GreaterOrEqualsExpression), (l, r) => (ulong)l >= (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(GreaterOrEqualsExpression), (l, r) => (float)l >= (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(GreaterOrEqualsExpression), (l, r) => (double)l >= (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(GreaterOrEqualsExpression), (l, r) => (decimal)l >= (decimal)r);
        ////    RegisterTypedCalculationFunction<IComparable>(typeof(GreaterOrEqualsExpression), (l, r) => ((IComparable)l).CompareTo((IComparable)r) >= 0);
        ////}

        protected override string Separator { get { return ">="; } }

        public GreaterOrEqualsExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return GreaterOrEqualsOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
