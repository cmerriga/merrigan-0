using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Executions.Actions {
    using Merrigan0.Internal.DotNet.Polyfills.System;

    public class DummyAction : Action {
        // {constant|expression}*
        public DummyAction() {
            Prerequisites = Array.Empty<Prerequisite>();
        }

        public override Execution CreateExecution(Execution parent) {
            return null;
        }
    }
}
