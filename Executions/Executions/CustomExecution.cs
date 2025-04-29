using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class CustomExecution : Execution {
        public CustomAction CustomAction { get { return (CustomAction)Action; } }

        public CustomExecution(Execution parent, CustomAction action) : base(parent, action) { }

        protected override bool TryFinish() {
            CustomAction.SystemAction();
            return true;
        }
    }
}
