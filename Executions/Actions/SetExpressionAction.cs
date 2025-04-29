using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class SetExpressionAction<T> : Action {
        public Expression<T> Expression { get; private set; }
        public string Name { get; private set; }

        public SetExpressionAction(string variable, Expression<T> expression) : base(expression) {
            Expression = expression;
            Name = variable;
        }

        public override Execution CreateExecution(Execution parent) {
            return new SetExpressionExecution<T>(parent, this);
        }
    }
}
