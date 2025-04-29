using System;
using System.Diagnostics;

namespace Merrigan0 {
    [Untested]
    public class NotNullTest : CompiledTest {
        public static readonly NotNullTest Only = new NotNullTest();

        public NotNullTest() : base("not null") { }

        protected override bool ReallyRun(object[] arguments, out String errorMessage) {
            if (arguments[0] == null) {
                errorMessage = "was null";
                return false;
            }
            errorMessage = null;
            return true;
        }
    }
}
