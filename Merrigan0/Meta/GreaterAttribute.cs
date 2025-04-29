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
    public class GreaterAttribute : ComparisonAttribute {
        public GreaterAttribute(object right) : base(right) { }
        public GreaterAttribute(object left, object right) : base(left, right) { }

        protected override Expression CreateComparisonExpression(Expression left, Expression right) {
            return new GreaterExpression(left, right);
        }
    }
}
