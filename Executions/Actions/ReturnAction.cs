using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class ReturnAction<T> : SetExpressionAction<T> {
        public ReturnAction(Expression<T> right) : base(Execution.ReturnVariableName, right) { }

        public override Execution CreateExecution(Execution parent) {
            return new ReturnExecution<T>(parent, this);
        }
    }
}
