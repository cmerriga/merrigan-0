using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class ReturnConstantExecution<T> : SetConstantExecution<T> {
        public ReturnConstantAction<T> ReturnConstantAction { get { return (ReturnConstantAction<T>)Action; } } 

        public ReturnConstantExecution(Execution parent, ReturnConstantAction<T> returnConstantAction) : base(parent, returnConstantAction) { }

        protected override bool TryFinish() {
            Parent.Context.Set(ReturnConstantAction.Name, ReturnConstantAction.Value);
            return true;
        }
    }
}
