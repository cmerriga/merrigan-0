using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class NotNegativeAttribute : ConstraintAttribute {
        public NotNegativeAttribute() { }
        public NotNegativeAttribute(string nml) : base(nml) { }

        protected override Expression CreateConstraint() {
            Expression newExpression = new GreaterOrEqualsExpression(SubjectExpression, 0);
            return newExpression;
        }
    }
}
