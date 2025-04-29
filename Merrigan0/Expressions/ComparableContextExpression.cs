using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class ComparableContextExpression : Expression {
        public override Array<Expression> Children { get { return Array<Expression>.Empty; } }

        public override bool TryEvaluate(Glom context, out object value) {
            //// Scope may need to have some hook to a POCO value
            value = (IComparable)context;
            return true;
        }
    }
}
