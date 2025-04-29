using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class CastExpression : Expression {
        private Func<object, object> castFunction;
        private Expression baseExpression;

        public override Array<Expression> Children { get { return Array<Expression>.From(baseExpression); } }

        public CastExpression(Expression baseExpression, Type type) : base(type) {
            this.baseExpression = baseExpression;
            if (baseExpression.Type == null) {
                castFunction = Reflection.CastFunction(baseExpression.Type, type);
            }
        }

        public override string ToString() { return "(" + Type.Name + ")"; }

        // 
        public override bool TryEvaluate(Glom context, out object value) {
            object baseValueObject;
            if (!baseExpression.TryEvaluate(context, out baseValueObject)) {
                value = null;
                return false;
            }
            if (castFunction != null) {
                value = castFunction(baseValueObject);
            } else {
                value = Reflection.Cast(baseValueObject, Type);
            }
            return true;
        }
    }
}
