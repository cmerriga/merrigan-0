using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class RemainderExpression : BinaryExpression {
        ////static RemainderExpression() {
        ////    RegisterTypedCalculationFunction<sbyte>(typeof(RemainderExpression), (l, r) => (sbyte)l % (sbyte)r);
        ////    RegisterTypedCalculationFunction<byte>(typeof(RemainderExpression), (l, r) => (byte)l % (byte)r);
        ////    RegisterTypedCalculationFunction<short>(typeof(RemainderExpression), (l, r) => (short)l % (short)r);
        ////    RegisterTypedCalculationFunction<ushort>(typeof(RemainderExpression), (l, r) => (ushort)l % (ushort)r);
        ////    RegisterTypedCalculationFunction<char>(typeof(RemainderExpression), (l, r) => (char)l % (char)r);
        ////    RegisterTypedCalculationFunction<int>(typeof(RemainderExpression), (l, r) => (int)l % (int)r);
        ////    RegisterTypedCalculationFunction<uint>(typeof(RemainderExpression), (l, r) => (uint)l % (uint)r);
        ////    RegisterTypedCalculationFunction<long>(typeof(RemainderExpression), (l, r) => (long)l % (long)r);
        ////    RegisterTypedCalculationFunction<ulong>(typeof(RemainderExpression), (l, r) => (ulong)l % (ulong)r);
        ////}

        protected override string Separator { get { return "%"; } }

        public RemainderExpression(Expression left, Expression right) : this(left, right, null) { }

        public RemainderExpression(Expression left, Expression right, Type type) : base(left, right, type) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return RemainderOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
