using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.GlomsInternal;

namespace Merrigan0 {
    [Untested]
    public class ContextAppendExpression : Expression {
        private Expression nameExpression;
        private Expression valueExpression;

        public override Array<Expression> Children { get { return Array<Expression>.From(nameExpression, valueExpression); } }

        public ContextAppendExpression(Expression nameExpression, Expression expression) {
            this.nameExpression = nameExpression;
            this.valueExpression = expression;
        }

        public override bool TryEvaluate(Glom context, out object value) {
            value = new SetGlom(context, context.Evaluate(nameExpression), context.Evaluate(valueExpression));
            return true;
        }

        public override string ToString() { return ":"; }
    }
}
