using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // A way to test whether a glom, after transormation via an expression, implies a particular fact
    [Untested]
    public class ImpliesExpression : Expression {
        public Expression Expression { get; private set; }
        public Fact Fact { get; private set; }

        public override Array<Expression> Children { get { return Array<Expression>.Empty; } }

        public ImpliesExpression(Expression expression, Fact fact)
            : base(typeof(bool)) {
            Expression = expression;
            Fact = fact;
        }

        public override bool TryEvaluate(Glom context, out object value) {
            object expressionResult;
            if (!Expression.TryEvaluate(context, out expressionResult)) {
                value = null;
                return false;
            }
            Glom expressionResultGlom = Glom.From(expressionResult);
            value = expressionResultGlom.Implies(Fact);
            return true;
        }
    }
}
