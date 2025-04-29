using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(
        AttributeTargets.Field | 
        AttributeTargets.Method | 
        AttributeTargets.Parameter | 
        AttributeTargets.Property | 
        AttributeTargets.ReturnValue, 
        AllowMultiple = true)]
    [Untested]
    public class EqualsAttribute : ComparisonAttribute {
        public EqualsAttribute(object right) : base(right) { }
        public EqualsAttribute(object left, object right) : base(left, right) { }

        protected override Expression CreateComparisonExpression(Expression left, Expression right) {
            return new EqualsExpression(left, right);
        }
    }
}
