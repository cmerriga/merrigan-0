using System;

namespace Executions.StructuredProgramming.Expressions {
    public class Literal<T> : Expression<T> {
        public T Value { get; private set; }

        public Literal(T value) {
            Value = value;
        }
    }
}
