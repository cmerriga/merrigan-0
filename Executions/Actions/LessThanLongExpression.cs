using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class LessThanLongExpression : BinaryExpression<bool> {
        public LessThanLongExpression(object left, object right) : base(left, right) { }

        public override Execution CreateExecution(Execution parent) {
            return new LessThanLongExecution(parent, this);
        }
    }
}
