using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // Keep in sync with ConstraintAttribute usage
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true)]
    [Untested]
    public class NotAttribute : ConstraintAttribute {
        public NotAttribute() : this(null) { }

        public NotAttribute(object subject)
            : base(subject) {
        }

        protected override Expression CreateConstraint() {
            Expression newExpression = new EqualsExpression(SubjectExpression, false);
            return newExpression;
        }
    }
}
