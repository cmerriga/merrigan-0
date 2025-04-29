using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class CustomAction : Action {
        public Action SystemAction { get; private set; }

        public CustomAction(Action systemAction) {
            SystemAction = systemAction;
        }

        public override Execution CreateExecution(Execution parent) {
            return new CustomExecution(parent, this);
        }
    }
}
