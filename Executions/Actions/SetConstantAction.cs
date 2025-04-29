using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Executions;

namespace Executions.Actions {
    public class SetConstantAction<T> : Action {
        public T Value { get; private set; }
        public string Name { get; private set; }

        public SetConstantAction(string name, T value) {
            Name = name;
            Value = value;
        }

        public override Execution CreateExecution(Execution parent) {
            return new SetConstantExecution<T>(parent, this);
        }
    }
}
