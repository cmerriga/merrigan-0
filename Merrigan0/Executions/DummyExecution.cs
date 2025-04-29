using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Merrigan0.ExecutionsInternal {
    internal class DummyExecution : MutableExecution {
        public DummyExecution() : base() {
            ////Context = new SimpleContext();
        }

        public override bool DoChunk() { return false; }

        /// [Invalid]
        protected override bool ReallyDoChunk() {
            throw new InvalidOperationException();
        }

        protected override void MarkDone() {
            Current = new FinishExecution(Current, null, Current.Executor.Time);
        }
    }
}
