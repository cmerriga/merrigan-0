using System;
using System.Collections;
using System.Reflection;
using Merrigan0.Internal.DotNet.Polyfills.System;
using Merrigan0.GlomsInternal;

namespace Merrigan0 {
    public class StrategyTest : CompiledTest {
        private TestStrategy strategy;
        private CompiledTest test;

        public StrategyTest(CompiledTest test, TestStrategy strategy) : base(strategy.Description(test)) {
            this.strategy = strategy;
            this.test = test;
        }

        //public override TestExecution Run(params object[] arguments) {
        //    TestExecution execution;
        //    using (execution = new TestExecution(this, arguments)) {
        //        if (execution.StopRequested) {
        //            execution.ReportSkipped();
        //        } else {
        //            execution.ReportStarted();
        //            try {
        //                String errorMessage;
        //                if (ReallyRun(arguments, out errorMessage)) {
        //                    execution.ReportSucceeded();
        //                } else {
        //                    execution.ReportFailed(errorMessage);
        //                }
        //            } catch (Exception e) {
        //                execution.ReportFailed(e);
        //            }
        //        }
        //    }
        //    return execution;
        //}

        protected override bool ReallyRun(object[] arguments, out String errorMessage) {
            return strategy.Run(test, arguments, out errorMessage);
        }
    }
}
