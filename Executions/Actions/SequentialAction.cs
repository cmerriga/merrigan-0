using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class SequentialAction : Action {
        public Action[] Actions { get; private set; }

        public SequentialAction(params Action[] actions) {
            Actions = actions;
        }

        public override Execution CreateExecution(Execution parent) {
            return new SequentialExecution(parent, this);
        }
    }
}