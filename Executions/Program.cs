using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Executions.Executions;
using Executions.Executors;

namespace Executions {
    class Program {
        static void Main(string[] args) {
            Test();
        }

        public static void Test() {
            Utilities.Test();
            ////Text.Test();
            ////SimpleContext.Test();
            ////Action.Test();
            ////Execution.Test();
            ////RecursiveCallExecution.Test();
            ////RoundRobinExecutor.Test();

            StructuredProgramming.Assignment<int>.Test();
            StructuredProgramming.Block.Test();
            StructuredProgramming.Call<int>.Test();
        }
    }
}
