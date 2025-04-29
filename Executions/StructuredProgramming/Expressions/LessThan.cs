using System;

namespace Executions.StructuredProgramming.Expressions {
    public class LessThan<T> : Condition {
        public Expression<T> Left { get; private set; }
        public Expression<T> Right { get; private set; }

        public LessThan(Expression<T> left, Expression<T> right) {
            Left = left;
            Right = right;
        }
    }
}
