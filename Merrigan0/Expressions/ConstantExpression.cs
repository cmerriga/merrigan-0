using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class ConstantExpression : Expression {
        [MayBeNull]
        public object Value { get; private set; }

        public override Array<Expression> Children { get { return Array<Expression>.Empty; } }

        public ConstantExpression(object value) : this(value == null ? typeof(object) : value.GetType(), value) { }

        public ConstantExpression(Type type, object value) : base(type) {
            Value = Conversion.Convert(value, type);
        }

        public override bool TryEvaluate(Glom context, out object value) {
            value = Value;
            return true;
        }

        public override string ToString() { return Value == null ? "[null]" : Value.ToString(); }
    }

    public class ConstantExpression<T> : Expression {
        public T Value { get; private set; }

        public override Array<Expression> Children { get { return Array<Expression>.Empty; } }

        public ConstantExpression(T value)
            : base(typeof(T)) {
            Value = value;
        }

        public override bool TryEvaluate(Glom context, out object value) {
            value = Value;
            return true;
        }

        public override string ToString() { return Value == null ? "[null]" : Value.ToString(); }
    }
}
