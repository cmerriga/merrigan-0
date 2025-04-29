using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Executions.Actions;
using Executions.Executions;

namespace Executions {
    public struct NamedExecution {
        public Execution Execution;
        public string Name;

        public NamedExecution(string name, Execution execution) {
            Execution = execution;
            Name = name;
        }
    }

    public class PrerequisiteExecution { //// struct?
        public object Constant;
        public Execution Execution;
        ////public Prerequisite Prerequisite;
        
        public object Value {
            get {
                if (Execution != null) {
                    return Execution.Result;
                }
                return Constant;
            }
        }

        ////public PrerequisiteExecution(Execution execution, object constant) {
        ////    Execution = execution;
        ////    Prerequisite = prerequisite;
        ////}
    }

    public abstract partial class Execution {
        public static readonly Execution Dummy = new DummyExecution();
        public const string ReturnVariableName = "return";

        private int iCurrentPrerequisite;

        public Action Action { get; private set; }
        public Context Context { get; protected set; }
        public bool Done { get { return TimeFinished.HasValue; } }
        public Executor Executor { get; set; }
        public Execution Parent { get; private set; }

        // Null at first. May be initialized later or may remain null forever
        public IList<PrerequisiteExecution> PrerequisiteExecutions { get; private set; }

        // Set by TryFinish calls in base class
        public bool PrerequisitesDone { get; protected set; }

        public virtual object Result { get { return null; } }
        public bool Started { get { return TimeStarted.HasValue; } }
        public ExecutorTime? TimeScheduled { get; set; }
        public ExecutorTime? TimeStarted { get; set; }
        public ExecutorTime? TimeFinished { get; protected set; }

        protected Execution(Execution parent, Action action) {
            Action = action;
            Context = parent == null ? new SimpleContext() : parent.Context;
            Parent = parent == null ? Dummy : parent;
        }

        // First have to resolve parameters given and assign to names in the context - well, no
        // Then have to 
        public virtual bool DoChunk() {
            if (!TimeStarted.HasValue) {
                TimeStarted = Executor.Time;
            }
            if (!PrerequisitesDone) {
                if (!TryFinishPrerequisites()) {
                    return false;
                } else {
                    PrerequisitesDone = true;
                }
            }
            bool done = TryFinish();
            if (done) {
                MarkDone();
                return true;
            }
            return false;
        }

        public virtual void ReportChildDone(Execution child) { }

        public void WaitUntilDone() { WaitUntilDone(TimeSpan.FromSeconds(1)); }

        public void WaitUntilDone(TimeSpan maxDelay) {
            // Start with a short wait and double until equal to max delay
            TimeSpan nextDelay = TimeSpan.FromMilliseconds(1);
            while (!Done) {
                Thread.Sleep(nextDelay);
                if (nextDelay < maxDelay) {
                    nextDelay = nextDelay + nextDelay;
                    if (nextDelay > maxDelay) {
                        nextDelay = maxDelay;
                    }
                }
            }
        }

        // This will be called only after the prerequisites have finished
        protected abstract bool TryFinish();
        
        // Makes reasonable progress on any prerequisites. Returns true if they are all done
        protected virtual bool TryFinishPrerequisites() {
            if (PrerequisiteExecutions == null) {
                if (Action.Prerequisites.Count == 0) {
                    return true;
                } else {
                    // Create prerequisite executions. There will definitely be at least one prerequisite, because we already checked that
                    int nPrerequisites = Action.Prerequisites.Count;
                    PrerequisiteExecution[] prerequisiteExecutionsSoFar = new PrerequisiteExecution[nPrerequisites];
                    for (int i = 0; i < nPrerequisites; ++i) {
                        Prerequisite prerequisite = Action.Prerequisites[i];
                        prerequisiteExecutionsSoFar[i] = new PrerequisiteExecution(); //// could be more efficient
                        if (prerequisite.Expression == null) {
                            prerequisiteExecutionsSoFar[i].Constant = prerequisite.Constant;
                        } else {
                            Execution execution = prerequisite.Expression.CreateExecution(this);
                            execution.Executor = Executor;
                            prerequisiteExecutionsSoFar[i].Execution = execution;
                        }
                    }
                    PrerequisiteExecutions = prerequisiteExecutionsSoFar;
                }
            }

            //// MAYBE SHOULD BE SCHEDULED LIKE OTHERS
            // Run any prerequisites until it can't make any more progress
            while (iCurrentPrerequisite < PrerequisiteExecutions.Count) {
                Execution currentPrerequisiteExecution = PrerequisiteExecutions[iCurrentPrerequisite].Execution;
                if (currentPrerequisiteExecution != null) {
                    if (!currentPrerequisiteExecution.Started) {
                        Executor.Schedule(currentPrerequisiteExecution);
                    }
                    if (!currentPrerequisiteExecution.Done) {
                        return false;
                    }
                }
                ++iCurrentPrerequisite;
            }
            return true;
        }

        protected virtual void MarkDone() {
            TimeFinished = Executor.Time;
            if (Parent != null) {
                Parent.ReportChildDone(this);
            }
        }
    }
}
