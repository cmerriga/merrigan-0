using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class FalseAttribute : ConstraintAttribute {
        public FalseAttribute() : this(null) { }

        public FalseAttribute(object subject)
            : base(subject) {
        }

        protected override Expression CreateConstraint() {
            return new NotExpression(SubjectExpression);
        }
    }
}
