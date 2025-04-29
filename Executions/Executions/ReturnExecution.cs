using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class ReturnExecution<T> : SetExpressionExecution<T> {
        public ReturnAction<T> ReturnAction { get { return (ReturnAction<T>)Action; } }

        public ReturnExecution(Execution parent, ReturnAction<T> returnAction) : base(parent, returnAction) { }
    }
}
