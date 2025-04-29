using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    [Untested]
    public class LessOrEqualAttribute : ComparisonAttribute {
        public LessOrEqualAttribute(object right) : base(right) { }
        public LessOrEqualAttribute(object left, object right) : base(left, right) { }

        protected override Expression CreateComparisonExpression(Expression left, Expression right) {
            return new LessOrEqualsExpression(left, right);
        }
    }
}
