using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Executions.Executors {
    // Things in execution queue are one-time, one-chunk only
    public partial class RoundRobinExecutor : Executor {
        private static TimeSpan tenMilliseconds = TimeSpan.FromMilliseconds(10);

        // The execution queue is just the executions currently in the round-robin rotation--not the parents etc.
        private ExecutionQueueRecord executionQueueHead;
        private ExecutionQueueRecord previousQueueNode;
        private ExecutionQueueRecord currentQueueNode;
        private Thread currentSleepingThread;
        private long chunksExecuted;

        // Ordered from soonest to last
        List<ExecutionWaitRecord> executionWaitQueue = new List<ExecutionWaitRecord>();

        public override long ChunksExecuted { get { return chunksExecuted; } }

        public override bool Empty {
            get {
                return object.ReferenceEquals(executionQueueHead.Next, executionQueueHead) && executionWaitQueue.Count == 0;
            }
        }

        public RoundRobinExecutor() {
            executionQueueHead = new ExecutionQueueRecord() {
                Execution = new InsertScheduledExecution(Execution.Dummy, this)
            };
            executionQueueHead.Next = executionQueueHead;
            currentQueueNode = executionQueueHead;
            previousQueueNode = currentQueueNode;
            Check();
        }

        public override void ExecuteChunk() {
            // If there are only scheduled activities, maybe we can sleep for a bit
            if (object.ReferenceEquals(executionQueueHead.Next, executionQueueHead)) {
                if (executionWaitQueue.Count != 0) {
                    DateTime now = DateTime.UtcNow;
                    TimeSpan timeUntilFirstScheduled = executionWaitQueue[0].Time - now;
                    if (timeUntilFirstScheduled > tenMilliseconds) {
                        currentSleepingThread = Thread.CurrentThread;
                        Thread.Sleep(timeUntilFirstScheduled);
                        currentSleepingThread = null;
                    }
                } else {
                    // No executions in queue anyway
                    return;
                }
            }

            currentQueueNode.Execution.DoChunk();
            ++chunksExecuted;

            // If it's done, remove it from execution queue
            if (currentQueueNode.Execution.Done) {
                RemoveCurrent();
            }

            // Move to next execution to work on
            previousQueueNode = currentQueueNode;
            currentQueueNode = currentQueueNode.Next;
            Check();
        }

        public override void RemoveCurrent() {
            currentQueueNode = currentQueueNode.Next;
            previousQueueNode.Next = currentQueueNode;
            Check();
        }

        public override void Schedule(Execution execution) {
            if (currentSleepingThread != null) {
                currentSleepingThread.Interrupt();
                currentSleepingThread = null;
            }
            execution.Executor = this;
            execution.TimeScheduled = Time;
            InsertBeforeCurrent(execution);
        }

        public override void Schedule(Execution execution, DateTime time) {
            if (currentSleepingThread != null) {
                currentSleepingThread.Interrupt();
                currentSleepingThread = null;
            }
            execution.Executor = this;
            execution.TimeScheduled = Time;

            // Insert before current node
            ExecutionWaitRecord newNode = new ExecutionWaitRecord() {
                Execution = execution,
                Time = time
            };
            int i = 0;
            for (; i < executionWaitQueue.Count; ++i) {
                if (time < executionWaitQueue[i].Time) {
                    break;
                }
            }
            executionWaitQueue.Insert(i, newNode);
        }

        public override void ScheduleBlocking(Execution execution) {
            if (currentSleepingThread != null) {
                currentSleepingThread.Interrupt();
                currentSleepingThread = null;
            }
            execution.Executor = this;
            execution.TimeScheduled = Time;

            // Replace execution in current node with the new one
            currentQueueNode.Execution = execution;
        }

        public override void ScheduleBlocking(Execution execution, DateTime time) {
            if (currentSleepingThread != null) {
                currentSleepingThread.Interrupt();
                currentSleepingThread = null;
            }
            execution.Executor = this;
            execution.TimeScheduled = Time;

            // Insert before current node
            ExecutionWaitRecord newNode = new ExecutionWaitRecord() {
                Execution = execution,
                Time = time
            };
            int i = 0;
            for (; i < executionWaitQueue.Count; ++i) {
                if (time < executionWaitQueue[i].Time) {
                    break;
                }
            }
            executionWaitQueue.Insert(i, newNode);
        }

        protected void InsertBeforeCurrent(Execution execution) {
            ExecutionQueueRecord newNode = new ExecutionQueueRecord() {
                Execution = execution,
                Next = currentQueueNode
            };
            previousQueueNode.Next = newNode;
            previousQueueNode = newNode;
            Check();
        }

        [DebuggerDisplay("{Execution.DebuggerDisplay}")]
        protected class ExecutionQueueRecord {
            public Execution Execution;
            public ExecutionQueueRecord Next;
        }

        protected class ExecutionWaitRecord {
            public Execution Execution;
            public DateTime Time;
        }

        protected class InsertScheduledExecution : Execution {
            public RoundRobinExecutor RoundRobinExecutor { get { return (RoundRobinExecutor)Executor; } }

            public InsertScheduledExecution(Execution parent, RoundRobinExecutor executor) : base(parent, Action.Dummy) {
                Executor = executor;
            }

            protected override bool TryFinish() {
                DateTime utcNow = DateTime.UtcNow;
                Executor.UpdateClockTime();
                int i = 0;

                // We will stop where i is the first execution not to be scheduled
                for (; i < RoundRobinExecutor.executionWaitQueue.Count; ++i) {
                    ExecutionWaitRecord waitRecord = RoundRobinExecutor.executionWaitQueue[i];
                    if (waitRecord.Time > utcNow) {
                        break;
                    }
                    RoundRobinExecutor.InsertBeforeCurrent(waitRecord.Execution);
                }
                if (i > 0) {
                    RoundRobinExecutor.executionWaitQueue.RemoveRange(0, i);
                }
                return false;
            }
        }
    }
}
