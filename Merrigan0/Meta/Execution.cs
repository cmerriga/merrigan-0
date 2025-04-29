using System;
using System.Collections.Generic;
using System.Diagnostics;

//namespace Merrigan0.MetaInternal {
//    /*
//     * Action
//     *      abstract
//     *      something out of which you can construct a "program"
//     * NativeAction
//     *      a finite-CPU non-looping block
//     *      void Do()
//     * SequentialAction
//     *      has list of actions to do in sequence
//     * ParallelAction
//     *      creates each child Execution
//     *      bool DoChunk()
//     *          keeps track of next child in round-robin
//     *          call DoChunk on that child
//     *          mark child finished in round-robin
//     *          when all finished, return true
//     * LoopAction
//     *      bool DoChunk()
//     *          keeps IEnumerator to work on
//     *          when IEnumerator exhausted, return true
//     * IfElseAction
//     *      bool DoChunk()
//     *          eval condition
//     *          call DoChunk() on one chunk, or the other
//     *          return true if that returned true
//     */
//    [Untested]
//    public abstract class Action {
//        public abstract Execution CreateExecution();
//    }

//    public class SimpleAction : Action {
//        public Action Action { get; private set; }

//        public SimpleAction(Action action) {
//            Action = action;
//        }
//    }

//    public class SequentialAction : Action {
//        public Array<Action> Actions { get; private set; }

//        public SequentialAction(Array<Action> actions) {
//            Actions = actions;
//        }
//    }
    
//    public class ParallelAction : Action {
//        public Array<Action> Actions { get; private set; }

//        public ParallelAction(Array<Action> actions) {
//            Actions = actions;
//        }
//    }

//    //public class ConditionalAction : Action {
//    //    public Array<Action> Actions { get; private set; }
//    //    protected Action ChosenAction;
//    //    protected System.Func<ExecutionContext, long> GetCondition { get; private set; }

//    //    public override bool DoChunk(ExecutionContext context) {
//    //        if (ChosenAction == null) {
//    //            long iChosenAction = GetCondition(context);
//    //            ChosenAction = Actions[iChosenAction];
//    //        }
//    //        return ChosenAction.DoChunk(context);
//    //    }
//    //}

//    public class IfElseAction {
//        protected Func<ExecutionContext, bool> condition;

//        public Action ElseAction { get; private set; }
//        public Action IfAction { get; private set; }

//        public IfElseAction(Func<ExecutionContext, bool> condition, Action ifAction, Action elseAction) {
//            this.condition = condition;
//            ElseAction = elseAction;
//            IfAction = ifAction;
//        }
//    }

//    /*
//     * Execution
//     *      something which represents one execution of an action
//     *      equivalent to a native execution stack, though the state at any time is a tree since executions may be parallel
//     *      for periodic executions, a single execution runs them all
//     *      should represent the entire history of a program, and be retraceable to where
//     *          running forward from a particular pre-action point begets the exact same result
//     *      abstract bool DoChunk()
//     * Sequence
//     *      bool DoChunk()
//     *          keeps track of steps and which on
//     *          move step forward only when this DoChunk returns true
//     * Parallel
//     *      creates each child Execution
//     *      bool DoChunk()
//     *          keeps track of next child in round-robin
//     *          call DoChunk on that child
//     *          mark child finished in round-robin
//     *          when all finished, return true
//     * Loop
//     *      bool DoChunk()
//     *          keeps IEnumerator to work on
//     *          when IEnumerator exhausted, return true
//     * If/else
//     *      bool DoChunk()
//     *          eval condition
//     *          call DoChunk() on one chunk, or the other
//     *          return true if that returned true
//     */
//    public abstract class Execution {
//        protected ExecutionContext context;
//        protected ExecutionState state;

//        public Action Action { get; private set; }
//        public Execution Parent { get; private set; }
//        public object Result { get; protected set; }
//        public System.DateTime? Start { get; private set; }

//        public Execution(Execution parent, Action action, System.DateTime? start) {
//            Action = action;
//            Start = start;
//        }

//        public virtual void Break() {
//            state = ExecutionState.CancelRequested;
//        }

//        public abstract bool DoChunk(ExecutionContext context);

//        public void Do(ExecutionContext context) {
//            while (!DoChunk(context)) { }
//        }
//    }

//    public class SequentialExecution : Execution {
//        private long iNextAction;
//        private Execution currentExecution;
//        private MutableArray<Execution> executionsSoFar = new MutableArray<Execution>();

//        public SequentialAction Action { get; private set; }
//        public Array<Execution> Executions { get { return executionsSoFar.Current; } }

//        public SequentialExecution(SequentialAction sequentialAction) {
//            Action = sequentialAction;
//        }

//        public override bool DoChunk(ExecutionContext context) {
//            if (iNextAction >= Action.Actions.Length) {
//                return true;
//            }

//            if (currentExecution == null) {
//                currentExecution = Action.Actions[iNextAction].CreateExecution();
//                executionsSoFar.Append(currentExecution);
//                ++iNextAction;
//            }

//            bool done = currentExecution.DoChunk(context);
//            if (done) {
//                currentExecution = null;
//            }
//            return iNextAction >= Action.Actions.Length;
//        }
//    }

//    public class ParallelExecution {
//        private ParallelAction parallelAction;
//        protected long iCurrentAction;
//        protected Array<ExecutionTracker> executions;

//        public Array<Action> Actions { get; private set; }

//        public ParallelExecution(ParallelAction parallelAction) {
//            MutableArray<ExecutionTracker> executionTrackersSoFar = new MutableArray<ExecutionTracker>();
//            for (long i = 0; i < parallelAction.Actions.Length; ++i) {
//                executionTrackersSoFar.Append(new ExecutionTracker() { Execution = Actions[i].CreateExecution() });
//            }
//            executions = executionTrackersSoFar.Current;
//        }

//        public override bool DoChunk(ExecutionContext context) {
//            long actionTrackersLength = executions.Length;
//            long actionsToCheck = actionTrackersLength;
//            while (actionsToCheck > 0) {
//                ExecutionTracker actionTracker = executions[iCurrentAction];
//                if (!actionTracker.Done) {
//                    bool done = actionTracker.Execution.DoChunk(context);
//                    if (done) {
//                        ++iCurrentAction;
//                        if (iCurrentAction >= actionTrackersLength) {
//                            iCurrentAction = 0;
//                        }
//                        actionTracker.Done = true;

//                        // If this was the last one not done, then we are done with the combined action now
//                        if (actionsToCheck == 1) {
//                            return true;
//                        } else {
//                            return false;
//                        }
//                    }
//                    return false;
//                }
//                --actionsToCheck;
//            }

//            // None of the actions were undone. This was an unnecessary call
//            return true;
//        }

//        protected class ExecutionTracker {
//            public Execution Execution;
//            public bool Done;
//        }
//    }

//    public class IfElseExecution {
//        public Action ChosenAction { get; private set; }
//        public IfElseAction IfElseAction { get; private set; }

//        public IfElseExecution(IfElseAction ifElseAction) {
//            IfElseAction = ifElseAction;
//        }

//        public override bool DoChunk(ExecutionContext context) {
//            if (ChosenAction == null) {
//                long iChosenAction = GetCondition(context);
//                ChosenAction = Actions[iChosenAction];
//            }
//            return ChosenAction.DoChunk(context);
//        }
//    }

//    public class PeriodicExecution : Execution {
//    }

//    public class AsynchronousExecution {
//        public bool Done { get { return (State & ExecutionState.DoneMask) != 0; } }
//        //public List<Event> History { get; private set; }
//        public bool PossiblyActive { get { return (State & ExecutionState.PossiblyActiveMask) != 0; } }
//        public bool PossiblyDone { get { return (State & ExecutionState.PossiblyDoneMask) != 0; } }
//        public double Speed { get; set; }
//        public ExecutionState State { get; protected set; }

//        //private Continuation continuation;

//        protected AsynchronousExecution() {
//            //History = new List<Event>();
//        }

//        public void RequestCancel() { ////Continuation continuation) {
//            // If it hasn't started
//            State = ExecutionState.CancelRequested;
//            //RecordEvent("request-cancel");
//            //this.continuation = continuation;
//            OnRequestCancel();
//        }

//        public void RequestPause() { ////Continuation continuation) {
//            State = ExecutionState.CancelRequested;
//            //RecordEvent("request-pause");
//            //this.continuation = continuation;
//            OnRequestPause();
//        }

//        public void RequestResume() { ////Continuation continuation) {
//            State = ExecutionState.ResumeRequested;
//            //RecordEvent("request-resume");
//            //this.continuation = continuation;
//            OnRequestResume();
//        }

//        public void RequestStart() { ////Continuation continuation) {
//            State = ExecutionState.ResumeRequested;
//            //RecordEvent("request-start");
//            //this.continuation = continuation;
//            OnRequestStart();
//        }

//        //protected abstract Event CreateEvent(string description, DateTime time);
//        protected abstract void OnRequestCancel();
//        protected abstract void OnRequestPause();
//        protected abstract void OnRequestResume();
//        protected abstract void OnRequestStart();

//        protected void ReportTransitionCompleted(ExecutionState newState) {
//            State = newState;
//            //if (continuation != null) {
//            //    continuation.AfterSuccess();
//            //    continuation = null;
//            //}
//        }


//        //protected virtual void RecordEvent(string description) {
//        //    History.Add(CreateEvent(description, DateTime.UtcNow));
//        //}
//    }
//}
