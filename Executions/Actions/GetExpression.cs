using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class GetExpression<T> : Expression<T> {
        public string Name { get; private set; }

        public GetExpression(string name) {
            Name = name;
        }

        public override Execution CreateExecution(Execution parent) {
            return new GetExecution<T>(parent, this);
        }
    }
}
