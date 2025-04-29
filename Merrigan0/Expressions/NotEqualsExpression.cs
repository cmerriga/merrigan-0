using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class NotEqualsExpression : ComparisonExpression {
        ////static NotEqualsExpression() {
        ////    RegisterCalculationFunction(typeof(object), typeof(object), typeof(object), (l, r) => !Object.Equals(l, r));
        ////}

        protected override string Separator { get { return "!="; } }

        public NotEqualsExpression(Expression left, Expression right) : base(left, right) { }

        public override bool TryGetCalculateFunction(Type leftType, Type rightType, out Func<object, object, object> calculateFunction) {
            return NotEqualsOperator.Only.TryGetCalculateFunction(leftType, rightType, out calculateFunction);
        }
    }
}
