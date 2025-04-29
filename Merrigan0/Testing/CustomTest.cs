using System;

namespace Merrigan0 {
    // A test that has no virtual functions overridden at compile time.
    [Untested]
    public class CustomTest : CompiledTest {
        private Func<object[], bool> reallyRunFunction;

        public CustomTest(String description, Func<object[], bool> reallyRunFunction)
            : base(description) {
            this.reallyRunFunction = reallyRunFunction;
        }

        protected override bool ReallyRun(object[] arguments, out String errorMessage) {
            errorMessage = null;
            return reallyRunFunction(arguments);
        }
    }
}
