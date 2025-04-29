using System;
using System.Collections;
using System.Reflection;
using Merrigan0.Internal.DotNet.Polyfills.System;
using Merrigan0.GlomsInternal;

namespace Merrigan0 {
    public class UntilFailTestStrategy : TestStrategy {
        private int maxRepetitions;

        public UntilFailTestStrategy() {
            this.maxRepetitions = 1000;
        }

        public UntilFailTestStrategy(int maxRepetitions) {
            this.maxRepetitions = maxRepetitions;
        }

        //public override bool MustMoveOn(TestExecution execution) {
        //    if (execution.ChildTests.Any(e => e.Failed)) {
        //        return true;
        //    }
        //    return execution.ChildTests.Length >= maxRepetitions;
        //}

        public override String Description(Test test) {
            return test.Description + " until fail";
        }

        public override bool Run(CompiledTest test, object[] arguments, out String errorMessage) {
            int repetitionsRemaining = maxRepetitions;
            while (repetitionsRemaining > 0) {
                // Make a new child execution
                TestExecution execution = test.Run(arguments);
                if (execution.Failed) {
                    errorMessage = execution.FailureMessage;
                    return false;
                }
                --repetitionsRemaining;
            }
            errorMessage = null;
            return true;
        }
    }
}
