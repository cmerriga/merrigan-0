using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class LessExpression : ComparisonExpression {
        ////static LessExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(LessExpression), (l, r) => (sbyte)l < (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(LessExpression), (l, r) => (byte)l < (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(LessExpression), (l, r) => (short)l < (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(LessExpression), (l, r) => (ushort)l < (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(LessExpression), (l, r) => (char)l < (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(LessExpression), (l, r) => (int)l < (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(LessExpression), (l, r) => (uint)l < (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(LessExpression), (l, r) => (long)l < (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(LessExpression), (l, r) => (ulong)l < (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(GreaterOrEqualsExpression), (l, r) => (float)l < (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(GreaterOrEqualsExpression), (l, r) => (double)l < (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(GreaterOrEqualsExpression), (l, r) => (decimal)l < (decimal)r);
        ////    RegisterTypedCalculationFunction<IComparable>(typeof(LessExpression), (l, r) => ((IComparable)l).CompareTo((IComparable)r) < 0);
        ////}

        protected override string Separator { get { return "<"; } }

        public LessExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return LessOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
