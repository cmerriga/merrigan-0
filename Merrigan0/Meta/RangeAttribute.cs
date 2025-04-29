using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class RangeAttribute : ConstraintAttribute {
        private bool maxInclusive;
        private object maxOrNml;
        private bool minInclusive;
        private object minOrNml;

        public RangeAttribute(object minOrNml, bool minInclusive, object maxOrNml, bool maxInclusive) {
            this.maxOrNml = maxOrNml;
            this.maxInclusive = maxInclusive;
            this.minOrNml = minOrNml;
            this.minInclusive = minInclusive;
        }

        public RangeAttribute([Example("(0.0, 1.0]")] string definition) {
            throw new NotImplementedException();
        }

        protected override Expression CreateConstraint() {
            Expression expression = new RangeExpression(SubjectExpression, minOrNml, minInclusive, maxOrNml, maxInclusive);
            return expression;
        }
    }
}
