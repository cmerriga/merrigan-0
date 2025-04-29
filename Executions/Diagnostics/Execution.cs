using System;
using System.Collections.Generic;
using System.Diagnostics;
using Executions.Actions;
using Executions.Executors;
using ExecutionsTest = Executions.Test;

namespace Executions {
    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class Execution {
        protected virtual string DebuggerDisplay {
            get {
                string typeName = this.GetType().Name;
                typeName = Text.RemoveNonalphanumeric(typeName);
                IList<string> parts = Text.GetLowerCasePartsFromMixedCase(typeName);
                List<string> partsWithoutAction = new List<string>(parts);
                for (int i = 0; i < partsWithoutAction.Count; ++i) {
                    if (partsWithoutAction[i] == "execution") {
                        partsWithoutAction.RemoveRange(i, partsWithoutAction.Count - i);
                        break;
                    }
                }
                string name = Text.CombineAsPascalCase(partsWithoutAction);
                return Done ? name + " done" : name;
            }
        }

        internal static void Test() {
            TestSingleExecution();
            TestComplexExecution();
            TestScheduledExecution();
        }

        protected static void TestSingleExecution() {
            Executor executor = new RoundRobinExecutor();
            bool done = false;
            Execution execution = new CustomAction(() => { done = true; }).CreateExecution(null);
            executor.Schedule(execution);
            executor.ExecuteUntilEmpty();
            ExecutionsTest.TestEqual(done, true, "Custom action was not run.");
        }

        protected static void TestComplexExecution() {
            // Set up program
            /*
             * a = 4;
             * while (a < 123) {
             *      a = a * 2;
             * }
             * return a;
             */
            Action action = new SequentialAction(
                new SetConstantAction<long>("a", 4L),
                new WhileAction(
                    new LessThanLongExpression(new GetExpression<long>("a"), 123L),
                    new SetExpressionAction<long>(
                        "a",
                        new MultiplyLongExpression(new GetExpression<long>("a"), 2L))),
                new SetExpressionAction<long>(Execution.ReturnVariableName, new GetExpression<long>("a")));

            // Run program
            Executor executor = new RoundRobinExecutor();
            Execution execution = action.CreateExecution(Execution.Dummy);
            executor.Schedule(execution);
            if (!executor.ExecuteUntilEmpty(10000)) {
                Console.WriteLine("Loop executed indefinitely.");
            }
            long result = (long)execution.Context[Execution.ReturnVariableName];
            ExecutionsTest.TestEqual(result, 128, "Result should have been 128 but was {0}", result);
        }

        protected static void TestScheduledExecution() {
            Executor executor = new RoundRobinExecutor();
            bool done1 = false;
            DateTime scheduleTime = DateTime.UtcNow;
            Execution scheduledExecution1 = new CustomAction(() => { done1 = true; }).CreateExecution(null);
            executor.Schedule(scheduledExecution1, scheduleTime.AddSeconds(1));
            bool done2 = false;
            Execution scheduledExecution2 = new CustomAction(() => { done2 = true; }).CreateExecution(null);
            executor.Schedule(scheduledExecution2, scheduleTime.AddSeconds(2));
            executor.ExecuteUntilEmpty();
            ExecutionsTest.TestEqual(done1, true, "Action 1 was not run.");
            ExecutionsTest.TestEqual(done2, true, "Action 2 was not run.");
            ExecutionsTest.TestWithinRange(scheduledExecution1.TimeStarted.Value.ClockTime, scheduleTime.AddSeconds(1), TimeSpan.FromMilliseconds(100), "Execution 1 didn't start in 1 second.");
            ExecutionsTest.TestWithinRange(scheduledExecution2.TimeStarted.Value.ClockTime, scheduleTime.AddSeconds(2), TimeSpan.FromMilliseconds(100), "Execution 2 didn't start in 2 seconds.");
        }
    }
}
