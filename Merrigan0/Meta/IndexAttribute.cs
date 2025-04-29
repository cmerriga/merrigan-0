using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class IndexAttribute : ConstraintAttribute {
        private object maxOrNml;

        public IndexAttribute([Example("Length")] object maxOrNml) {
            this.maxOrNml = maxOrNml;
        }

        protected override Expression CreateConstraint() {
            Expression expression = new RangeExpression(SubjectExpression, 0, true, maxOrNml, false);
            return expression;
        }
    }
}
