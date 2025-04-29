using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class PositiveAttribute : ConstraintAttribute {
        // Specify nothing: uses current context
        public PositiveAttribute() { }

        public PositiveAttribute(string nml) : base(nml) { }

        protected override Expression CreateConstraint() {
            Expression newExpression = new GreaterExpression(SubjectExpression, 0);
            return newExpression;
        }

        ////public override Constraint GetConstraint(Type type) {

        ////    //// The thing that must be positive is the result of evaluating the expression
        ////    return (Constraint)Reflection.New(typeof(GreaterConstraint<>), type, leftExpression, Utilities.Convert(0, type));
        ////}
    }
}
