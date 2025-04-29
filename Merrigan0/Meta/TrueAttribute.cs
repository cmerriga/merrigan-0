using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue)]
    [Untested]
    public class TrueAttribute : ConstraintAttribute {
        public TrueAttribute() : this(null) { }

        public TrueAttribute(object subject)
            : base(subject) {
        }

        protected override Expression CreateConstraint() {
            return SubjectExpression;
        }
    }
}
