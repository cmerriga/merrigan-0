using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class LessOrEqualsExpression : ComparisonExpression {
        ////static LessOrEqualsExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(LessOrEqualsExpression), (l, r) => (sbyte)l <= (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(LessOrEqualsExpression), (l, r) => (byte)l <= (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(LessOrEqualsExpression), (l, r) => (short)l <= (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(LessOrEqualsExpression), (l, r) => (ushort)l <= (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(LessOrEqualsExpression), (l, r) => (char)l <= (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(LessOrEqualsExpression), (l, r) => (int)l <= (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(LessOrEqualsExpression), (l, r) => (uint)l <= (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(LessOrEqualsExpression), (l, r) => (long)l <= (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(LessOrEqualsExpression), (l, r) => (ulong)l <= (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(LessOrEqualsExpression), (l, r) => (float)l <= (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(LessOrEqualsExpression), (l, r) => (double)l <= (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(LessOrEqualsExpression), (l, r) => (decimal)l <= (decimal)r);
        ////    RegisterTypedCalculationFunction<IComparable>(typeof(LessOrEqualsExpression), (l, r) => ((IComparable)l).CompareTo((IComparable)r) <= 0);
        ////}

        protected override string Separator { get { return "<="; } }

        public LessOrEqualsExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return LessOrEqualsOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
