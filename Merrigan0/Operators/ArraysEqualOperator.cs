using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class ArraysEqualOperator : ComparisonOperator {
        public static ArraysEqualOperator Only = new ArraysEqualOperator();

        protected ArraysEqualOperator(): base("==") {
            RegisterTypedCalculationFunction<IEnumerable>((l, r) => Utilities.EnumerablesEqual((IEnumerable)l, (IEnumerable)r));
        }
    }
}
