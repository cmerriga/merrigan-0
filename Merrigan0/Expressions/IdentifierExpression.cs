using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class IdentifierExpression : Expression {
        private String identifier;

        public override Array<Expression> Children { get { return Array<Expression>.Empty; } }

        public IdentifierExpression(String identifier) {
            this.identifier = identifier;
        }

        public override bool TryEvaluate(Glom context, out object value) {
            //// how should nulls be handled?
            value = context[identifier];
            return value != null;
        }

        public override string ToString() { return identifier; }
    }
}
