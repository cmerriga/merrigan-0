using System;

namespace Executions.StructuredProgramming {
    public partial class Call<T> : Expression<T> { //// Expression?
        public Function<T> Function { get; private set; }
        public Expression[] ParameterValues { get; private set; }

        public Call(Function<T> function, Expression[] parameterValues) {
            Function = function;
            ParameterValues = parameterValues;
        }
    }
}
