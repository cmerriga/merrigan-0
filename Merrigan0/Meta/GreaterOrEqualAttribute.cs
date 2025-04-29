using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    [Untested]
    public class GreaterOrEqualAttribute : ComparisonAttribute {
        public GreaterOrEqualAttribute(object right) : base(right) { }
        public GreaterOrEqualAttribute(object left, object right) : base(left, right) { }

        protected override Expression CreateComparisonExpression(Expression left, Expression right) {
            return new GreaterOrEqualsExpression(left, right);
        }
    }
}
