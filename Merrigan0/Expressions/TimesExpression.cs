using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class TimesExpression : BinaryExpression {
        ////static TimesExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(TimesExpression), (l, r) => (sbyte)l * (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(TimesExpression), (l, r) => (byte)l * (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(TimesExpression), (l, r) => (short)l * (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(TimesExpression), (l, r) => (ushort)l * (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(TimesExpression), (l, r) => (char)l * (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(TimesExpression), (l, r) => (int)l * (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(TimesExpression), (l, r) => (uint)l * (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(TimesExpression), (l, r) => (long)l * (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(TimesExpression), (l, r) => (ulong)l * (ulong)r);
        ////    RegisterTypedCalculationFunction<float>(typeof(TimesExpression), (l, r) => (float)l * (float)r);
        ////    RegisterTypedCalculationFunction<double>(typeof(TimesExpression), (l, r) => (double)l * (double)r);
        ////    RegisterTypedCalculationFunction<decimal>(typeof(TimesExpression), (l, r) => (decimal)l * (decimal)r);
        ////}

        protected override string Separator { get { return "*"; } }

        public TimesExpression(Expression left, Expression right) : this(left, right, null) { }

        public TimesExpression(Expression left, Expression right, Type type) : base(left, right, type) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return TimesOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
