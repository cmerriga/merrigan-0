using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class LogicalAndExpression : BinaryExpression {
        ////static LogicalAndExpression() {
        ////    RegisterTypedCalculationFunction<bool>(typeof(LogicalAndExpression), (l, r) => (bool)l && (bool)r);
        ////}

        protected override string Separator { get { return "&&"; } }

        public LogicalAndExpression(Expression left, Expression right) : base(left, right, typeof(bool)) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return LogicalAndOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
