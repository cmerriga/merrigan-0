using System;
using System.Collections.Generic;

namespace Executions {
    public struct ExecutorTime {
        public DateTime ClockTime;
        public long Ticks;
    }

    public abstract class Executor {
        private TimeSpan tenMilliseconds = TimeSpan.FromMilliseconds(10);
        ExecutorTime time;

        public abstract long ChunksExecuted { get; }

        public abstract bool Empty { get; }

        public ExecutorTime Time {
            get {
                ++time.Ticks;
                return time;
            }
            protected set {
                time = value;
            }
        }

        public abstract void ExecuteChunk();

        public virtual void ExecuteUntilEmpty() {
            while (!Empty) {
                ExecuteChunk();
            }
        }

        public virtual void ExecuteUntilEmpty(TimeSpan maxTime) {
            DateTime startTime = DateTime.UtcNow;
            DateTime endTime = startTime + maxTime;
            while (!Empty && DateTime.UtcNow < endTime) {   //// need to check time way less often
                ExecuteChunk(); //// trouble is that this might sleep
            }
        }

        // Returns false if not finished
        public virtual bool ExecuteUntilEmpty(int maxIterations) {
            int nIterationsSoFar = 0;
            while (!Empty) {
                ExecuteChunk();
                ++nIterationsSoFar;
                if (nIterationsSoFar > maxIterations) {
                    return false;
                }
            }
            return true;
        }

        public abstract void RemoveCurrent();
        public abstract void Schedule(Execution execution);
        public abstract void Schedule(Execution execution, DateTime time);
        public abstract void ScheduleBlocking(Execution execution);
        public abstract void ScheduleBlocking(Execution execution, DateTime time);

        public virtual void UpdateClockTime() {
            DateTime now = DateTime.UtcNow;
            if (now != time.ClockTime) {
                time.ClockTime = now;
                time.Ticks = 0;
            }
        }
    }
}
