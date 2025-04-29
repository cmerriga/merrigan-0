using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class ReturnConstantAction<T> : SetConstantAction<T> {
        public ReturnConstantAction(T value) : base(Execution.ReturnVariableName, value) { }

        public override Execution CreateExecution(Execution parent) {
            return new ReturnConstantExecution<T>(parent, this);
        }
    }
}
