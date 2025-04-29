using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class LogicalOrExpression : BinaryExpression {
        ////static LogicalOrExpression() {
        ////    RegisterTypedCalculationFunction<bool>(typeof(LogicalOrExpression), (l, r) => (bool)l || (bool)r);
        ////}

        protected override string Separator { get { return "||"; } }

        public LogicalOrExpression(Expression left, Expression right) : base(left, right, typeof(bool)) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return LogicalOrOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
