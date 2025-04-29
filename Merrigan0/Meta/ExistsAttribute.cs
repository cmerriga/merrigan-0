using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class ExistsAttribute : ConstraintAttribute {
        // Specify nothing: uses current context
        public ExistsAttribute() { }

        public ExistsAttribute(string nml) : base(nml) { }

        protected override Expression CreateConstraint() {
            Expression newExpression = new ExistsExpression(SubjectExpression);
            return newExpression;
        }
    }
}
