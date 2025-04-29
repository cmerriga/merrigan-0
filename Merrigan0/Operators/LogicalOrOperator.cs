using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class LogicalOrOperator : ComparisonOperator {
        public static LogicalOrOperator Only = new LogicalOrOperator();

        protected LogicalOrOperator() : base("||") 
        {
            RegisterTypedCalculationFunction<bool>((l, r) => (bool)l || (bool)r);
        }
    }
}
