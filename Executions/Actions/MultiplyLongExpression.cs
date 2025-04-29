using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class MultiplyLongExpression : BinaryExpression<long> {
        public MultiplyLongExpression(object left, object right) : base(left, right) { }

        public override Execution CreateExecution(Execution parent) {
            return new MultiplyLongExecution(parent, this);
        }
    }
}
