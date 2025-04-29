using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public partial class WhileExecution {
        protected override string DebuggerDisplay {
            get {
                StringBuilder builder = new StringBuilder();
                builder.Append(base.DebuggerDisplay);
                int iterationsCompleted = history.Count / 2;
                builder.AppendFormat(" {0} iterations", iterationsCompleted);
                if (Done) {
                    builder.Append(" done");
                } else if (phase == 0) {
                    builder.Append(" condition");
                } else {
                    builder.Append(" block");
                }
                return builder.ToString();
            }
        }
    }
}
