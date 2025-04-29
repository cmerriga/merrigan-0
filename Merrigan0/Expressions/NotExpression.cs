using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class NotExpression : UnaryExpression {
        private static object calculateFunction(object baseValue) {
            return !(bool)baseValue;
        }

        protected override String Prefix { get { return "!"; } }

        public NotExpression(Expression baseExpression) : base(baseExpression, typeof(bool)) { }

        public override bool TryGetCalculateFunction(Type type, out Func<object, object> calculateFunction) {
            calculateFunction = NotExpression.calculateFunction;
            return true;
        }
    }
}
