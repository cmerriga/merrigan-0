using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Executions.Actions;
using Executions.Executors;
using ExecutionsExecution = Executions.Execution;
using ExecutionsTest = Executions.Test;

namespace Executions.Executions {
    public partial class RecursiveCallExecution : Execution {
        public static new void Test() {
            TestRecursiveExecution();
        }

        protected static void TestRecursiveExecution() {
            // Set up program
            /*
             * long Fibonacci(long n) {
             *     if (n == 0) {
             *         return 0;
             *     }
             *     if (n == 1) {
             *         return 1;
             *     }
             *     return Fibonacci(n - 2) + Fibonacci(n - 1);
             * }
             * 
             * Fibaonacci(8);
             */
            Action fibonacci = new SequentialAction(
                new IfAction(
                    new EqualLongExpression(new GetExpression<long>("n"), 0),
                    new ReturnConstantAction<long>(0)),
                new IfAction(
                    new EqualLongExpression(new GetExpression<long>("n"), 1),
                    new ReturnConstantAction<long>(1)),
                new ReturnAction<long>(
                    new AddLongExpression(
                        new RecursiveCallAction(3, new AddLongExpression(new GetExpression<long>("n"), -2L)),
                        new RecursiveCallAction(3, new AddLongExpression(new GetExpression<long>("n"), -1L))))
            );

            // Run program
            Executor executor = new RoundRobinExecutor();
            Execution execution = fibonacci.CreateExecution(null);
            execution.Context.Set("n", 1L);
            executor.Schedule(execution);
            if (!executor.ExecuteUntilEmpty(10000)) {
                Console.WriteLine("Loop executed indefinitely.");
            }
            long result = (long)execution.Context[ExecutionsExecution.ReturnVariableName];
            ExecutionsTest.TestEqual(result, 1, "Result should have been 1 but was {0}", result);
        }
    }
}
