using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class ExistsExpression : Expression {
        private Expression baseExpression;

        public ExistsExpression(Expression baseExpression) : base(typeof(bool)) {
            this.baseExpression = baseExpression;
        }

        //public override object Evaluate(Glom context) {
        //    object dummyValue;
        //    return !TryEvaluate(context, out dummyValue);
        //}

        public override bool TryEvaluate(Glom context, out object value) {
            object dummyValue;
            value = baseExpression.TryEvaluate(context, out dummyValue);
            return true;
        }

        public override string ToString() {
            return baseExpression.ToString() + " exists";
        }
    }
}
