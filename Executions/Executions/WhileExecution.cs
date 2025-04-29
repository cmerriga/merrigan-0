using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    public partial class WhileExecution : Execution {
        private int phase;
        private List<Execution> history = new List<Execution>();

        public WhileAction WhileAction { get { return (WhileAction)Action; } }

        public WhileExecution(Execution parent, WhileAction action) : base(parent, action) { }

        protected override bool TryFinish() {
            // Schedule the first condition
            ReportChildDone(null);

            // Take this one out of the queue
            Executor.RemoveCurrent();

            return false;
        }

        public override void ReportChildDone(Execution child) {
            if (phase == 1) {
                // The condition just finished
                if (((ExpressionExecution<bool>)child).TypedResult) {
                    Execution blockExecution = WhileAction.Action.CreateExecution(this);
                    history.Add(blockExecution);
                    phase = 0;
                    Executor.Schedule(blockExecution);
                } else {
                    MarkDone();
                    return;
                }
            } else { // phase should be 0
                // The block just finished
                Execution conditionExecution = WhileAction.Condition.CreateExecution(this);
                history.Add(conditionExecution);
                phase = 1;
                Executor.Schedule(conditionExecution);
            }
        }
    }
}
