using System;

namespace Merrigan0 {
    // A test that is pre-coded, not wrapped around inline code
    [Untested]
    public abstract class CompiledTest : Test {
        public CompiledTest(String description) : base(description) { }

        public virtual TestExecution Run(params object[] arguments) {
            TestExecution execution;
            using (execution = new TestExecution(this, arguments)) {
                if (execution.StopRequested) {
                    execution.ReportSkipped();
                } else {
                    execution.ReportStarted();
                    try {
                        String errorMessage;
                        if (ReallyRun(arguments, out errorMessage)) {
                            execution.ReportSucceeded();
                        } else {
                            execution.ReportFailed(errorMessage);
                        }
                    } catch (Exception e) {
                        execution.ReportFailed(e);
                    }
                }
            }
            return execution;
        }

        [return: WhatItIs("whether the test succeeded")]
        protected abstract bool ReallyRun(object[] arguments, out String errorMessage);
    }
}
