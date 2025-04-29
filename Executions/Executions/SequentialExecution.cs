using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public partial class SequentialExecution : Execution {
        private int iNextAction;
        private List<Execution> executionsSoFar = new List<Execution>();

        public SequentialAction SequentialAction { get { return (SequentialAction)Action; } }
        public Execution[] Executions { get { return executionsSoFar.ToArray(); } }

        public SequentialExecution(Execution parent, SequentialAction sequentialAction) : base(parent, sequentialAction) { }

        protected override bool TryFinish() {
            // Schedule the first condition
            iNextAction = -1;
            ReportChildDone(null);

            // Take this one out of the queue
            Executor.RemoveCurrent();

            return false;
        }

        public override void ReportChildDone(Execution child) {
            // Schedule next action or finish
            ++iNextAction;
            if (iNextAction >= SequentialAction.Actions.Length) {
                MarkDone();
                return;
            }

            Execution currentExecution = SequentialAction.Actions[iNextAction].CreateExecution(this);
            Executor.Schedule(currentExecution);
            executionsSoFar.Add(currentExecution);
        }
    }
}
