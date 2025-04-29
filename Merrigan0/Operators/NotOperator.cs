using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class NotOperator : TypedUnaryOperator<bool> {
        public static NotOperator Only = new NotOperator();

        protected NotOperator() : base("~") 
        {
            RegisterCalculationFunction(typeof(bool), o => !(bool)o);
        }
    }
}
