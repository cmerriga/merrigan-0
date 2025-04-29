using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;

namespace Executions.Executions {
    ////// A call will have a phase to resolve parameter expressions, and then a phase
    ////// wherein the action is executed
    ////public class CallExecution : Execution {
    ////    // 0 is 1st parameter, 1 is 2nd, etc.
    ////    // Then one more for the desired action
    ////    private int iNextAction;
    ////    private List<Execution> executionsSoFar = new List<Execution>();

    ////    public CallAction CallAction { get { return (CallAction)Action; } }
    ////    public Execution[] Executions { get { return executionsSoFar.ToArray(); } }

    ////    public CallExecution(Execution parent, CallAction action) : base(parent, action) {
    ////        // If there's no parent but we expect to share a context, make one
    ////        if (parent == null) {
    ////            Context = new SimpleContext();
    ////        } else {
    ////            Context = parent.Context;
    ////        }
    ////    }

    ////    protected override bool TryFinish() {
    ////        // Schedule the first condition
    ////        iNextAction = -1;
    ////        ReportChildDone(null);

    ////        // Take this one out of the queue
    ////        Executor.RemoveCurrent();

    ////        return false;
    ////    }

    ////    public override void ReportChildDone(Execution child) {
    ////        // Schedule next action or finish
    ////        int nParametersToResolve = Action.NamedExpressions.Count;

    ////        // If a parameter was just resolved, record the value in the context
    ////        if (iNextAction <= nParametersToResolve) {
    ////            object value = child.Result;
    ////            Context.Set(Action.NamedExpressions[iNextAction].Key, value);
    ////        }

    ////        // If the action was just executed, mark done
    ////        if (iNextAction >= (nParametersToResolve + 1)) {
    ////            MarkDone();
    ////            return;
    ////        }

    ////        // Start the next action: either another parameter to resolve, of the real desired action
    ////        Execution execution;
    ////        if (iNextAction < nParametersToResolve) {
    ////            execution = Action.NamedExpressions[iNextAction].Value.CreateExecution(this);
    ////        } else {
    ////            execution = Action.Action.CreateExecution(this);
    ////        }
    ////        ++iNextAction;
    ////        Executor.Schedule(execution);
    ////        executionsSoFar.Add(execution);
    ////    }
    ////}
}
