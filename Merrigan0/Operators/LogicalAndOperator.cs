using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class LogicalAndOperator : ComparisonOperator {
        public static LogicalAndOperator Only = new LogicalAndOperator();

        protected LogicalAndOperator() : base("&&") 
        {
            RegisterTypedCalculationFunction<bool>((l, r) => (bool)l && (bool)r);
        }
    }
}
