using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class SetConstantExecution<T> : Execution {
        public SetConstantAction<T> SetConstantAction { get { return (SetConstantAction<T>)Action; } }

        public SetConstantExecution(Execution parent, SetConstantAction<T> setConstantAction) : base(parent, setConstantAction) { }

        protected override bool TryFinish() {
            Parent.Context.Set(SetConstantAction.Name, SetConstantAction.Value);
            return true;
        }
    }
}
