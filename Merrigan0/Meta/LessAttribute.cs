using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    [Untested]
    public class LessAttribute : ComparisonAttribute {
        public LessAttribute(object right) : base(right) { }
        public LessAttribute(object left, object right) : base(left, right) { }

        protected override Expression CreateComparisonExpression(Expression left, Expression right) {
            return new LessExpression(left, right);
        }
    }
}
