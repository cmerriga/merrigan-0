using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class AddLongExpression : BinaryExpression<long> {
        public AddLongExpression(object left, object right) : base(left, right) { }

        public override Execution CreateExecution(Execution parent) {
            return new AddLongExecution(parent, this);
        }
    }
}
