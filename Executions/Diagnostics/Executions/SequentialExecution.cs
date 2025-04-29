using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public partial class SequentialExecution : Execution {
        protected override string DebuggerDisplay {
            get {
                StringBuilder builder = new StringBuilder();
                builder.Append(base.DebuggerDisplay);
                int actionsCompleted = iNextAction + 1;
                builder.AppendFormat(" {0} completed", actionsCompleted);
                if (Done) {
                    builder.Append(", done");
                }
                return builder.ToString();
            }
        }
    }
}
