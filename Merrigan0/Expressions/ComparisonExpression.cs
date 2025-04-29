using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public abstract class ComparisonExpression : BinaryExpression {
        protected ComparisonExpression(Expression left, Expression right) : base(left, right, typeof(bool)) { }
    }
}
