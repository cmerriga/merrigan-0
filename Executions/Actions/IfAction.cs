using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class IfAction : Action {
        public Action Action { get; private set; }
        public Expression<bool> Condition { get; private set; }

        public IfAction(Expression<bool> condition, Action action) {
            Action = action;
            Condition = condition;
        }

        public override Execution CreateExecution(Execution parent) {
            return new IfExecution(parent, this);
        }
    }
}
