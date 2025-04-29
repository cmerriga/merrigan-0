using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public abstract class ExpressionExecution<T> : Execution {
        public override object Result { get { return TypedResult; } }
        public T TypedResult { get; protected set; }

        protected ExpressionExecution(Execution parent, Expression<T> expression) : base(parent, expression) { }
    }
}
