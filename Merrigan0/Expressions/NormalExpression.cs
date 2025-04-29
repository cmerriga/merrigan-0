using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class NormalExpression : UnaryExpression {
        static NormalExpression() {
            RegisterCalculationFunction(typeof(bool), typeof(float), o => 0.0f < (float)o && (float)o < 1.0f);
            RegisterCalculationFunction(typeof(bool), typeof(double), o => 0.0 < (double)o && (double)o < 1.0);
            RegisterCalculationFunction(typeof(bool), typeof(decimal), o => 0.0m < (decimal)o && (decimal)o < 1.0m);
        }

        public NormalExpression(Expression expression) : base(expression, typeof(bool)) { }

        //protected override Func<object, object, object> CalculateFunction(Type upcastType) {
        //    return (l, r) => Object.Equals(l, r);
        //}
    }
}
