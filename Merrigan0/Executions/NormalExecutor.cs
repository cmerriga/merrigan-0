using System;
using System.Collections.Generic;
using System.Diagnostics;
using Merrigan0.Internal.DotNet.Polyfills.System;

namespace Merrigan0.ExecutionsInternal {
    [WhatItIs("Executes actions using the .NET stack like normal, with no tracking or checking.")]
    [Untested]
    internal class NormalExecutor : Executor {
        private MutableArray<ExecutorExecution> deferredExecutions = new MutableArray<ExecutorExecution>();
        private long iCurrentDeferredExecution;
        private long nChunksExecuted;

        public override long ChunksExecuted { get { return nChunksExecuted; } }
        public override bool Empty { get { return deferredExecutions.Current.Length == 0; } }

        public override void Do(Action classAction) {
            classAction();
        }

        public override void Do<T1>(Action<T1> classAction, T1 parameter1) {
            classAction(parameter1);
        }

        public override void Do<T1, T2>(Action<T1, T2> classAction, T1 parameter1, T2 parameter2) {
            classAction(parameter1, parameter2);
        }

        public override void Do<T1, T2, T3>(Action<T1, T2, T3> classAction, T1 parameter1, T2 parameter2, T3 parameter3) {
            classAction(parameter1, parameter2, parameter3);
        }

        public override void Do<T1, T2, T3, T4>(Action<T1, T2, T3, T4> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) {
            classAction(parameter1, parameter2, parameter3, parameter4);
        }

        public override void Do<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) {
            classAction(parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        public override void Do<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6) {
            classAction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        public override void Do<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7) {
            classAction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
        }

        public override void Do<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8) {
            classAction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        }

        public override TResult Call<TResult>(Func<TResult> memberFunction) {
            return memberFunction();
        }

        public override MutableExecution BeginAction(Action classAction) {
            MutableExecution execution = new NormalActionExecution(classAction);
            deferredExecutions.Append(new ExecutorExecution(execution));
            return execution;
        }

        public override MutableExecution BeginAction<T1>(Action<T1> classAction, T1 parameter1) {
            return BeginAction(() => { classAction(parameter1); });
        }

        public override MutableExecution BeginAction<T1, T2>(Action<T1, T2> classAction, T1 parameter1, T2 parameter2) {
            return BeginAction(() => { classAction(parameter1, parameter2); });
        }

        public override MutableExecution BeginAction<T1, T2, T3>(Action<T1, T2, T3> classAction, T1 parameter1, T2 parameter2, T3 parameter3) {
            return BeginAction(() => { classAction(parameter1, parameter2, parameter3); });
        }

        public override MutableExecution BeginAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) {
            return BeginAction(() => { classAction(parameter1, parameter2, parameter3, parameter4); });
        }

        public override MutableExecution BeginAction<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) {
            return BeginAction(() => { classAction(parameter1, parameter2, parameter3, parameter4, parameter5); });
        }

        public override MutableExecution BeginAction<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6) {
            return BeginAction(() => { classAction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6); });
        }

        public override MutableExecution BeginAction<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7) {
            return BeginAction(() => { classAction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7); });
        }

        public override MutableExecution BeginAction<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> classAction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8) {
            return BeginAction(() => { classAction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8); });
        }

        public override MutableExecution BeginFunc<TResult>(Func<TResult> memberFunction) {
            MutableExecution execution = new NormalFuncExecution<TResult>(memberFunction);
            deferredExecutions.Append(new ExecutorExecution(execution));
            return execution;
        }

        public override MutableExecution BeginFunc<T1, TResult>(Func<T1, TResult> memberFunction, T1 parameter1) {
            return BeginFunc<TResult>(() => { return memberFunction(parameter1); });
        }

        public override MutableExecution BeginFunc<T1, T2, TResult>(Func<T1, T2, TResult> memberFunction, T1 parameter1, T2 parameter2) {
            return BeginFunc<TResult>(() => { return memberFunction(parameter1, parameter2); });
        }

        public override MutableExecution BeginFunc<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3) {
            return BeginFunc<TResult>(() => { return memberFunction(parameter1, parameter2, parameter3); });
        }

        public override MutableExecution BeginFunc<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) {
            return BeginFunc<TResult>(() => { return memberFunction(parameter1, parameter2, parameter3, parameter4); });
        }

        public override MutableExecution BeginFunc<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) {
            return BeginFunc<TResult>(() => { return memberFunction(parameter1, parameter2, parameter3, parameter4, parameter5); });
        }

        public override MutableExecution BeginFunc<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6) {
            return BeginFunc<TResult>(() => { return memberFunction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6); });
        }

        public override MutableExecution BeginFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7) {
            return BeginFunc<TResult>(() => { return memberFunction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7); });
        }

        public override MutableExecution BeginFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8) {
            return BeginFunc<TResult>(() => { return memberFunction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8); });
        }

        public override void DoNextChunk() {
            Array<ExecutorExecution> executions = deferredExecutions.Current;
            long length = executions.Length;
            if (length > 0) {
                bool finished = executions[iCurrentDeferredExecution].Execution.DoChunk();
                ++nChunksExecuted;
                if (finished) {
                    deferredExecutions.Remove(iCurrentDeferredExecution, 1);
                } else {
                    DateTimeOffset now = DateTimeOffset.UtcNow;
                    ExecutorExecution execution;
                    long iFirstTried = iCurrentDeferredExecution;
                    while (true) {
                        ++iCurrentDeferredExecution;
                        if (iCurrentDeferredExecution >= length) {
                            iCurrentDeferredExecution = 0;
                        }

                        // Have to stop after any complete loop around
                        if (iCurrentDeferredExecution == iFirstTried) {
                            break;
                        }
                        execution = executions[iCurrentDeferredExecution];

                        if (!execution.ScheduledAt.HasValue || execution.ScheduledAt.Value < now) {
                            break;
                        }
                    }
                }
            }
        }

        public virtual void ExecuteUntilEmpty() {
            while (!Empty) {
                DoNextChunk();
            }
        }

        public virtual void ExecuteUntilEmpty(TimeSpan maxTime) {
            DateTimeOffset startTime = DateTimeOffset.UtcNow;
            DateTimeOffset endTime = startTime + maxTime;
            while (!Empty && DateTimeOffset.UtcNow < endTime) {   //// need to check time way less often
                DoNextChunk(); //// trouble is that this might sleep
            }
        }

        // Returns false if not finished
        public virtual bool ExecuteUntilEmpty(int maxIterations) {
            int nIterationsSoFar = 0;
            while (!Empty) {
                DoNextChunk();
                ++nIterationsSoFar;
                if (nIterationsSoFar > maxIterations) {
                    return false;
                }
            }
            return true;
        }

        public override TResult Call<T1, TResult>(Func<T1, TResult> memberFunction, T1 parameter1) {
            return memberFunction(parameter1);
        }

        public override TResult Call<T1, T2, TResult>(Func<T1, T2, TResult> memberFunction, T1 parameter1, T2 parameter2) {
            return memberFunction(parameter1, parameter2);
        }

        public override TResult Call<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3) {
            return memberFunction(parameter1, parameter2, parameter3);
        }

        public override TResult Call<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) {
            return memberFunction(parameter1, parameter2, parameter3, parameter4);
        }

        public override TResult Call<T1, T2, T3, T4, T5, TResult>(Func<T1, T2, T3, T4, T5, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) {
            return memberFunction(parameter1, parameter2, parameter3, parameter4, parameter5);
        }

        public override TResult Call<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, T2, T3, T4, T5, T6, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6) {
            return memberFunction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
        }

        public override TResult Call<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7) {
            return memberFunction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
        }

        public override TResult Call<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> memberFunction, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6, T7 parameter7, T8 parameter8) {
            return memberFunction(parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
        }

        // Does nothing.
        public override void RemoveCurrent() { }

        public override void Schedule(MutableExecution execution, DateTimeOffset time) {
            deferredExecutions.Append(new ExecutorExecution(execution, time));
        }

        public override void WaitUntilFinished(Array<MutableExecution> executions) {
            foreach (MutableExecution execution in executions) {
                long i;
                if (deferredExecutions.Current.TryGetIndex(e => (e.Execution == execution), 0, out i)) {
                    execution.Finish();
                    deferredExecutions.Remove(i, 1);
                }
            }
        }

        protected class ExecutorExecution {
            public MutableExecution Execution { get; private set; }
            public DateTimeOffset? ScheduledAt { get; private set; }

            public ExecutorExecution(MutableExecution execution, DateTimeOffset? scheduledAt = null) {
                Execution = execution;
                ScheduledAt = scheduledAt;
            }
        }
    }
}
