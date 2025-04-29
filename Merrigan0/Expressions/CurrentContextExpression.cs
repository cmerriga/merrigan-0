using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    // An expression that just inserts the context being used in the current evaluation
    [Untested]
    public class CurrentContextExpression : Expression {
        public override Array<Expression> Children { get { return Array<Expression>.Empty; } }

        public override bool TryEvaluate(Glom context, out object value) {
            value = context; //////
            return true;
        }

        public override string ToString() { return "."; }
    }
}
