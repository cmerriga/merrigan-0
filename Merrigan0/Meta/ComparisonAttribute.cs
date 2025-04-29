using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // Strictly a shim to indicate that a real constraint should be used for the associated target.
    [Untested]
    public abstract class ComparisonAttribute : ConstraintAttribute {
        protected String leftNml;
        protected String rightNml;
        protected object oLeft;
        protected object oRight;

        protected ComparisonAttribute(object oRight) : this(null, oRight) { }

        // oLeft: if null, the context given, such as a parameter the attribute is applied on
        // oRight: 
        protected ComparisonAttribute(object oLeft, object oRight) {
            string leftString = oLeft as string;
            if (leftString != null) {
                leftNml = leftString;
            } else {
                this.oLeft = oLeft;
            }
            string rightString = oRight as string;
            if (rightString != null) {
                rightNml = rightString;
            } else {
                this.oRight = oRight;
            }
        }

        protected override Expression CreateConstraint() {
            Expression leftExpression;
            if (leftNml != null) {
                leftExpression = Nml.Expression(leftNml);
            } else if (oLeft == null) {
                leftExpression = Expression.CurrentContext;
            } else {
                leftExpression = new ConstantExpression(oLeft);
            }
            Expression rightExpression;
            if (rightNml != null) {
                rightExpression = Nml.Expression(rightNml);
            } else {
                rightExpression = new ConstantExpression(oRight);
            }

            Expression newExpression = CreateComparisonExpression(leftExpression, rightExpression);
            return newExpression;
        }

        protected abstract Expression CreateComparisonExpression(Expression left, Expression right);
    }
}
