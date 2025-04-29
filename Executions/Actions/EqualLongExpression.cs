using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class EqualLongExpression : BinaryExpression<bool> {
        public EqualLongExpression(Expression<long> left, long right) : base(left, right) { }

        public override Execution CreateExecution(Execution parent) {
            return new EqualLongExecution(parent, this);
        }
    }
}
