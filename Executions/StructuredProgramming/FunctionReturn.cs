using System;

namespace Executions.StructuredProgramming {
    public class FunctionReturn : Statement {
    }

    public class FunctionReturn<T> : Statement {
        public Expression<T> Expression { get; private set; }
        public Type Type { get { return typeof(T); } }

        public FunctionReturn(Expression<T> expression) { }
    }
}
