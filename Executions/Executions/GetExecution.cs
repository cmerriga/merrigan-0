using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class GetExecution<T> : ExpressionExecution<T> {
        public GetExpression<T> GetExpression { get { return (GetExpression<T>)Action; } }

        public GetExecution(Execution parent, GetExpression<T> action) : base(parent, action) { }

        protected override bool TryFinish() {
            TypedResult = Context.Get<T>(GetExpression.Name);
            return true;
        }
    }
}
