using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    // A recursive call is just like a regular call, except that the execution done is defined by how
    // many levels up the execution is that must be duplicated
    public partial class RecursiveCallExecution : Execution {
        private object result;

        public Execution Execution { get; protected set; }
        public RecursiveCallAction RecursiveExpressionAction { get { return (RecursiveCallAction)Action; } }
        public override object Result {
            get {
                return result;
            }
        }
        public RecursiveCallExecution(Execution parent, RecursiveCallAction action)
            : base(parent, GetParentAction(parent, action.LevelsUp - 1)) {
        }

        // Waits for the action called to finish
        protected override bool TryFinish() {
            if (Execution == null) {
                Execution = Action.CreateExecution(this);
                Executor.Schedule(Execution);
            }
            bool done = Execution.Done;
            if (done) {
                result = Execution.Result;
            }
            return done;
        }

        protected static Action GetParentAction(Execution execution, int levelsUp) {
            Execution currentExecution = execution;
            while (levelsUp > 0) {
                currentExecution = currentExecution.Parent;
                --levelsUp;
            }
            return currentExecution.Action;
        }
    }
}
