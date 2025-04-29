using System;

namespace Executions.StructuredProgramming {
    public abstract class Expression {
        public abstract Type Type { get; }
    }

    public abstract class Expression<T> : Expression {
        public override Type Type { get { return typeof(T); } }
    }
}
