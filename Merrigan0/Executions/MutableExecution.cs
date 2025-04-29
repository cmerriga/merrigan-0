using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Merrigan0.ExecutionsInternal;

namespace Merrigan0 {
    [Untested]
    public abstract class MutableExecution {
        public static readonly MutableExecution Dummy = new DummyExecution();

        private MutableExecution parent;

        public Execution Current { get; protected set; }

        //public void ReportCanceled() {
        //    Current = new ChangeStageExecution(Current, ExecutionStage.Finished);
        //}

        //public void ReportFailed() {
        //    Current = new ChangeStageExecution(Current, ExecutionStage.Finished);
        //}

        //public void ReportSucceeded() {
        //}

        //public void RequestCancel() {
        //}

        //public void RequestPause() {
        //}

        //public void RequestResume() {
        //}

        //public void RequestStart() {
        //}

        public MutableExecution() { }

        public MutableExecution(MutableExecution parent) {
            this.parent = parent;
        }

        //// First have to resolve parameters given and assign to names in the context - well, no
        //// Then have to 
        [return: WhatItIs("whether the execution is finished")]
        public virtual bool DoChunk() {
            if (!Current.StartTime.HasValue) {
                Current = new StartExecution(Current, Current.Executor.Time);
            }
            bool done = ReallyDoChunk();
            if (done) {
                MarkDone();
                return true;
            }
            return false;
        }

        [WhenItsCalled("by an Executor")]
        public void Finish() {
            while (!DoChunk()) { }
        }

        public virtual void ReportChildDone(Execution child) { }

        [WhatItIs("Waits for the execution to finish.")]
        [WhenItsCalled("Often from another thread.")]
        public void WaitUntilFinished() { WaitUntilFinished(TimeSpan.FromMilliseconds(100)); }

        [WhatItIs("Waits for the execution to finish.")]
        [WhenItsCalled("Often from another thread.")]
        [Concept("delay", "lag time between true execution finish, and when this function returns")]
        public void WaitUntilFinished(TimeSpan maxDelay) {
            // Start with a short wait and double until equal to max delay
            TimeSpan nextDelay = TimeSpan.FromMilliseconds(1);
            while (!Current.Finished) {
                Thread.Sleep(nextDelay);
                if (nextDelay < maxDelay) {
                    nextDelay = nextDelay + nextDelay;
                    if (nextDelay > maxDelay) {
                        nextDelay = maxDelay;
                    }
                }
            }
        }

        [WhatItIs("The meat of the execution.")]
        [WhenItsCalled("Repeatedly until the execution is finished.")]
        [Note("May be called multiple times after execution is finished.")]
        [return: WhatItIs("Whether the execution is finished.")]
        protected abstract bool ReallyDoChunk();

        protected virtual void MarkDone() {
            Current = new FinishExecution(Current, null, Current.Executor.Time);
            parent.ReportChildDone(this.Current);
        }
    }
}
