using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class IsExpression : ComparisonExpression {
        static IsExpression() {
            RegisterCalculationFunction(typeof(IsExpression), typeof(object), typeof(Type), (t1, t2) => Reflection.Implements((Type)t1, (Type)t2));
        }

        protected override string Separator { get { return "is"; } }

        public IsExpression(Expression left, Expression right) : base(left, right) { }
    }
}
