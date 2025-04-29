using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class GreaterExpression : ComparisonExpression {
        ////static GreaterExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(GreaterExpression), (l, r) => (sbyte)l > (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(GreaterExpression), (l, r) => (byte)l > (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(GreaterExpression), (l, r) => (short)l > (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(GreaterExpression), (l, r) => (ushort)l > (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(GreaterExpression), (l, r) => (char)l > (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(GreaterExpression), (l, r) => (int)l > (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(GreaterExpression), (l, r) => (uint)l > (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(GreaterExpression), (l, r) => (long)l > (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(GreaterExpression), (l, r) => (ulong)l > (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(GreaterExpression), (l, r) => (float)l > (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(GreaterExpression), (l, r) => (double)l > (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(GreaterExpression), (l, r) => (decimal)l > (decimal)r);
        ////    RegisterTypedCalculationFunction<IComparable>(typeof(GreaterExpression), (l, r) => ((IComparable)l).CompareTo((IComparable)r) > 0);
        ////}

        protected override string Separator { get { return ">"; } }

        public GreaterExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return GreaterOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
