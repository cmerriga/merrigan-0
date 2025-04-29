using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Executions.Executions {
    public class DummyExecution : Execution {
        public DummyExecution() : base(null, null) {
            Context = new SimpleContext();
        }

        public override bool DoChunk() { return false; }

        protected override bool TryFinish() {
            throw new InvalidOperationException();
        }
    }
}
