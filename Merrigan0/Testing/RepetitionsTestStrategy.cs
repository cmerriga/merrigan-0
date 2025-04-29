using System;
using System.Collections;
using System.Reflection;
using Merrigan0.Internal.DotNet.Polyfills.System;
using Merrigan0.GlomsInternal;

namespace Merrigan0 {
    public class RepetitionsTestStrategy : TestStrategy {
        private int repetitions;

        public RepetitionsTestStrategy(int repetitions) {
            this.repetitions = repetitions;
        }

        //public override bool MustMoveOn(TestExecution execution) {
        //    return execution.ChildTests.Length >= repetitions;
        //}

        public override String Description(Test test) {
            return test.Description + " " + repetitions + "x";
        }

        public override bool Run(CompiledTest test, object[] arguments, out String errorMessage) {
            int repetitionsRemaining = repetitions;
            bool failed = false;
            errorMessage = null;
            while (repetitionsRemaining > 0) {
                TestExecution execution = test.Run(arguments);
                if (execution.Failed) {
                    failed = true;
                    errorMessage = execution.FailureMessage;
                }
               --repetitionsRemaining;
            }
            if (failed) {
                return false;
            }
            return true;
        }
    }
}
