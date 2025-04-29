using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public abstract class ComparisonOperator : TypedBinaryOperator<bool> {
        protected ComparisonOperator(String symbol) : base(symbol) { }
    }
}
