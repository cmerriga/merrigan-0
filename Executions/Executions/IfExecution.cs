using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public class IfExecution : Execution {
        private int phase;
        private List<Execution> history = new List<Execution>();

        public IfAction IfAction { get { return (IfAction)Action; } }

        public IfExecution(Execution parent, IfAction ifAction) : base(parent, ifAction) { }

        protected override bool TryFinish() {
            // Schedule the first condition
            ReportChildDone(null);

            // Take this one out of the queue
            Executor.RemoveCurrent();

            return false;
        }

        public override void ReportChildDone(Execution child) {
            if (phase == 0) {
                // Nothing has been run yet
                Execution conditionExecution = IfAction.Condition.CreateExecution(this);
                history.Add(conditionExecution);
                phase = 1;
                Executor.Schedule(conditionExecution);
                return;
            }

            if (phase == 1) {
                // The condition just finished
                if (((ExpressionExecution<bool>)child).TypedResult) {
                    // If it was true, schedule the block and remember it
                    Execution blockExecution = IfAction.Action.CreateExecution(this);
                    history.Add(blockExecution);
                    phase = 2;
                    Executor.Schedule(blockExecution);
                    return;
                }
            }

            // If it didn't have more to process, it is done
            MarkDone();
            return;
        }
    }
}
