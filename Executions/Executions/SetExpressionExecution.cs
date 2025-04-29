using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class SetExpressionExecution<T> : Execution {
        public SetExpressionAction<T> SetExpressionAction { get { return (SetExpressionAction<T>)Action; } }

        public SetExpressionExecution(Execution parent, SetExpressionAction<T> setExpressionAction) : base(parent, setExpressionAction) { }

        protected override bool TryFinish() {
            Parent.Context.Set(SetExpressionAction.Name, PrerequisiteExecutions[0].Value);
            return true;
        }
    }
}
