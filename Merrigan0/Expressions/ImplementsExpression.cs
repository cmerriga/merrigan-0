using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class ImplementsExpression : ComparisonExpression {
        static ImplementsExpression() {
            RegisterCalculationFunction(typeof(IsExpression), typeof(object), typeof(Type), (d, b) => ((Type)b).IsAssignableFrom((Type)b));
        }

        protected override string Separator { get { return "implements"; } }

        public ImplementsExpression(Expression left, Expression right) : base(left, right) { }
    }
}
