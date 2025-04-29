using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Executions.Actions;
using ExecutionsTest = Executions.Test;

namespace Executions.Executors {
    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class RoundRobinExecutor {
        private string DebuggerDisplay {
            get {
                if (!Check()) {
                    return "[CORRUPT]";
                }

                int executionLengthSoFar = 0;
                ExecutionQueueRecord record = executionQueueHead;
                while (true) {
                    ++executionLengthSoFar;
                    record = record.Next;
                    if (Object.ReferenceEquals(record, executionQueueHead)) {
                        break;
                    }
                }
                return "Current executions: " + executionLengthSoFar;
            }
        }

        public static void Test() {
            TestThreadSleeps();
        }

        protected static void TestThreadSleeps() {
            Executor executor = new RoundRobinExecutor();
            bool done1 = false;
            DateTime scheduleTime = DateTime.UtcNow;
            Execution scheduledExecution1 = new CustomAction(() => { done1 = true; }).CreateExecution(null);
            executor.Schedule(scheduledExecution1, scheduleTime.AddSeconds(1));
            bool done2 = false;
            Execution scheduledExecution2 = new CustomAction(() => { done2 = true; }).CreateExecution(null);
            executor.Schedule(scheduledExecution2, scheduleTime.AddSeconds(2));
            int nDurationsOverTwoMilliseconds = 0;
            while (!executor.Empty) {
                DateTime startTime = DateTime.UtcNow;
                executor.ExecuteChunk();
                TimeSpan duration = DateTime.UtcNow - startTime;
                if (duration > TimeSpan.FromMilliseconds(2)) {
                    ++nDurationsOverTwoMilliseconds;
                }
            }
            ExecutionsTest.TestEqual(done1, true, "Action 1 was not run.");
            ExecutionsTest.TestEqual(done2, true, "Action 2 was not run.");
            ExecutionsTest.TestEqual(nDurationsOverTwoMilliseconds, 2, "The wrong number of long chunks were run: {0}", nDurationsOverTwoMilliseconds);
            ExecutionsTest.TestLessOrEqual(executor.ChunksExecuted, 100000, "Too many short chunks were run: {0}", executor.ChunksExecuted);
        }

        protected bool Check() {
            // Make sure queue links back to head
            // Make sure previous and current are in queue
            ExecutionQueueRecord record = executionQueueHead;
            HashSet<ExecutionQueueRecord> recordsChecked = new HashSet<ExecutionQueueRecord>();
            bool currentRecordFound = false;
            bool previousRecordFound = false;
            while (true) {
                if (recordsChecked.Contains(record)) {
                    return false;
                }
                recordsChecked.Add(record);
                if (Object.ReferenceEquals(record, currentQueueNode)) {
                    currentRecordFound = true;
                }
                if (Object.ReferenceEquals(record, previousQueueNode)) {
                    previousRecordFound = true;
                }
                record = record.Next;
                if (Object.ReferenceEquals(record, executionQueueHead)) {
                    break;
                }
            }
            if (!currentRecordFound || !previousRecordFound) {
                return false;
            }

            // Make sure everything in wait queue is ordered properly
            for (int i = 1; i < executionWaitQueue.Count; ++i) {
                if (executionWaitQueue[i].Time < executionWaitQueue[i - 1].Time) {
                    return false;
                }
            }
            return true;
        }
    }
}
